using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.SmsManagements;
using P4.SmsManagements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SmsController : P4ControllerBase
    {

        #region Var
        private readonly ISmsManagementAppService _smsManagementAppService;
        private readonly ISmsModelAppService _smsModelAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsManagementAppService"></param>
        /// <param name="smsModelAppService"></param>
        public SmsController(ISmsManagementAppService smsManagementAppService, ISmsModelAppService smsModelAppService)
        {
            _smsManagementAppService = smsManagementAppService;
            _smsModelAppService = smsModelAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SmsRecordManagement")]
        public ActionResult Smss()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetSmss(SearchSmssInput input)
        {
            return Json(_smsManagementAppService.GetSmsList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SmsModelManagement")]
        public ActionResult SmsModels()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SmsModelManagement")]
        public JsonResult GetSmsModelList(SearchSmsModelInput input)
        {
            return this.Json(_smsModelAppService.GetAllSmsModel(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessSmsModel(CreateOrUpdateDtoInput input)
        {
            switch (input.oper)
            {
                case "add":
                     Insert(input);
                    break;
                case "del":
                     Delete(input);
                    break;
                case "edit":
                     Update(input);
                    break;
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("SmsModelManagement.Insert")]
        public void Insert(CreateOrUpdateDtoInput input)
        {
            _smsModelAppService.Insert(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("SmsModelManagement.Delete")]
        public void Delete(CreateOrUpdateDtoInput input)
        {
            _smsModelAppService.Delete(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("SmsModelManagement.Update")]
        public void Update(CreateOrUpdateDtoInput input)
        {
            _smsModelAppService.Update(input);
        }

    }
}