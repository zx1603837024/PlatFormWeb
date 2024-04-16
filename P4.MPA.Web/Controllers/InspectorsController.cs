using Abp.Domain.Repositories;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Companys;
using P4.Dictionarys;
using P4.InspectorCharges;
using P4.InspectorCharges.Dtos;
using P4.Inspectors;
using P4.Inspectors.Dtos;
using P4.Parks;
using P4.Web.Models;
using P4.WorkGroupInspectors;
using P4.WorkGroupInspectors.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class InspectorsController : P4ControllerBase
    {
        // GET: Inspectors
        #region var
        private readonly IDictionarysAppService _dictionarysApplicationService;
        private readonly IInspectorAppService _InspectorApplicationService;
        private readonly ICompanyAppService _companyApplicationService;
        private readonly IParkAppService _parkApplicationService;

        private readonly IWorkGroupInspectorsAppService _workgroupAppService;
        private readonly IRepository<WorkGroupInspectorsBerthsecs> _abpWorkGroupBerthsecRepository;
        private readonly IRepository<WorkGroupInspectors.WorkGroupInspectors> _abpWorkGroupInspectorsRepository;

        private readonly IInspectorChargesAppService _inspectorChargesAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionarysApplicationService"></param>
        /// <param name="InspectorApplicationService"></param>
        /// <param name="companyApplicationService"></param>
        /// <param name="parkApplicationService"></param>
        /// <param name="workgroupAppService"></param>
        /// <param name="abpWorkGroupBerthsecRepository"></param>
        /// <param name="abpWorkGroupInspectorsRepository"></param>
        public InspectorsController(IDictionarysAppService dictionarysApplicationService, IInspectorAppService InspectorApplicationService, ICompanyAppService companyApplicationService, IParkAppService parkApplicationService, IWorkGroupInspectorsAppService workgroupAppService, IRepository<WorkGroupInspectorsBerthsecs> abpWorkGroupBerthsecRepository, IRepository<WorkGroupInspectors.WorkGroupInspectors> abpWorkGroupInspectorsRepository, IInspectorChargesAppService inspectorChargesAppService)
        {
            _dictionarysApplicationService = dictionarysApplicationService;
            _InspectorApplicationService = InspectorApplicationService;
            _companyApplicationService = companyApplicationService;
            _parkApplicationService = parkApplicationService;

            _workgroupAppService = workgroupAppService;
            _abpWorkGroupBerthsecRepository = abpWorkGroupBerthsecRepository;
            _abpWorkGroupInspectorsRepository = abpWorkGroupInspectorsRepository;

            _inspectorChargesAppService = inspectorChargesAppService;
        }
        [AbpMvcAuthorize("InspectorManagement")]
        public ActionResult InspectorManagement()
        {
            return View();
        }
        public string GetAllAccountStaus()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10005"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
        public JsonResult GetAllInspectorList(InspectorInput entity)
        {
            return this.Json(_InspectorApplicationService.GetAllInspectorList(entity));

        }
        public JsonResult ProcessInspector(CreatOrUpdateInspector input)
        {
            JsonResult returnJson = new JsonResult();
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = InspectorInsert(input);
                    break;
                case P4Consts.JqGridDelete:
                    returnJson = InspectorDelete(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = InspectorEdit(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }
        [AbpMvcAuthorize("InspectorManagement.Insert")]
        public string InspectorInsert(CreatOrUpdateInspector input)
        {
            return _InspectorApplicationService.InspectorInsert(input);

        }

        [AbpMvcAuthorize("InspectorManagement.Delete")]
        public JsonResult InspectorDelete(CreatOrUpdateInspector input)
        {
            _InspectorApplicationService.InspectorDelete(input);
            return this.Json(new MvcAjaxResponse());
        }
        [AbpMvcAuthorize("InspectorManagement.Update")]
        public string InspectorEdit(CreatOrUpdateInspector input)
        {
            return _InspectorApplicationService.InspectorUpdate(input);
        }

        [AbpMvcAuthorize("InspectorTasks")]
        public ActionResult InspectorTasks()
        {
            return View();
        }

        /// <summary>
        /// 排班管理
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("ScheduleManage")]
        public ActionResult ScheduleManage()
        {
            InspectorModel model = new InspectorModel();
            model.Inspectors = _InspectorApplicationService.GetInspectorAll();
            return View(model);
        }

        /// <summary>
        /// 获取巡查任务列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult GetAllInspectorTasksList(InspectorTasksInput entity)
        {
            return this.Json(_InspectorApplicationService.GetAllInspectorTasksList(entity));

        }

        /// <summary>
        /// 获取分公司作下拉使用
        /// </summary>
        /// <returns></returns>
        public string GetAllCompanyName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            //if (!AbpSession.CompanyId.HasValue)
            //{
            //    strtemp.AppendLine(emptyoption);
            //}
            foreach (var model in _companyApplicationService.GetAllCompanyName())
            {
                if (AbpSession.CompanyId.HasValue)
                {
                    if (model.Id == AbpSession.CompanyId)
                    {
                        strtemp.AppendFormat(option, model.Id, model.CompanyName);
                        break;
                    }
                    continue;
                }
                strtemp.AppendFormat(option, model.Id, model.CompanyName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取停车场
        /// </summary>
        /// <returns></returns>
        public string GetAllParkName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _parkApplicationService.GetParkAll())
            {
                if (AbpSession.ParkIds.Exists(e => e == model.Id))
                    strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取巡查员
        /// </summary>
        /// <returns></returns>
        public string GetInspectorName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _InspectorApplicationService.GetInspectorAll())
            {
                strtemp.AppendFormat(option, model.Id, model.TrueName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取任务状态
        /// </summary>
        /// <returns></returns>
        public string GetInspectorTaskStatus()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10016"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 巡查任务增删改操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessInspectorTask(CreatOrUpdateInspectorTasks input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = InsertInspectorTask(input);
                    break;
                case P4Consts.JqGridDelete:
                    DeleteInspectorTask(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = ModifyInspectorTask(input);
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("InspectorTasks.Insert")]
        public string InsertInspectorTask(CreatOrUpdateInspectorTasks input)
        {
            return _InspectorApplicationService.Insert(input);
        }

        [AbpMvcAuthorize("InspectorTasks.Update")]
        public string ModifyInspectorTask(CreatOrUpdateInspectorTasks input)
        {
            return _InspectorApplicationService.Modify(input);
        }

        [AbpMvcAuthorize("InspectorTasks.Delete")]
        public void DeleteInspectorTask(CreatOrUpdateInspectorTasks input)
        {
            _InspectorApplicationService.Delete(input.Id);
        }


        [AbpMvcAuthorize("InspectorTaskFeedbacks")]
        public ActionResult InspectorTaskFeedbacks()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("InspectorsReport")]
        public ActionResult InspectorsReport()
        {
            InspectorChargesModel model = new InspectorChargesModel();
            model.InspectorList = _InspectorApplicationService.GetInspectorAll();
            model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 获取巡查反馈任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllInspectorTaskFeedbacksList(InspectorTaskFeedbacksInput input)
        {
            return this.Json(_InspectorApplicationService.GetAllInspectorTaskFeedbacksList(input));
        }

        #region 巡查员工作组
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("InspectorsWorkGroup")]
        public ActionResult InspectorsWorkGroup()
        {
            return View();
        }

        /// <summary>
        /// 根据WorkGroupID获取未分配工作组的收费员和泊位段以及已分配给该工作组的收费员和泊位段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetWorkGroupById(string id)
        {
            int workgroupId = 0;
            int.TryParse(id, out workgroupId);
            WorkGroupsInspectorsDto workgroupdto = _workgroupAppService.GetWorkGroupList(workgroupId);
            return this.Json(workgroupdto);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        public JsonResult ProcessWorkGroup(CreateOrUpdateWorkGroupInspectorsInput input)
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
        public JsonResult DeteleWorkGroup(CreateOrUpdateWorkGroupInspectorsInput input)
        {
            if (_workgroupAppService.Delete(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("该工作组已分配了用户或者泊位段，请先解除绑定！")));
            else
                return this.Json(new MvcAjaxResponse(true), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult InsertWorkGroup(CreateOrUpdateWorkGroupInspectorsInput input)
        {
            if (_workgroupAppService.Insert(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("工作组名称重复！")), JsonRequestBehavior.AllowGet);
            else if (_workgroupAppService.Insert(input) == 2)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("工作组名称不能为空！")), JsonRequestBehavior.AllowGet);
            else
                return this.Json(new MvcAjaxResponse(true), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ModifyWorkGroup(CreateOrUpdateWorkGroupInspectorsInput input)
        {

            if (_workgroupAppService.Update(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("工作组名称重复")), JsonRequestBehavior.AllowGet);
            else
                return this.Json(new MvcAjaxResponse(true), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult GetAllWorkGroupList(WorkGroupInspectorsInput entity)
        {
            return this.Json(_workgroupAppService.GetAllWorkGroupList(entity));
        }

        #endregion

        #region

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("InspectorMap")]
        public ActionResult InspectorMap()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("InspectorMap")]
        public JsonResult GetInspectorByMap(int Id)
        {
            return this.Json(_InspectorApplicationService.GetAllInsperctorMap(Id));
        }

        /// <summary>
        /// 获取jqgrid数据
        /// </summary>
        /// <returns></returns>
        public JsonResult JqGridData(InspectorChargeInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            GetAllInspectorChargesOutput businessDetaillist = _inspectorChargesAppService.GetInspectorChargesDayReport(input);
            return this.Json(businessDetaillist);
        }

        /// <summary>
        /// 获取Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult EcharData(InspectorChargeInput input)
        {
            //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
            //考虑到图表的category是字符串数组 这里定义一个string的List
            List<string> categoryList = new List<string>();
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();

            //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("实收"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("未收");
            legendList.Add("现金");
            legendList.Add("刷卡");
            legendList.Add("应收");

            //定义一个Series对象
            Series seriesObj = new Series();
            //  seriesObj.id = 1;
            seriesObj.name = "实收";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            // seriesObj2.id = 2;
            seriesObj2.type = "line";
            seriesObj2.name = "未收";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            // seriesObj2.id = 2;
            seriesObj3.type = "line";
            seriesObj3.name = "现金";
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            // seriesObj2.id = 2;
            seriesObj4.type = "line";
            seriesObj4.name = "刷卡";
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            // seriesObj2.id = 2;
            seriesObj5.type = "line";
            seriesObj5.name = "应收";
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //DateTime begindt = DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");
            //string employeeId = employeeIdInput == null ? "" : employeeIdInput;
            //string employeeName = employeeNameInput == null ? "" : employeeNameInput;
            //dt = DateTime.Parse("2015-09-28 00:00:00");
            List<InspectorChargesDto> employeechargeslist = _inspectorChargesAppService.GetInspectorChargesDayReportToEchart(input);

            for (var i = 0; i < employeechargeslist.Count; i++)
            {
                categoryList.Add(employeechargeslist[i].ChargeOperaName);
                seriesObj.data.Add(employeechargeslist[i].SumFactReceive);
                seriesObj2.data.Add(employeechargeslist[i].SumArrearage);
                seriesObj3.data.Add(Convert.ToDecimal(employeechargeslist[i].XJSumFactReceive));
                seriesObj4.data.Add(Convert.ToDecimal(employeechargeslist[i].SKSumFactReceive));
                seriesObj5.data.Add(Convert.ToDecimal(employeechargeslist[i].SumMoney));
            }
            seriesList.Add(seriesObj);
            seriesList.Add(seriesObj2);
            seriesList.Add(seriesObj3);
            seriesList.Add(seriesObj4);
            seriesList.Add(seriesObj5);

            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            var newObj = new
            {
                category = categoryList,
                series = seriesList,
                legend = legendList
            };

            return this.Json(newObj);
        }
        #endregion
    }
}