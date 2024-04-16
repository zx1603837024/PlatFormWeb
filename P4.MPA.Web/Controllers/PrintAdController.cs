using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.PrintAdvertisement;
using P4.PrintAdvertisement.Dtos;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PrintAdController : P4ControllerBase
    {

        #region Var
        private readonly IPrintAdAppService _printAdAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printAdAppService"></param>
        public PrintAdController(IPrintAdAppService printAdAppService)
        {
            _printAdAppService = printAdAppService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("PrintAdManager")]
        public ActionResult PrintAdManager()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetPrintAdList(PrintAdInput input)
        {
            return Json(_printAdAppService.GetAllPrintAdsList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessPrintAd(CreatOrUpdatePrintAdInput input)
        {
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    InsertPrintAd(input);
                    break;
                case P4Consts.JqGridDelete:
                    DeletePrintAd(input);
                    break;
                case P4Consts.JqGridUpdate:
                    UpdatePrintAd(input);
                    break;
            }
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("PrintAdManager.Insert")]
        public JsonResult InsertPrintAd(CreatOrUpdatePrintAdInput input)
        {
            _printAdAppService.InsertPrintAd(input);
            return Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("PrintAdManager.Update")]
        public JsonResult UpdatePrintAd(CreatOrUpdatePrintAdInput input)
        {
            _printAdAppService.UpdatePrintAd(input);
            return Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("PrintAdManager.Delete")]
        public JsonResult DeletePrintAd(CreatOrUpdatePrintAdInput input)
        {
            _printAdAppService.DeletePrintAd(input);
            return Json("");
        }
    }
}