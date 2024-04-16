using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.WorkGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using P4.Employees.Dtos;
using System.Data;
using P4.Berthsecs.Dto;
using P4.Berths.Dtos;
using P4.EmployeeCharges.Dto;
using System.Data.SqlClient;

namespace P4.WorkGroups
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkGroupAppService : ApplicationService, IWorkGroupAppService
    {
        #region Var
        private readonly IRepository<WorkGroup> _abpWorkGroupRepository;
        private readonly IRepository<WorkGroupBerthsec> _abpWorkGroupBerthsecRepository;
        private readonly IRepository<WorkGroupEmployee> _abpWorkGroupEmployeeRepository;
        private readonly IWorkGroupRepository _workgroupRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpWorkGroupRepository"></param>
        /// <param name="abpWorkGroupBerthsecRepository"></param>
        /// <param name="abpWorkGroupEmployeeRepository"></param>
        /// <param name="workgroupRepository"></param>
        public WorkGroupAppService(IRepository<WorkGroup> abpWorkGroupRepository, IRepository<WorkGroupBerthsec> abpWorkGroupBerthsecRepository,
            IRepository<WorkGroupEmployee> abpWorkGroupEmployeeRepository, IWorkGroupRepository workgroupRepository)
        {
            _abpWorkGroupRepository = abpWorkGroupRepository;
            _abpWorkGroupBerthsecRepository = abpWorkGroupBerthsecRepository;
            _abpWorkGroupEmployeeRepository = abpWorkGroupEmployeeRepository;
            _workgroupRepository = workgroupRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWorkGroupOutput GetAllWorkGroupList(WorkGroupInput input)
        {

            return _workgroupRepository.GetAllWorkGroupList(input);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workgroupId"></param>
        /// <param name="workStatus"></param>
        /// <returns></returns>
        public WorkGroupDto GetWorkGroupList(int workgroupId,int workStatus)
        {
            return _workgroupRepository.GetWorkGroupList(workgroupId,workStatus);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Insert(CreateOrUpdateWorkGroupInput input)
        {
            if (_abpWorkGroupRepository.FirstOrDefault(dic => dic.WorkGroupName == input.WorkGroupName) != default(WorkGroup))
            {
                //已存在typeCode
                return 1;
            }
            //判断工作组名称是否为空
            if(String.IsNullOrEmpty(input.WorkGroupName))
            {
                return 2;
            }

            WorkGroup entity = new WorkGroup();
            entity.TenantId = input.TenantId;
            entity.WorkGroupName = input.WorkGroupName;
            entity.CompanyId = input.CompanyId;
            entity.IsActive = input.IsActive;
            entity.Status = input.Status;

            var workGroupId = _abpWorkGroupRepository.InsertAndGetId(entity);



            //删除用户得时候会把用户角色表中该用户的信息清空
            //删除角色时 查询是否已经为该角色分配了用户 如果分配了用户？？？？

            //如果model.RoleDisplayName不为空 更改UserRole表数据

            if (!string.IsNullOrWhiteSpace(input.EmployeesString))
            {
                string[] strarr = input.EmployeesString.Split(',');

                foreach (var i in strarr)
                {
                    WorkGroupEmployee wu = new WorkGroupEmployee();
                    wu.WorkGroupId = workGroupId;
                    wu.EmployeeId = int.Parse(i);
                    wu.IsActive = input.IsActive;
                    wu.Status = input.Status;
                    _abpWorkGroupEmployeeRepository.Insert(wu);
                }
            }


            if (!string.IsNullOrWhiteSpace(input.BerthSecsString))
            {
                string[] strarr = input.BerthSecsString.Split(',');

                foreach (var i in strarr)
                {
                    WorkGroupBerthsec wb = new WorkGroupBerthsec();

                    wb.WorkGroupId = workGroupId;
                    wb.BerthsecId = int.Parse(i);
                    wb.IsActive = input.IsActive;
                    wb.Status = input.Status;
                    _abpWorkGroupBerthsecRepository.Insert(wb);
                }
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Update(CreateOrUpdateWorkGroupInput input)
        {


            var entity = _abpWorkGroupRepository.Load(input.Id);

            if (entity.WorkGroupName != input.WorkGroupName && _abpWorkGroupRepository.FirstOrDefault(dic => dic.WorkGroupName == input.WorkGroupName) != default(WorkGroup))
                return 1;
            entity.TenantId = input.TenantId;
            entity.WorkGroupName = input.WorkGroupName;
            entity.CompanyId = input.CompanyId;
            entity.IsActive = input.IsActive;
            entity.Status = input.Status;

            List<WorkGroupEmployee> userlist = _abpWorkGroupEmployeeRepository.GetAll().Where(u => u.WorkGroupId == input.Id).ToList();
            //先取出数据库中该工作组中所有泊位
            //将不包含在参数传过来的泊位段中的数据删除    

            //List<string> userStrList = input.EmployeesString == null ? null : input.EmployeesString.Split(',').ToList();//把数组转换成泛型类

            if (input.EmployeesString != null)
            {
                List<string> userStrList = input.EmployeesString.Split(',').ToList();//把数组转换成泛型类
                foreach (WorkGroupEmployee we in userlist)
                {
                    if (!userStrList.Contains(we.EmployeeId.ToString()))//如果数据库中的数据不在字符串参数中，删除数据库中元素
                    {
                        _abpWorkGroupEmployeeRepository.Delete(we);
                    }
                    else//如果数据库中的数据在字符串参数中，删除字符串中该数组
                    {
                        userStrList.Remove(we.EmployeeId.ToString());//利用泛型类remove掉元素
                    }
                }
                foreach (string str in userStrList)
                {
                    WorkGroupEmployee workGroupEmployee = new WorkGroupEmployee();
                    workGroupEmployee.WorkGroupId = input.Id;
                    workGroupEmployee.EmployeeId = Convert.ToInt32(str);
                    workGroupEmployee.IsActive = input.IsActive;
                    workGroupEmployee.Status = input.Status;
                    _abpWorkGroupEmployeeRepository.Insert(workGroupEmployee);
                }

            }
            else
            {
                foreach (WorkGroupEmployee we in userlist)
                {
                    _abpWorkGroupEmployeeRepository.Delete(we); 
                }
                
            }
            
            List<WorkGroupBerthsec> berthseclist = _abpWorkGroupBerthsecRepository.GetAll().Where(u => u.WorkGroupId == input.Id).ToList();
            if (input.BerthSecsString != null)
            {
                //先取出数据库中该工作组中所有泊位
                //将不包含在参数传过来的泊位段中的数据删除         
                List<string> berthsecStrList = input.BerthSecsString.Split(',').ToList();//把数组转换成泛型类

                foreach (WorkGroupBerthsec wb in berthseclist)
                {
                    if (!berthsecStrList.Contains(wb.BerthsecId.ToString()))//如果数据库中的数据不在字符串参数中，删除数据库中元素
                    {
                        _abpWorkGroupBerthsecRepository.Delete(wb);
                    }
                    else//如果数据库中的数据在字符串参数中，删除字符串中该数组
                    {
                        berthsecStrList.Remove(wb.BerthsecId.ToString());//利用泛型类remove掉元素
                    }
                }
                foreach (string str in berthsecStrList)
                {
                    WorkGroupBerthsec workgroupberthsec = new WorkGroupBerthsec();
                    workgroupberthsec.WorkGroupId = input.Id;
                    workgroupberthsec.CompanyId = input.CompanyId;
                    workgroupberthsec.BerthsecId = Convert.ToInt32(str);
                    workgroupberthsec.IsActive = input.IsActive;
                    workgroupberthsec.Status = input.Status;
                    _abpWorkGroupBerthsecRepository.Insert(workgroupberthsec);
                }
           
            }
            else
            {
                foreach (WorkGroupBerthsec wb in berthseclist)
                {
                    _abpWorkGroupBerthsecRepository.Delete(wb);
                }

            }
            return _abpWorkGroupRepository.Update(entity) == null ? 1 : 0;



            //return await _userManager.UpdateAsync(entity) == null ? 1 : 0;

        }
        /// <summary>
        /// 如果角色已分配给了用户,返回值为1，不允许删除。未给该角色分配用户，返回值为0
        /// </summary>
        /// <param name="input"></param>
        public int Delete(CreateOrUpdateWorkGroupInput input)
        {
            var entity = _abpWorkGroupRepository.Load(input.Id);
            if (_abpWorkGroupBerthsecRepository.FirstOrDefault(dic => dic.WorkGroupId == input.Id) != default(WorkGroupBerthsec) ||
                _abpWorkGroupEmployeeRepository.FirstOrDefault(dic => dic.WorkGroupId == input.Id) != default(WorkGroupEmployee))
                return 1;

            _abpWorkGroupRepository.Delete(input.Id);
            return 0;

        }
        /// <summary>
        /// 工作组Echars数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WorkGroupEcharsDto> GetWorkGroupReportToList(WorkGroupInput input) 
        {
          return  _workgroupRepository.GetWorkGroupReportToList(input);
        }
        /// <summary>
        /// 工作组JqGrid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWorkGroupReport GetWorkGroupReportJqGrid(WorkGroupInput input) 
        {
            return _workgroupRepository.GetWorkGroupReportJqGrid(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeGps(long InspectorId)
        {
            string sql = "select AbpEmployees.*, X, Y, Convert(varchar(19), Gps.CreationTime, 121) as UpLastTime, BerthsecName, AbpEquipments.Version from AbpEmployees left join (select * from AbpEquipmentGps with(nolock) where id in (select max(id) as Id from AbpEquipmentGps group by CreatorUserId)) as Gps on AbpEmployees.Id = Gps.CreatorUserId right join AbpBerthsecs on AbpEmployees.BerthsecId = AbpBerthsecs.id left join AbpEquipments on AbpEquipments.PDA = Gps.PDA where X is not null and AbpEmployees.Id in (select CheckInEmployeeId from AbpBerthsecs where Id in (select BerthsecId from AbpWorkGroupInspectorsBerthsecs where WorkGroupId in (select WorkGroupId from AbpWorkGroupInspectors where InspectorsId = " + InspectorId + " and IsDeleted = 0) and IsDeleted = 0) and UseStatus = 1)";
            List<EmployeeDto> templist = Abp.DataProcessHelper.GetEntityFromTable<EmployeeDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
            List<EmployeeDto> list = new List<EmployeeDto>();
            foreach (var model in templist)
            {
                model.EmployeeCharges = GetEmployeeChargesDto(model.Id);
                list.Add(model);
            }
            return list;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        public List<BerthsecDto> GetBerthsecGps(long InspectorId)
        {
            string sql = "select BerthsecTotal.BerthCount, AbpBerthsecs.*, AbpEmployees.TrueName as CheckInName, AbpEmployeesOut.TrueName as CheckOutName from AbpBerthsecs left join AbpEmployees on AbpEmployees.Id = AbpBerthsecs.CheckInEmployeeId left join AbpEmployees as AbpEmployeesOut on AbpEmployeesOut.Id = CheckOutEmployeeId left join (select BerthsecId, convert(varchar(5), sum(case when BerthStatus = 1 then 1 else 0 end))+'/'+ convert(varchar(5),count(1)) as BerthCount from AbpBerths group by BerthsecId) as BerthsecTotal on AbpBerthsecs.Id = BerthsecTotal.BerthsecId where AbpBerthsecs.Id in (select BerthsecId from AbpWorkGroupInspectorsBerthsecs where WorkGroupId in (select WorkGroupId from AbpWorkGroupInspectors where InspectorsId = " + InspectorId + " and IsDeleted = 0) and IsDeleted = 0)";
            return Abp.DataProcessHelper.GetEntityFromTable<BerthsecDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        
        public List<BerthDto> GetBerthList(int BerthsecId)
        {
            string sql = "select * from AbpBerths where BerthsecId = " + BerthsecId;
            return Abp.DataProcessHelper.GetEntityFromTable<BerthDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeesId"></param>
        /// <returns></returns>
        public EmployeeChargesDto GetEmployeeChargesDto(long EmployeesId)
        {
            string sql = "select isnull(InOperaId, ChargeOperaId) as InOperaId, isnull(XJSumFactReceive, 0) as XJSumFactReceive, isnull(SKSumFactReceive, 0) as SKSumFactReceive, isnull(SumMoney, 0) as SumMoney, isnull(SumArrearage, 0) as SumArrearage, isnull(XJSumRepayment, 0) as XJSumRepayment, isnull(SKSumRepayment, 0) as SKSumRepayment, isnull(SumFactReceive, 0) as SumFactReceive, RowNumber, isnull(ChargeOperaName, bujiaoName) as ChargeOperaName, chuchangName, bujiaoName, isnull(UserName, bujiaoUserName) as UserName, (isnull(XJSumFactReceive, 0) + isnull(SKSumFactReceive, 0) + isnull(XJSumRepayment, 0) + isnull(SKSumRepayment, 0) + isnull(CardMoney, 0)) as SumIncomePlusBack, CardMoney from(select isnull(A.xj, 0) + isnull(B.xj, 0) as XJSumFactReceive, isnull(A.sk, 0) + isnull(B.sk, 0) as SKSumFactReceive, isnull(A.xj, 0) + isnull(B.xj, 0) + isnull(A.sk, 0) + isnull(B.sk, 0) + isnull(Arrearage, 0) as SumMoney, isnull(B.Arrearage, 0) AS SumArrearage, InOperaId, isnull(de.SJ_Repayment, 0) AS XJSumRepayment, isnull(de.SK_Repayment, 0) AS SKSumRepayment, (isnull(A.xj, 0) + isnull(B.xj, 0) + isnull(A.sk, 0) + isnull(B.sk, 0)) as SumFactReceive, B.ChargeOperaId, ROW_NUMBER() OVER(Order by A.InOperaId DESC) AS RowNumber, isnull(case when AE.isdeleted = 1 then AE.truename + '(该收费员已删除)' else AE.truename end, AR.truename) as ChargeOperaName, AR.truename as chuchangName, AT.truename as bujiaoName, isnull(AE.UserName, AR.UserName) as UserName, AT.UserName as bujiaoUserName, isnull(dr.CardMoney, 0) as CardMoney  from( select sum( case when PrepaidPayStatus = 1 then Prepaid else 0 end) xj, sum( case when PrepaidPayStatus = 2 then Prepaid else 0 end) sk, InOperaId from AbpBusinessDetail with(nolock) where CarInTime > @beginTime and CarInTime < @endTime  and TenantId = 1  and isdeleted = 0  and CompanyId = 35 group by InOperaId) AS A full join(select sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj, sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, sum(Money) as money, sum(Arrearage) as Arrearage, ChargeOperaId from AbpBusinessDetail with(nolock) where CarpayTime > @beginTimeC and CarpayTime < @endTimeD  and TenantId = 1  and CompanyId = 35 and isdeleted = 0  group by ChargeOperaId) AS B on A.InOperaId = B.ChargeOperaId full join(select sum(case when EscapePayStatus = 1 then Repayment else 0 end) as SJ_Repayment, sum(case when EscapePayStatus = 2 then Repayment else 0 end) as SK_Repayment, escapeoperaid from AbpBusinessDetail where CarRepaymentTime > @beginTime and CarRepaymentTime < @endTime  and TenantId = 1  and CompanyId = 35   group by escapeoperaid) as de on A.InOperaId = de.escapeoperaid full join(select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0  and TenantId = 1  and InTime > @beginTime and InTime < @endTime group by EmployeeId) AS dr on A.InOperaId = dr.EmployeeId left join AbpEmployees as AE on A.InOperaId = AE.id left join AbpEmployees as AR on B.ChargeOperaId = AR.id  left join AbpEmployees as AT on de.escapeoperaid = AT.id) as b where inOperaid = @inOperaid";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@beginTime", DateTime.Now.ToString("yyyy-MM-dd 00:00:00")),
                new SqlParameter("@beginTimeC", DateTime.Now.ToString("yyyy-MM-dd 00:00:00")),
                new SqlParameter("@endTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@endTimeD", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@inOperaid", EmployeesId)
            };

            List<EmployeeChargesDto> list = Abp.DataProcessHelper.GetEntityFromTable<EmployeeChargesDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql, param).Tables[0]);
            if (list.Count > 0)
                return list[0];
            else
                return new EmployeeChargesDto() { XJSumFactReceive = 0, SumFactReceive = 0, SKSumFactReceive = 0, SumArrearage = 0, SumMoney = 0, CardMoney = 0, XJSumRepayment = 0, SKSumRepayment = 0, SumIncomePlusBack = 0 };
        }
    }
}
