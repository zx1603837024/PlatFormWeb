using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.WorkGroupInspectors.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P4.WorkGroupInspectors
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkGroupInspectorsAppService : ApplicationService, IWorkGroupInspectorsAppService
    {
        #region Var
        private readonly IRepository<WorkGroupsInspectors> _abpWorkGroupRepository;
        private readonly IRepository<WorkGroupInspectorsBerthsecs> _abpWorkGroupInspectorsBerthsecRepository;
        private readonly IRepository<WorkGroupInspectors> _abpWorkGroupInspectorsRepository;
        private readonly IWorkGroupInspectorsRepository _workgroupInspectorsRepository;
        #endregion

        /// <summary>
        /// abpWorkGroupInspectorsBerthsecRepository
        /// </summary>
        /// <param name="abpWorkGroupRepository"></param>
        /// <param name="abpWorkGroupInspectorsBerthsecRepository"></param>
        /// <param name="abpWorkGroupInspectorsRepository"></param>
        /// <param name="workgroupInspectorsRepository"></param>
        public WorkGroupInspectorsAppService(IRepository<WorkGroupsInspectors> abpWorkGroupRepository, IRepository<WorkGroupInspectorsBerthsecs> abpWorkGroupInspectorsBerthsecRepository,
            IRepository<WorkGroupInspectors> abpWorkGroupInspectorsRepository, IWorkGroupInspectorsRepository workgroupInspectorsRepository)
        {
            _abpWorkGroupRepository = abpWorkGroupRepository;
            _abpWorkGroupInspectorsBerthsecRepository = abpWorkGroupInspectorsBerthsecRepository;
            _abpWorkGroupInspectorsRepository = abpWorkGroupInspectorsRepository;
            _workgroupInspectorsRepository = workgroupInspectorsRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWorkGroupInspectorsOutput GetAllWorkGroupList(WorkGroupInspectorsInput input)
        {

            return _workgroupInspectorsRepository.GetAllWorkGroupList(input);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workgroupId"></param>
        /// <returns></returns>
        public WorkGroupsInspectorsDto GetWorkGroupList(int workgroupId)
        {
            return _workgroupInspectorsRepository.GetWorkGroupList(workgroupId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Insert(CreateOrUpdateWorkGroupInspectorsInput input)
        {
            if (_abpWorkGroupRepository.FirstOrDefault(dic => dic.WorkGroupName == input.WorkGroupName) != default(WorkGroupsInspectors))
            {
                //已存在typeCode
                return 1;
            }
            //判断工作组名称是否为空
            if(String.IsNullOrEmpty(input.WorkGroupName))
            {
                return 2;
            }

            WorkGroupsInspectors entity = new WorkGroupsInspectors();
            entity.TenantId = input.TenantId;
            entity.WorkGroupName = input.WorkGroupName;
            entity.CompanyId = input.CompanyId;
            entity.IsActive = input.IsActive;
            var workGroupId = _abpWorkGroupRepository.InsertAndGetId(entity);



            //删除用户得时候会把用户角色表中该用户的信息清空
            //删除角色时 查询是否已经为该角色分配了用户 如果分配了用户？？？？

            //如果model.RoleDisplayName不为空 更改UserRole表数据

            if (!string.IsNullOrWhiteSpace(input.EmployeesString))
            {
                string[] strarr = input.EmployeesString.Split(',');

                foreach (var i in strarr)
                {
                    WorkGroupInspectors wu = new WorkGroupInspectors();
                    wu.WorkGroupId = workGroupId;
                    wu.InspectorsId = int.Parse(i);
                    wu.IsActive = input.IsActive;
                    _abpWorkGroupInspectorsRepository.Insert(wu);
                }
            }


            if (!string.IsNullOrWhiteSpace(input.BerthSecsString))
            {
                string[] strarr = input.BerthSecsString.Split(',');

                foreach (var i in strarr)
                {
                    WorkGroupInspectorsBerthsecs wb = new WorkGroupInspectorsBerthsecs();

                    wb.WorkGroupId = workGroupId;
                    wb.BerthsecId = int.Parse(i);
                    wb.IsActive = input.IsActive;
                    _abpWorkGroupInspectorsBerthsecRepository.Insert(wb);
                }
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Update(CreateOrUpdateWorkGroupInspectorsInput input)
        {
            var entity = _abpWorkGroupRepository.Load(input.Id);
            if (entity.WorkGroupName != input.WorkGroupName && _abpWorkGroupRepository.FirstOrDefault(dic => dic.WorkGroupName == input.WorkGroupName) != default(WorkGroupsInspectors))
                return 1;
            entity.TenantId = input.TenantId;
            entity.WorkGroupName = input.WorkGroupName;
            entity.CompanyId = input.CompanyId;
            entity.IsActive = input.IsActive;
            List<WorkGroupInspectors> userlist = _abpWorkGroupInspectorsRepository.GetAll().Where(u => u.WorkGroupId == input.Id).ToList();
            //先取出数据库中该工作组中所有泊位
            //将不包含在参数传过来的泊位段中的数据删除    

            //List<string> userStrList = input.EmployeesString == null ? null : input.EmployeesString.Split(',').ToList();//把数组转换成泛型类

            if (input.EmployeesString != null)
            {
                List<string> userStrList = input.EmployeesString.Split(',').ToList();//把数组转换成泛型类
                foreach (WorkGroupInspectors we in userlist)
                {
                    if (!userStrList.Contains(we.InspectorsId.ToString()))//如果数据库中的数据不在字符串参数中，删除数据库中元素
                    {
                        _abpWorkGroupInspectorsRepository.Delete(we);
                    }
                    else//如果数据库中的数据在字符串参数中，删除字符串中该数组
                    {
                        userStrList.Remove(we.InspectorsId.ToString());//利用泛型类remove掉元素
                    }
                }
                foreach (string str in userStrList)
                {
                    WorkGroupInspectors workGroupEmployee = new WorkGroupInspectors();
                    workGroupEmployee.WorkGroupId = input.Id;
                    workGroupEmployee.InspectorsId = Convert.ToInt32(str);
                    workGroupEmployee.IsActive = input.IsActive;
                    _abpWorkGroupInspectorsRepository.Insert(workGroupEmployee);
                }

            }
            else
            {
                foreach (WorkGroupInspectors we in userlist)
                {
                    _abpWorkGroupInspectorsRepository.Delete(we); 
                }
                
            }
            
            List<WorkGroupInspectorsBerthsecs> berthseclist = _abpWorkGroupInspectorsBerthsecRepository.GetAll().Where(u => u.WorkGroupId == input.Id).ToList();
            if (input.BerthSecsString != null)
            {
                //先取出数据库中该工作组中所有泊位
                //将不包含在参数传过来的泊位段中的数据删除         
                List<string> berthsecStrList = input.BerthSecsString.Split(',').ToList();//把数组转换成泛型类

                foreach (WorkGroupInspectorsBerthsecs wb in berthseclist)
                {
                    if (!berthsecStrList.Contains(wb.BerthsecId.ToString()))//如果数据库中的数据不在字符串参数中，删除数据库中元素
                    {
                        _abpWorkGroupInspectorsBerthsecRepository.Delete(wb);
                    }
                    else//如果数据库中的数据在字符串参数中，删除字符串中该数组
                    {
                        berthsecStrList.Remove(wb.BerthsecId.ToString());//利用泛型类remove掉元素
                    }
                }
                foreach (string str in berthsecStrList)
                {
                    WorkGroupInspectorsBerthsecs workgroupberthsec = new WorkGroupInspectorsBerthsecs();
                    workgroupberthsec.WorkGroupId = input.Id;
                    workgroupberthsec.CompanyId = input.CompanyId;
                    workgroupberthsec.BerthsecId = Convert.ToInt32(str);
                    workgroupberthsec.IsActive = input.IsActive;
                    _abpWorkGroupInspectorsBerthsecRepository.Insert(workgroupberthsec);
                }
           
            }
            else
            {
                foreach (WorkGroupInspectorsBerthsecs wb in berthseclist)
                {
                    _abpWorkGroupInspectorsBerthsecRepository.Delete(wb);
                }

            }
            return _abpWorkGroupRepository.Update(entity) == null ? 1 : 0;

        }
        /// <summary>
        /// 如果角色已分配给了用户,返回值为1，不允许删除。未给该角色分配用户，返回值为0
        /// </summary>
        /// <param name="input"></param>
        public int Delete(CreateOrUpdateWorkGroupInspectorsInput input)
        {
            var entity = _abpWorkGroupRepository.Load(input.Id);
            if (_abpWorkGroupInspectorsBerthsecRepository.FirstOrDefault(dic => dic.WorkGroupId == input.Id) != default(WorkGroupInspectorsBerthsecs) ||
                _abpWorkGroupInspectorsRepository.FirstOrDefault(dic => dic.WorkGroupId == input.Id) != default(WorkGroupInspectors))
                return 1;

            _abpWorkGroupRepository.Delete(input.Id);
            return 0;

        }
        /// <summary>
        /// 工作组Echars数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WorkGroupInspectorsEcharsDto> GetWorkGroupReportToList(WorkGroupInspectorsInput input) 
        {
          return _workgroupInspectorsRepository.GetWorkGroupReportToList(input);
        }
        /// <summary>
        /// 工作组JqGrid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWorkGroupInspectorsReport GetWorkGroupReportJqGrid(WorkGroupInspectorsInput input) 
        {
            return _workgroupInspectorsRepository.GetWorkGroupReportJqGrid(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        public WorkGroupsInspectorsDto GetWorkGroupListByInspectorId(long InspectorId)
        {
            return GetWorkGroupList(_abpWorkGroupInspectorsRepository.FirstOrDefault(entity => entity.InspectorsId == InspectorId && entity.IsActive == true).WorkGroupId);
        }
    }
}
