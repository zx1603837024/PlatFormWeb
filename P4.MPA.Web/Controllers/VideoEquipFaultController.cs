using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using F2.SystemWork.Models;
using F2.SystemWork.Services;
using P4.VideoEquipFaultsNs;
using P4.VideoEquipFaultsNs.Dtos;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class VideoEquipFaultController : P4ControllerBase
    {
        #region Var
        private readonly IVideoEquipFaultsAppService _veqFaultApp;
        #endregion

        public VideoEquipFaultController(IVideoEquipFaultsAppService videoEquipFaultsAppService)
        {
            _veqFaultApp = videoEquipFaultsAppService;
        }

        // GET: VideoEquipFault
        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize("VideoEqManagementFaultData")]
        public ActionResult VideoEquipFaults()
        {
            return View();
        }
        [AbpMvcAuthorize("VideoSpEquipFaults")]
        public ActionResult VideoSpEquipFaults()
        {
            return View();
        }

        public JsonResult GetVideoEquipFaults(VideoEqBusinessDetailModel input)
        {
            VideoService vs = new VideoService();
            var res = vs.GetAllVideoEquipFaults(input);
            return Json(res, JsonRequestBehavior.AllowGet);
            //return this.Json(_veqFaultApp.GetAllVideoEqFaults(input));
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

        [AbpMvcAuthorize("VideoEqManagementStopData.Delete")]
        public void VqFaultDelete(CreateOrUpdateVideoEqFault input)
        {
            _veqFaultApp.Delete(input.Id);
        }
    }
}