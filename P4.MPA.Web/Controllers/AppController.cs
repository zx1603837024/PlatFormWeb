using Abp.Web.Mvc.Authorization;
using P4.AppManage;
using P4.AppManage.Dto;
using P4.CarownerApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// app用户管理
    /// </summary>
    [AbpMvcAuthorize]
    public class AppController : P4ControllerBase
    {
        #region Var
        private readonly IAppManageAppService _appManageAppService;
        private readonly ICarownerAppService _carownerAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appManageAppService"></param>
        /// <param name="carownerAppService"></param>
        public AppController(IAppManageAppService appManageAppService, ICarownerAppService carownerAppService)
        {
            _appManageAppService = appManageAppService;
            _carownerAppService = carownerAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 下载升级包配置文件
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public FileStreamResult DownFile(string r)
        {
            string absoluFilePath = Server.MapPath("~\\Config\\update.xml");
            return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode("update.xml"));
        }

        /// <summary>
        /// 下载App
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public FileResult DownAppApk(string filename)
        {
            string fileName = filename;
            string absoluFilePath = Server.MapPath("~\\Apk\\" + fileName);
            return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetParkInfo(int parkId)
        {
            return Json(_carownerAppService.GetParkInfo(parkId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetOrderDetail(string guid, string p, string v)
        {
            return Json(_carownerAppService.GetOrderDetail(guid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("App")]
        public ActionResult AppManage()
        {
            return View();
        }

        /// <summary>
        /// App用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("App")]
        public JsonResult GetAppList(SearchAppInput input)
        {
            return this.Json(_appManageAppService.GetAll(input));
        }
    }
}