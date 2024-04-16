using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.MonthlyCars;
using P4.MonthlyCars.Dtos;
using P4.Parks;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 包月车辆
    /// </summary>
    [AbpMvcAuthorize]
    public class MonthlyCarsController : P4ControllerBase
    {
        #region Var
        private readonly IMonthlyCarAppService _monthlyCarAppService;
        private readonly IParkAppService _parkAppService;
        #endregion 

        public MonthlyCarsController(IMonthlyCarAppService monthlyCarAppService, IParkAppService parkAppService)
        {
            _monthlyCarAppService = monthlyCarAppService;
            _parkAppService = parkAppService;
        }

        [AbpMvcAuthorize("MonthCars")]
        public ActionResult MonthlyCar()
        {
            return View();
        }


        [AbpMvcAuthorize("MonthAbnormaLCar")]
        public ActionResult MonthlyCarAbnormal()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAllParkName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");     
            foreach (var model in _parkAppService.GetParkAll())
            {   if(AbpSession.ParkIds.Exists(e=>e==model.Id))
                strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAllParkNameNoAuthority()
        {
            StringBuilder strtemp = new StringBuilder("<select>");

            strtemp.AppendFormat(option, "0", "不限停车场");

            foreach (var model in _parkAppService.GetParkAll())
            {
                strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllMonthlyCarAbnormalList(MonthlyCarAbnormalInput input)
        {
            return this.Json(_monthlyCarAppService.GetMonthlyCarAbnormalList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessMonthlyCarAbnormal(CreateOrUpdateMonthlyCarAbnormalInput input)
        {
            switch (input.oper)
            {
                case P4Consts.JqGridUpdate:
                    _monthlyCarAppService.Update(input);
                    break;
                case P4Consts.JqGridDelete:
                    _monthlyCarAppService.Delete(input.Id);
                    break;
            }
            return this.Json(new MvcAjaxResponse(true));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllMonthlyCarList(MonthlyCarInput input)
        {
            if (input.filters != null && input.filters.Contains("ParkName"))
            {
                input.filters = input.filters.Replace("ParkName", "ParkIds");
            }
            if (input.sidx == "ParkName")
            {
                input.sidx = "ParkIds";
            }
            return this.Json(_monthlyCarAppService.GetMonthlyCarList(input));
        }

        /// <summary>
        ///  获取包月卡数据（单条）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MonthCars.Field2")]
        public JsonResult GetMonthCarModel(int Id)
        {
            return Json(_monthlyCarAppService.GetModel(Id));
        }

        /// <summary>
        /// 包月卡续费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MonthCars.Field2")]
        public JsonResult MonthRenew(CreateOrUpdateMonthlyCarInput input)
        {
            return Json(_monthlyCarAppService.MonthRenew(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult> ProcessMonthlyCar(CreateOrUpdateMonthlyCarInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = await InsertMonthlyCar(input);
                    break;
                case P4Consts.JqGridDelete:
                    DeleteMonthlyCar(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = await UpdateMonthlyCar(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
            //return this.Json(new MvcAjaxResponse(new ErrorInfo("发生异常")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MonthCars" + P4Consts.OperationInsert)]
        public async Task<string> InsertMonthlyCar(CreateOrUpdateMonthlyCarInput input)
        {
            return await _monthlyCarAppService.Insert(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("MonthCars" + P4Consts.OperationDelete)]
        public void DeleteMonthlyCar(CreateOrUpdateMonthlyCarInput input)
        {
            _monthlyCarAppService.Delete(input.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MonthCars" + P4Consts.OperationModify)]
        public async Task<string> UpdateMonthlyCar(CreateOrUpdateMonthlyCarInput input)
        {
           return await _monthlyCarAppService.Update(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllMonthlyCarHistory(MonthlyCarHistoryInput input)
        {
            return this.Json(_monthlyCarAppService.GetMonthlyCarHistoryList(input), JsonRequestBehavior.AllowGet);
        }
    }
}