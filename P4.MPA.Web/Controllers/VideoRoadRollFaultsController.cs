using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.VideoEquipFaultsNs;
using P4.VideoEquipFaultsNs.Dtos;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class VideoRoadRollFaultsController : P4ControllerBase
    {
        #region Var
        private readonly IVideoEquipFaultsAppService _veqFaultApp;
        #endregion

        public VideoRoadRollFaultsController(IVideoEquipFaultsAppService videoEquipFaultsAppService)
        {
            _veqFaultApp = videoEquipFaultsAppService;
        }

        // GET: VideoRoadRollFaults
        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize("VideoRRFaultData")]
        public ActionResult VideoEquipFaults()
        {
            return View();
        }

        public JsonResult GetVideoEquipFaults(VeqFaultInput input)
        {
            return this.Json(_veqFaultApp.GetAllVideoEqFaults2(input));
        }

        public JsonResult ProcessVideoEquipFaults(CreateOrUpdateVideoEqFault input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridDelete:
                    VqFaultDelete(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("VideoRRFaultData.Delete")]
        //[AbpMvcAuthorize("SensorBusinessDetail.Delete")]
        public void VqFaultDelete(CreateOrUpdateVideoEqFault input)
        {
            _veqFaultApp.Delete2(input.Id);
        }
    }
}