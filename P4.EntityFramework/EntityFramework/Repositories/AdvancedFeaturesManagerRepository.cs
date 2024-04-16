using Abp.EntityFramework;
using P4.AdvancedFeaturesManager;
using P4.AdvancedFeaturesManager.Model;
using P4.Berthsecs;
using P4.Businesses;
using P4.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.Rates.Dtos;

namespace P4.EntityFramework.Repositories
{
    public class AdvancedFeaturesManagerRepository : P4RepositoryBase<Berthsec>, IAdvancedFeaturesManagerRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public AdvancedFeaturesManagerRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 平账(现金平账)
        /// </summary>
        /// <param name="view">入参</param>
        public int FlatAccount(FlatAccountView view)
        {
            //获取收费员信息
            var employee = Context.Employees.FirstOrDefault(u => u.UserName == view.InOperaId);
            if (employee == null)
                return 0;
            //加账 逻辑基本保持不变，只是车牌号取消费最大的车牌号
            #region 加账
            if (view.AverageAccount > 0)
            {
                //status = 2表示支付方式是现金，PayStatus = 1，支付状态已支付
                string StrSql = @"select top 1 * from AbpBusinessDetail a with(nolock) 
            inner join AbpEmployees b on b.Id=a.InOperaId and b.UserName=@InOperaId 
            where a.companyid in (@CompanyIds) and a.TenantId = @TenantId
            and (a.carpaytime between @begindt and @enddt) 
            and a.isdeleted =@isdeleted  and a.BerthsecId = @BerthsecId   and a.PayStatus = @PayStatus and a.status = 2 order by Receivable desc";

                var entity = Context.Database.SqlQuery<BusinessDetail>(StrSql, new SqlParameter[]
                {
                new SqlParameter("@CompanyIds",string.Join(",", view.CompanyIds)),
                new SqlParameter("@TenantId",view.TenantId),
                new SqlParameter("@begindt",view.begindt),
                new SqlParameter("@enddt",view.enddt),
                new SqlParameter("@isdeleted",view.isdeleted),
                new SqlParameter("@BerthsecId",view.BerthsecId),
                new SqlParameter("@InOperaId",view.InOperaId),
                new SqlParameter("@PayStatus",view.PayStatus),
                }).FirstOrDefault();

                if (entity != null)
                {
                    //查找到选择收费员并且泊位号跟纸质版表格上一致，锁定一条金额最大收费记录，新增一条无牌车记录
                    entity.Receivable = view.AverageAccount;//平账金额
                    entity.Money = view.AverageAccount;
                    entity.FactReceive = view.AverageAccount;
                    entity.PlateNumber = entity.PlateNumber;

                    //新增一条无牌车记录
                    Context.BusinessDetails.Add(entity);
                    return Context.SaveChanges();

                }
                else
                {
                    //暂无记录，异常处理
                    return 0;
                }
            }
            #endregion
            else //减账
            {
                //根据收费员和所在泊位段获取费率设置
                string rateSql = @"SELECT * FROM dbo.AbpRates WHERE IsDeleted=0 AND IsActive=1 AND TenantId=@TenantId AND CompanyId IN (@CompanyId) 
AND Id IN (
 SELECT  CASE WHEN  RateId >0 THEN RateId WHEN RateId1>0 then  RateId ELSE RateId2 END AS RateId 
 FROM dbo.AbpBerthsecs a
 INNER JOIN AbpWorkGroupBerthsecs b
 ON a.Id=b.BerthsecId AND b.IsDeleted=0 AND b.IsActive=1
 INNER JOIN AbpWorkGroupEmployees c
 ON b.WorkGroupId=c.Id AND c.IsDeleted=0 AND c.IsActive=1
 WHERE  c.EmployeeId=@EmployeeId) ";
                //所有正在使用的费率
                var rateList = Context.Database.SqlQuery<Rates.Rate>(rateSql, new SqlParameter[]
                {
                    new SqlParameter("@CompanyId",string.Join(",", view.CompanyIds)),
                    new SqlParameter("@TenantId",view.TenantId),
                    new SqlParameter("@EmployeeId",employee.Id)
                }).ToList();

                //没有费率设置
                if (rateList.Count == 0)
                {
                    return 0;
                }

                var rate = rateList.First();//取第一条费率设置
                var set = rate.RateJson.DeserializeObject<RateModel>();
                var MinAmount = set.GetMinConsume();//最低消费金额

                //当天该收费员负责的订单 按金额从大到小排序
                string StrSql = @"
SELECT *
FROM AbpBusinessDetail a WITH(NOLOCK)
     INNER JOIN AbpEmployees b 
ON b.Id=a.ChargeOperaId AND b.Id=@InOperaId
WHERE a.CompanyId IN (@CompanyIds)AND
	  a.TenantId=@TenantId AND
	  (a.CarPayTime BETWEEN @begindt AND @enddt)AND 
	  a.IsDeleted=@isdeleted AND 
	  a.BerthsecId=@BerthsecId AND 
	  a.ChargeOperaId=@InOperaId AND a.PayStatus=@PayStatus AND a.Receivable>0 AND a.Status =2
UNION ALL
SELECT *
FROM AbpBusinessDetail a WITH(NOLOCK)
     INNER JOIN AbpEmployees b 
ON b.Id=a.EscapeOperaId AND b.Id=@InOperaId
WHERE a.CompanyId IN (@CompanyIds)AND
	  a.TenantId=@TenantId AND
	  (a.EscapeTime BETWEEN @begindt AND @enddt)AND 
	  a.IsDeleted=@isdeleted AND 
	  a.BerthsecId=@BerthsecId AND 
	  a.EscapeOperaId=@InOperaId AND a.PayStatus=@PayStatus AND a.Repayment>0 AND a.Status =4
ORDER BY a.Receivable DESC, a.Repayment DESC
";//现金支付 正常缴纳+逃逸补缴
                var businessDetails = Context.Database.SqlQuery<BusinessDetail>(StrSql, new SqlParameter[]
                {
                    new SqlParameter("@CompanyIds",string.Join(",", view.CompanyIds)),
                    new SqlParameter("@TenantId",view.TenantId),
                    new SqlParameter("@begindt",view.begindt),
                    new SqlParameter("@enddt",view.enddt),
                    new SqlParameter("@isdeleted",view.isdeleted),
                    new SqlParameter("@BerthsecId",view.BerthsecId),
                    new SqlParameter("@InOperaId",employee.Id),
                    new SqlParameter("@PayStatus",view.PayStatus),
                }).ToList();

                foreach (var oneOrder in businessDetails)
                {
                    if (view.AverageAccount >= 0)
                    {
                        break;
                    }
                    if (oneOrder.Status == 2)
                    {
                        var fillAmount = oneOrder.Receivable - MinAmount;//需要填充金额=实收-最小消费金额
                        var changeAmount = fillAmount + view.AverageAccount > 0 ? -view.AverageAccount : fillAmount;//最多平账金额
                                                                                                                    //获取一个合理的停车区间
                        var timeList = set.GetRationalParkTime(oneOrder.CarInTime.Value, oneOrder.FactReceive - changeAmount);
                        if (timeList.Item1 == timeList.Item2)
                        {
                            continue;
                        }
                        //更新数据
                        Context.BusinessDetails.Attach(oneOrder);
                        oneOrder.CarOutTime = timeList.Item2;
                        oneOrder.Receivable -= changeAmount;
                        oneOrder.Money -= changeAmount;
                        oneOrder.FactReceive -= changeAmount;
                        //oneOrder.Arrearage -= changeAmount;
                        //oneOrder.CarPayTime = timeList.Item2;
                        oneOrder.StopTime = (oneOrder.CarOutTime.Value - oneOrder.CarInTime.Value).Minutes;
                        //减去本次平账金额
                        view.AverageAccount += changeAmount;
                    }
                    else if(oneOrder.Status == 4)
                    {
                        if (!oneOrder.Repayment.HasValue || oneOrder.Repayment.Value <= 0)
                        {
                            continue;
                        }
                        var fillAmount = oneOrder.Repayment.Value - MinAmount;//需要填充金额=实收-最小消费金额
                        var changeAmount = fillAmount + view.AverageAccount > 0 ? -view.AverageAccount : fillAmount;//最多平账金额
                                                                                                                    //获取一个合理的停车区间
                        var timeList = set.GetRationalParkTime(oneOrder.CarInTime.Value, oneOrder.Arrearage - changeAmount);
                        if (timeList.Item1 == timeList.Item2)
                        {
                            continue;
                        }
                        //更新数据
                        Context.BusinessDetails.Attach(oneOrder);
                        oneOrder.CarOutTime = timeList.Item2;
                        oneOrder.Receivable -= changeAmount;
                        oneOrder.Money -= changeAmount;
                        oneOrder.Arrearage -= changeAmount;
                        oneOrder.Repayment -= changeAmount;
                        oneOrder.CarPayTime = timeList.Item2;
                        oneOrder.StopTime = (oneOrder.CarOutTime.Value - oneOrder.CarInTime.Value).Minutes;
                        //减去本次平账金额
                        view.AverageAccount += changeAmount;
                    }
                   
                }
                if (view.AverageAccount < 0) return 0;
                return Context.SaveChanges();
            }
        }

        /// <summary>
        /// 获取收费员分配的所有泊位段
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public List<Berthsec> getBerthsecAllListByEmpId(getBerthsecAllListByEmpIdInput view)
        {
            string StrSql = @"select * from AbpBerthsecs a where a.Id in (
                select BerthsecId from  AbpWorkGroupBerthsecs where 
                WorkGroupId in (
                select  WorkGroupId from  AbpWorkGroupEmployees a
                inner join AbpEmployees b on b.Id=a.EmployeeId where b.UserName=@EmployeeId
                and b.IsDeleted=0)  
                and IsDeleted=0 and IsActive=1
                ) and IsActive=1 and CompanyId in (@CompanyId) and TenantId=@TenantId";

            var lst = Context.Database.SqlQuery<Berthsec>(StrSql, new SqlParameter[]
            {
                new SqlParameter("@CompanyId",string.Join(",", view.CompanyIds)),
                new SqlParameter("@TenantId",view.TenantId),
                new SqlParameter("@EmployeeId",view.EmployeeId)
            }).ToList();

            if (lst != null && lst.Count > 0)
            {
                return lst;
            }
            else
            {
                //暂无记录，异常处理
                return new List<Berthsec>();
            }
        }
    }
}
