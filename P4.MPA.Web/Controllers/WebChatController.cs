using P4.WebChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 微信接口
    /// </summary>
    public class WebChatController : P4ControllerBase
    {
        #region Var
        private readonly IWebchatAppService _webchatAppService;
        #endregion


        public WebChatController(IWebchatAppService webchatAppService)
        {
            _webchatAppService = webchatAppService;
        }

        // GET: WebChat
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取历史停车列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetStopCarList(int page, int size)
        {
            return this.Json(_webchatAppService.GetStopCarList(page, size), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取停车记录详细信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetStopCarInfo(Int64 Id)
        {
            return this.Json(_webchatAppService.GetStopCarInfo(Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAd()
        {
            return this.Json(_webchatAppService.GetAdsList(), JsonRequestBehavior.AllowGet);
        }
    }
}