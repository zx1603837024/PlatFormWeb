using Abp.Domain.Repositories;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Tenants;
using P4.WorkGroups;
using P4.WorkGroups.Dto;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class WorkGroupsController : P4ControllerBase
    {
        #region Var
        private readonly IWorkGroupAppService _workgroupAppService;
        private readonly IRepository<WorkGroupBerthsec> _abpWorkGroupBerthsecRepository;
        private readonly IRepository<WorkGroupEmployee> _abpWorkGroupEmployeeRepository;
        private readonly ITenantAppService _tenantApplicationService;
        #endregion

        public WorkGroupsController(IWorkGroupAppService workgroupAppService, IRepository<WorkGroupBerthsec> abpWorkGroupBerthsecRepository,
            IRepository<WorkGroupEmployee> abpWorkGroupEmployeeRepository, ITenantAppService tenantApplicationService)
        {
            _workgroupAppService = workgroupAppService;
            _abpWorkGroupBerthsecRepository = abpWorkGroupBerthsecRepository;
            _abpWorkGroupEmployeeRepository = abpWorkGroupEmployeeRepository;
            _tenantApplicationService = tenantApplicationService;

        }

        [AbpMvcAuthorize("WorkgroupManagement")]
        public ActionResult WorkGroup()
        {

            return View();
        }
        /// <summary>
        /// 根据WorkGroupID获取未分配工作组的收费员和泊位段以及已分配给该工作组的收费员和泊位段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetWorkGroupById(string id,int workStatus)
        {
            //List<EmployeeDto> userlist= _workgroupAppService.GetWorkGroupEmployeeList(int.Parse(id)); 
            int workgroupId = 0;
            int.TryParse(id, out workgroupId);
            WorkGroupDto workgroupdto = _workgroupAppService.GetWorkGroupList(workgroupId,workStatus);
            return this.Json(workgroupdto);
        }
        public ActionResult WorkGroupEmployee()
        {
            return View();
        }
        public ActionResult WorkGroupBerthSec()
        {
            return View();
        }
        public JsonResult GetAllWorkGroupList(WorkGroupInput entity)
        {
            var model = _workgroupAppService.GetAllWorkGroupList(entity);

            return this.Json(_workgroupAppService.GetAllWorkGroupList(entity));
        }

        //public string SubGrid()
        //{
        //    string users = ""; string berthsecs = "";
        //    int Id = int.Parse(Request.Params["Id"]);
        //    List<User> userlist = _abpWorkGroupEmployeeRepository.GetAll().Where(u => u.WorkGroupId == Id).Select(entity => entity.User).ToList();
        //    List<Berthsec> berthseclist = _abpWorkGroupBerthsecRepository.GetAll().Where(u => u.WorkGroupId == Id).Select(entity => entity.Berthsec).ToList();
        //    foreach (User user in userlist)
        //    {
        //        users += user.UserName + ",";
        //    }
        //    foreach (Berthsec berthsec in berthseclist)
        //    {
        //        berthsecs += berthsec.BerthsecName + ",";
        //    }
        //    string jsondata = "{" + " \"page\":\"1\"," + " \"total\":1," + " \"records\":\"1\"," + " \"rows\":[" + " {" + " \"id\":\"1\"," + " \"cell\":[\"1\",\"" + users.Substring(0, users.Length - 1) + "\",\"" + berthsecs.Substring(0, berthsecs.Length - 1) + "\"]" + " }" + " ]" + " }";
        //    return jsondata;
        //}
        public JsonResult ProcessWorkGroup(CreateOrUpdateWorkGroupInput input)
        {          
            switch (input.oper)
            {
                case "del":
                    return DeteleWorkGroup(input);
                case "modify":
                    return ModifyWorkGroup(input);
                case "insert":
                    return InsertWorkGroup(input);
                default:
                    return this.Json(new MvcAjaxResponse(new ErrorInfo("操作有误")), JsonRequestBehavior.AllowGet);
                   
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult DeteleWorkGroup(CreateOrUpdateWorkGroupInput input)
        {
            if (_workgroupAppService.Delete(input) == 1)
                 return this.Json(new MvcAjaxResponse(new ErrorInfo("该工作组已分配了用户或者泊位段，请先解除绑定！")));
            else
                return this.Json(new MvcAjaxResponse(true), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult InsertWorkGroup(CreateOrUpdateWorkGroupInput input)
        {
            if (_workgroupAppService.Insert(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("工作组名称重复！")), JsonRequestBehavior.AllowGet);
            else if (_workgroupAppService.Insert(input) == 2)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("工作组名称不能为空！")), JsonRequestBehavior.AllowGet);
            else
                return this.Json(new MvcAjaxResponse(true), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ModifyWorkGroup(CreateOrUpdateWorkGroupInput input)
        {

            if (_workgroupAppService.Update(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("工作组名称重复")), JsonRequestBehavior.AllowGet); 
            else
                return this.Json(new MvcAjaxResponse(true), JsonRequestBehavior.AllowGet);
  
        }
    }
}