using Abp.Configuration;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Companys;
using P4.Dictionarys;
using P4.Employees;
using P4.Equipments;
using P4.Equipments.Dtos;
using P4.EquipmentVersion;
using P4.EquipmentVersion.Dtos;
using P4.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 设备管理
    /// </summary>
    [AbpMvcAuthorize]
    public class EquipmentsController : P4ControllerBase
    {
        #region Var
        private readonly IEquipmentAppService _equipmentsApplicationService;
        private readonly ICompanyAppService _companyApplicationService;
        private readonly IEmployeeAppService _employeeApplicationService;
        private readonly IDictionarysAppService _dictionarysApplicationService;
        private readonly ISettingStore _settingStore;
        private readonly IEquipmentVersionAppService _equipmentVersionAppService;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="equipmentApplicationService"></param>
        /// <param name="companyApplicationService"></param>
        /// <param name="employeeApplicationService"></param>
        /// <param name="dictionarysApplicationService"></param>
        public EquipmentsController(IEquipmentAppService equipmentApplicationService, ICompanyAppService companyApplicationService, IEmployeeAppService employeeApplicationService, IDictionarysAppService dictionarysApplicationService, ISettingStore settingStore, IEquipmentVersionAppService equipmentVersionAppService)
        {
            _equipmentsApplicationService = equipmentApplicationService;
            _companyApplicationService = companyApplicationService;
            _employeeApplicationService = employeeApplicationService;
            _dictionarysApplicationService = dictionarysApplicationService;
            _settingStore = settingStore;
            _equipmentVersionAppService = equipmentVersionAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EquipmentVersion")]
        public ActionResult EquipmentVersion()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetEquipmentVersionList(SearchEquipmentVersionInput input)
        {
            return this.Json(_equipmentVersionAppService.GetAll(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessEquipmentVersion(CreateOrUpdateEquipmentVersionInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = _equipmentVersionAppService.Insert(input);
                    break;
                case P4Consts.JqGridDelete:
                    info.Message = _equipmentVersionAppService.Delete(input.Id);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = _equipmentVersionAppService.Update(input);
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("PDAEquipment")]
        public ActionResult Equipments()
        {
            return View();
        }

        /// <summary>
        /// PDA配置
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("PDAConfigure")]
        public async Task<ActionResult> PDAConfigure()
        {
            PDAConfigModel model = new PDAConfigModel();
            
            var settings = await _settingStore.GetAllListAsync(AbpSession.TenantId.Value, null);
            model.PDAChar = settings.FirstOrDefault(entity => entity.Name == "PDAChar").Value;
            model.PDAInCarEscape = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAInCarEscape").Value);
            model.PDAInCarPhotoFlag = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAInCarPhotoFlag").Value);
            model.PDAInCarPhotoNum = settings.FirstOrDefault(entity => entity.Name == "PDAInCarPhotoNum").Value;
            model.PDAOutCarEscape = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAOutCarEscape").Value);
            model.PDAOutCarPhotoFlag = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAOutCarPhotoFlag").Value);
            model.PDAOutCarPhotoNum = settings.FirstOrDefault(entity => entity.Name == "PDAOutCarPhotoNum").Value;
            model.PDABindUser = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDABindUser").Value);      //pda是否绑定收费员
            model.PDARegion = settings.FirstOrDefault(entity => entity.Name == "PDARegion").Value;
            model.Password1 = settings.FirstOrDefault(entity => entity.Name == "PDAPassword1").Value;
            model.Password2 = settings.FirstOrDefault(entity => entity.Name == "PDAPassword2").Value;
            model.Password3 = settings.FirstOrDefault(entity => entity.Name == "PDAPassword3").Value;
            model.Password4 = settings.FirstOrDefault(entity => entity.Name == "PDAPassword4").Value;
            model.Password5 = settings.FirstOrDefault(entity => entity.Name == "PDAPassword5").Value;
            model.PDAPrepaidFlag = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAPrepaidFlag").Value);
            model.PDAPrepaid = settings.FirstOrDefault(entity => entity.Name == "PDAPrepaid").Value;                    //预缴金额
            model.PDAUpgrate = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAUpgrate").Value);        //PDA是否自动升级

            model.IPassCardPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "IPassCardPay").Value);
            model.IPassCardDiscount = settings.FirstOrDefault(entity => entity.Name == "IPassCardDiscount").Value;

            model.CashPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "CashPay").Value);
            model.CashPayDiscount = settings.FirstOrDefault(entity => entity.Name == "CashPayDiscount").Value;

            model.WeixinPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "WeixinPay").Value);
            //model.CCBAggregatePay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "CCBAggregatePay").Value);//建行聚合码
            model.WeixinDiscount = settings.FirstOrDefault(entity => entity.Name == "WeixinDiscount").Value;
            model.AliPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "AliPay").Value);
            model.VideoRecognition = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "VideoRecognition").Value);
            model.AccountPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "AccountPay").Value);
            model.AccountDiscount = settings.FirstOrDefault(entity => entity.Name == "AccountDiscount").Value;

            model.EscapePrint = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapePrint").Value);//出场逃逸是否打印小票

            model.EscapeXingCode = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapeXingCode").Value);//出场逃逸是否打印二维码

            model.PeriodPaid = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PeriodPaid").Value);        //是否启用时间预缴
            model.PeriodTime = settings.FirstOrDefault(entity => entity.Name == "PeriodTime").Value;                    //预缴时段时间
            model.PeriodTime1 = settings.FirstOrDefault(entity => entity.Name == "PeriodTime1").Value;                    //预缴时段时间1

            model.SensorMorningBegin = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorMorningBegin").Value);
            model.SensorMorningDelay = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorMorningDelay").Value);
            model.SensorMorningEnd = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorMorningEnd").Value);
            model.SensorNightBegin = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorNightBegin").Value);
            model.SensorNightDelay = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorNightDelay").Value);
            model.SensorNightEnd = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorNightEnd").Value);

            model.PDASyncData = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDASyncData").Value);
            model.ElectronicFence = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "ElectronicFence").Value);
            model.ElectronicFenceOffset = settings.FirstOrDefault(entity => entity.Name == "ElectronicFenceOffset").Value;
            model.PrivilegeCarReceipt = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PrivilegeCarReceipt").Value);
            model.OnlinePayUrl= settings.FirstOrDefault(entity => entity.Name == "OnlinePayUrl").Value;
            return View(model);
        }

        /// <summary>
        /// 保存PDA配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("PDAConfigure")]
        public async Task<JsonResult> SavePDAConfigure(PDAConfigModel entity)
        {
            SettingInfo model = new SettingInfo();
            model.TenantId = AbpSession.TenantId.Value;
            model.Name = "PDAChar";
            model.Value = entity.PDAChar;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAInCarEscape";
            model.Value = entity.PDAInCarEscape.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAInCarPhotoFlag";
            model.Value = entity.PDAInCarPhotoFlag.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAInCarPhotoNum";
            model.Value = entity.PDAInCarPhotoNum;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAOutCarEscape";
            model.Value = entity.PDAOutCarEscape.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAOutCarPhotoFlag";
            model.Value = entity.PDAOutCarPhotoFlag.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAOutCarPhotoNum";
            model.Value = entity.PDAOutCarPhotoNum;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDARegion";
            model.Value = entity.PDARegion;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAPassword1";
            model.Value = entity.Password1;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAPassword2";
            model.Value = entity.Password2;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAPassword3";
            model.Value = entity.Password3;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAPassword4";
            model.Value = entity.Password4;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAPassword5";
            model.Value = entity.Password5;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAPrepaid";
            model.Value = entity.PDAPrepaid;
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAPrepaidFlag";
            model.Value = entity.PDAPrepaidFlag.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PDABindUser";
            model.Value = entity.PDABindUser.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PDAUpgrate";
            model.Value = entity.PDAUpgrate.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "Sensorbind";
            model.Value = entity.Sensorbind.ToString();
            await _settingStore.UpdateAsync(model);


            model.Name = "SensorTimer";
            model.Value = entity.SensorTimer.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "WeixinPay";
            model.Value = entity.WeixinPay.ToString();
            await _settingStore.UpdateAsync(model);

            ///
            model.Name = "CCBAggregatePay";
            model.Value = entity.CCBAggregatePay.ToString();
            await _settingStore.UpdateAsync(model);
            ///

            model.Name = "AliPay";
            model.Value = entity.AliPay.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "WeixinDiscount";
            model.Value = entity.WeixinDiscount.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "IPassCardPay";
            model.Value = entity.IPassCardPay.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "IPassCardDiscount";
            model.Value = entity.IPassCardDiscount.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "CashPay";
            model.Value = entity.CashPay.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "CashPayDiscount";
            model.Value = entity.CashPayDiscount.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "AccountPay";
            model.Value = entity.AccountPay.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "AccountDiscount";
            model.Value = entity.AccountDiscount.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "EscapeXingCode";
            model.Value = entity.EscapeXingCode.ToString();
            await _settingStore.UpdateAsync(model);

            //
            model.Name = "EscapePrint";
            model.Value = entity.EscapePrint.ToString();
            await _settingStore.UpdateAsync(model);

            //
            model.Name = "PeriodPaid";
            model.Value = entity.PeriodPaid.ToString();
            await _settingStore.UpdateAsync(model);

            //
            model.Name = "PeriodTime";
            model.Value = entity.PeriodTime.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PeriodTime1";
            model.Value = entity.PeriodTime1.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "VideoRecognition";
            model.Value = entity.VideoRecognition.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "SensorMorningDelay";
            model.Value = entity.SensorMorningDelay.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "SensorMorningBegin";
            model.Value = entity.SensorMorningBegin.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "SensorMorningEnd";
            model.Value = entity.SensorMorningEnd.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "SensorNightDelay";
            model.Value = entity.SensorNightDelay.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "SensorNightBegin";
            model.Value = entity.SensorNightBegin.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "SensorNightEnd";
            model.Value = entity.SensorNightEnd.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PDASyncData";
            model.Value = entity.PDASyncData.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "ElectronicFence";
            model.Value = entity.ElectronicFence.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "ElectronicFenceOffset";
            model.Value = entity.ElectronicFenceOffset.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "PrivilegeCarReceipt";
            model.Value = entity.PrivilegeCarReceipt.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "OnlinePayUrl";
            model.Value = entity.OnlinePayUrl.ToString();
            await _settingStore.UpdateAsync(model);

            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 获取pda设备列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetEquipments(EquipmentInput input)
        {
            return this.Json(_equipmentsApplicationService.GetAllEquipmentOutputList(input));
        }

        /// <summary>
        /// 获取分公司作下拉使用
        /// </summary>
        /// <returns></returns>
        public string GetAllCompanyName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            if (!AbpSession.CompanyId.HasValue)
            {
                strtemp.AppendLine(emptyoption);
            }
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
        /// 获取收费员名称
        /// </summary>
        /// <returns></returns>
        public string GetEmployeeName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendLine(emptyoption);
            foreach (var model in _employeeApplicationService.GetEmployeeAll())
            {

                strtemp.AppendFormat(option, model.Id, model.TrueName + "(" + model.UserName + ")");
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取设备类型
        /// </summary>
        /// <returns></returns>
        public string GetEquipmentType()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10013"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取设备制式
        /// </summary>
        /// <returns></returns>
        public string GetEquipmentStandard()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10014"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取使用状态
        /// </summary>
        /// <returns></returns>
        public string GetUseStatus()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10015"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// pda数据增删改操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessEquipment(CreateOrUpdateEquipmentInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = InsertEquipment(input);
                    break;
                case P4Consts.JqGridDelete:
                    DeleteEquipment(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = ModifyEquipment(input);
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("PDAEquipment.Insert")]
        public string InsertEquipment(CreateOrUpdateEquipmentInput input)
        {
            return _equipmentsApplicationService.Insert(input);
        }

        [AbpMvcAuthorize("PDAEquipment.Update")]
        public string ModifyEquipment(CreateOrUpdateEquipmentInput input)
        {
            
            return _equipmentsApplicationService.Modify(input);
        }

        [AbpMvcAuthorize("PDAEquipment.Delete")]
        public void DeleteEquipment(CreateOrUpdateEquipmentInput input)
        {
            _equipmentsApplicationService.Delete(input.Id);
        }

        [AbpMvcAuthorize("PDAMaintenance")]
        public ActionResult EquipmentMaintenances()
        {
            return View();
        }

        /// <summary>
        /// 获取pda维护列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetEquipmentMaintenances(EquipmentMaintenanceInput input)
        {
            return this.Json(_equipmentsApplicationService.GetAllEquipmentMaintenanceOutputList(input));
        }

        /// <summary>
        /// pda维护列表删除操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessEquipmentMaintenance(CreateOrUpdateEquipmentMaintenanceInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridDelete:
                    EquipmentMaintenanceDelete(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("PDAMaintenance.Delete")]
        public void EquipmentMaintenanceDelete(CreateOrUpdateEquipmentMaintenanceInput input)
        {
            _equipmentsApplicationService.DeleteEquipmentMaintenance(input.Id);
        }

        /// <summary>
        /// 获取使用状态饼状图显示数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEquipmentNumGroupByUseStatus()
        {


            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<SeriesPie> seriesList = new List<SeriesPie>();

            SeriesPie seriesObj = new SeriesPie();
            seriesObj.type = "pie";
            seriesObj.name = "PDA设备使用状态";
            seriesObj.radius = "55%";
            seriesObj.data = new List<SeriesPie.EqModel>(); //先初始化 不初始化后面直直接data.Add(x)会报错
            
            List<EquipmentDto> equipmentlist = _equipmentsApplicationService.GetEquipmentNumGroupByUseStatus();

            for (var i = 0; i < equipmentlist.Count; i++)
            {
                SeriesPie.EqModel emodel = new SeriesPie.EqModel();
                emodel.value = equipmentlist[i].UseNum;
                emodel.name = equipmentlist[i].UseStatus;
                seriesObj.data.Add(emodel);
            }

            seriesList.Add(seriesObj);
            
            var newObj = new
            {
                series = seriesList
            };
            return this.Json(newObj);
            //return this.Json(_equipmentsApplicationService.GetEquipmentNumGroupByUseStatus());
        }

        public void CargoInfoExport()
        {
            _equipmentsApplicationService.CargoInfoExport();
        }

    }
}