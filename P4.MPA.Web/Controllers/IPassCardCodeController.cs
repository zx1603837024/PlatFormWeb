using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.CardCode;
using P4.CardCode.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    public class IPassCardCodeController : P4ControllerBase
    {
        private readonly IIPassCardCodeAppService _ipasscardcodeAppService;

        public IPassCardCodeController(IIPassCardCodeAppService ipasscardcodeAppService)
        {
            _ipasscardcodeAppService = ipasscardcodeAppService;
        }


        [AbpMvcAuthorize("IpassCardCode")]
        public ActionResult IPassCardCode()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("IpassCardCode.Search")]
        public JsonResult GetAllIPassCardCodeList(GetAllIPassCardCodeInput input)
        {
            return this.Json(_ipasscardcodeAppService.GetIPassCardCodeList(input));
        }


        public JsonResult ProcessIPassCardCode(CreateOrUpdateIPassCardCodeInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case "del":
                    DeleteIPassCardCode(input);
                    break;
                case "add":
                    info.Message = InsertIPassCardCode(input);
                    break;
                case "edit":
                    info.Message = ModifyIPassCardCode(input);
                    break;
                default:
                    break;

            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("IpassCardCode.Update")]
        public string ModifyIPassCardCode(CreateOrUpdateIPassCardCodeInput input)
        {
            return _ipasscardcodeAppService.ModifyIPassCardCode(input);
        }

        [AbpMvcAuthorize("IpassCardCode.Insert")]
        public string InsertIPassCardCode(CreateOrUpdateIPassCardCodeInput input)
        {
            return _ipasscardcodeAppService.InsertIPassCardCode(input);
        }

        [AbpMvcAuthorize("IpassCardCode.Delete")]
        public JsonResult DeleteIPassCardCode(CreateOrUpdateIPassCardCodeInput input)
        {
            _ipasscardcodeAppService.DeleteIPassCardCode(input);
            return this.Json("");
        }

        public string GetYesOrNoSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "true", "是");
            strtemp.AppendFormat(option, "false", "否");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public string GetDirectionSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "1", "进");
            strtemp.AppendFormat(option, "2", "出");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
    }
}