using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.SensorBusinessDetails;
using P4.SensorBusinessDetails.Dtos;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SensorBusinessDetailController : P4ControllerBase
    {
        #region Var
        private readonly ISensorBusinessDetailAppService _sensorBusinessDetailApplicationService;
        #endregion

        public SensorBusinessDetailController(ISensorBusinessDetailAppService sensorBusinessDetailApplicationService)
        {
            _sensorBusinessDetailApplicationService = sensorBusinessDetailApplicationService;
        }

        // GET: SensorBusinessDetail
        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize("InspectionStopData")]
        public ActionResult SensorBusinessDetails()
        {
            return View();
        }

        public JsonResult GetSensorBusinessDetails(SensorBusinessDetailInput input)
        {
            return this.Json(_sensorBusinessDetailApplicationService.GetAllSensorBusinessDetaillist(input));
        }

        public JsonResult ProcessSensorBusinessDetail(CreatOrUpdateSensorBusinessDetailInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridDelete:
                    SensorBusinessDetailDelete(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }
        [AbpMvcAuthorize("InspectionStopData.Delete")]
        //[AbpMvcAuthorize("SensorBusinessDetail.Delete")]
        public void SensorBusinessDetailDelete(CreatOrUpdateSensorBusinessDetailInput input)
        {
            _sensorBusinessDetailApplicationService.Delete(input.Id);
        }

    }
}