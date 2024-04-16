using Abp.Web.Mvc.Authorization;
using P4.Berthsecs;
using P4.ParkCharges;
using P4.ParkCharges.Dto;
using P4.Web.Models;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class OwnReportController : P4ControllerBase
    {
        #region Var
        private readonly IParkChargesAppService _parkChargesAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkAppService"></param>
        public OwnReportController(IParkChargesAppService parkChargesAppService, IBerthsecAppService berthsecAppService)
        {
            _parkChargesAppService = parkChargesAppService;
            _berthsecAppService = berthsecAppService;
        }

        // GET: OwnReport
        [AbpMvcAuthorize("OwnReport")]
        public ActionResult OwnDynamicReport()
        {
            ParkChargesModel model = new ParkChargesModel();
            model.BerthsecList = _berthsecAppService.GetAllBerthsec();
            model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 欠费报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult BerthescOwnJqGridData(ParkChargeInput input)
        {
            GetAllParkChargesOutput businessDetaillist = _parkChargesAppService.GetBerthescOwnOutput(input);

            return Json(businessDetaillist);
        }

        /// <summary>
        /// 欠费报表详细
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult OwnReportDetail(string Id, ParkChargeInput input)
        {
           var businessDetaillist = _parkChargesAppService.OwnReportDetail(input);
            return this.Json(businessDetaillist);
        }
    }
}