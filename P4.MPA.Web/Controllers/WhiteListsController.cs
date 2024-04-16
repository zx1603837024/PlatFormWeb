using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.BlackLists;
using P4.Dictionarys;
using P4.WhiteLists;
using P4.WhiteLists.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{   
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class WhiteListsController :P4ControllerBase
    {   

        #region Var
        private readonly IWhiteListsAppService _WhiteListsAppService;
        private readonly IDictionarysAppService _dictionarysApplicationService;
        #endregion
        public WhiteListsController(IWhiteListsAppService WhiteListsAppService, IDictionarysAppService dictionarysApplicationService)
        {
            _WhiteListsAppService = WhiteListsAppService;
            _dictionarysApplicationService = dictionarysApplicationService;
        }
        // GET: WhiteLists
        [AbpMvcAuthorize("WhiteManagement")]
        public ActionResult WhiteManagement()
        {
            return View();
        }
        public JsonResult GetAllWhiteLists(WhiteListsInput entity)
        {
            return this.Json(_WhiteListsAppService.GetAllWhiteLists(entity));
        }
        public string GetAllVehicleType()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10008"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public JsonResult ProcessWhiteLists(CreateOrUpdateWhiteListsInput input)
        {
            JsonResult returnJson = new JsonResult();
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case "add":
                    info.Message = WhiteListsInsert(input);
                    break;
                case "del":
                    returnJson = WhiteListsDelete(input);
                    break;
                case "edit":
                    info.Message = WhiteListsEdit(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("WhiteMangement.Insert")]
        public string WhiteListsInsert(CreateOrUpdateWhiteListsInput input)
        {
           return _WhiteListsAppService.WhiteListsInsert(input);
     
        }

        [AbpMvcAuthorize("WhiteMangement.Delete")]
        public JsonResult WhiteListsDelete(CreateOrUpdateWhiteListsInput input)
        {
            _WhiteListsAppService.WhiteListsDelete(input);
            return this.Json("123123");
        }
        [AbpMvcAuthorize("WhiteMangement.Update")]
        public string WhiteListsEdit(CreateOrUpdateWhiteListsInput input)
        {
           return   _WhiteListsAppService.WhiteListsUpdate(input);
          
        }

    }
}