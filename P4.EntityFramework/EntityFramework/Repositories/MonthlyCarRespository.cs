using Abp.EntityFramework;
using P4.MonthlyCars;
using P4.MonthlyCars.Dtos;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Data.SqlClient;


namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class MonthlyCarRespository : P4RepositoryBase<MonthlyCar>, IMonthlyCarRespository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public MonthlyCarRespository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public MonthlyCars.Dtos.GetAllMonthlyCarsOutput GetMonthlyCarAll(MonthlyCars.Dtos.MonthlyCarInput input)
        {
            int records = Table.Filters(input).Count();
            MonthlyCarDto b = new MonthlyCarDto();
            int parkid = int.Parse(b.ParkIds);
            var query = Table.Select(c => new MonthlyCarDto
            {
                Id = c.Id,
                BeginTime = c.BeginTime,
                EndTime = c.EndTime,
                Money = c.Money,
                PlateNumber = c.PlateNumber,
                Telphone = c.Telphone,
                VehicleOwner = c.VehicleOwner
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllMonthlyCarsOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }




        /// <summary>
        /// 收费员获取包月卡数据
        /// </summary>
        /// <param name="parkid"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public List<MonthlyCarDto> GetAllMonthlyCarBySql(string parkid, int TenantId, int CompanyId)
        {
            string[] strs = parkid.Split(',');
            List<MonthlyCarDto> monthlyList = new List<MonthlyCarDto>();
            string sqlStr = "select  Id, VehicleOwner, Telphone, PlateNumber, Money, BeginTime, EndTime, TenantId, CompanyId, IsDeleted, DeleterUserId, DeletionTime, LastModificationTime, LastModifierUserId, CreationTime, CreatorUserId, @parkid as ParkIds, CarType,  Version, MonthyType from AbpMonthlyCars with(nolock) where (case when CompanyId is null then AbpMonthlyCars.tenantid else " + TenantId + "  end)  = " + TenantId + " and (case when CompanyId is not null then AbpMonthlyCars.CompanyId else " + CompanyId + " end) = " + CompanyId + " and BeginTime <= getdate() and EndTime >= getdate() and (charindex(','+@parkid+ ',', ','+ ParkIds + ',') > 0 or ParkIds = '0') and IsDeleted = 0";
            foreach (var str in strs)
            {
                monthlyList.AddRange(Context.Database.SqlQuery<MonthlyCarDto>(sqlStr, new SqlParameter[] { new SqlParameter("@parkid",  str ) }).ToList());
            }
            return monthlyList;
        }
    }
}
