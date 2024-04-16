using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.BlackLists;
using P4.BlackLists.Dtos;
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
    public class BlackListsController : P4ControllerBase
    {
        #region Var
      private readonly  IBlackListsAppService _BlackListsAppService;
        private readonly IDictionarysAppService _dictionarysApplicationService;
        #endregion
        public BlackListsController(IBlackListsAppService blackListsAppService, IDictionarysAppService dictionarysApplicationService)
        {
            _BlackListsAppService = blackListsAppService;
            _dictionarysApplicationService = dictionarysApplicationService;
        }
       
        [AbpMvcAuthorize("BlackManagement")]
        public ActionResult BlackListManagement()
        {
            return View();
        }
        public JsonResult GetAllBlackLists(BlackListsInput entity)
        {
            return this.Json(_BlackListsAppService.GetAllBlackLists(entity));
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

        public JsonResult ProcessBlackLists(CreateOrUpdateBlackListsInput input)
        {
            JsonResult returnJson = new JsonResult();
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = BlackListsInsert(input);
                    break;
                case P4Consts.JqGridDelete:
                    returnJson = BlackListsDelete(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = BlackListsEdit(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("BlackManagement.Insert")]
        public string BlackListsInsert(CreateOrUpdateBlackListsInput input)
        {
           return  _BlackListsAppService.BlackListsInsert(input);
    
        }

        [AbpMvcAuthorize("BlackManagement.Delete")]
        public JsonResult BlackListsDelete(CreateOrUpdateBlackListsInput input)
        {
            _BlackListsAppService.BlackListsDelete(input);
            return this.Json("123123");
        }
        [AbpMvcAuthorize("BlackManagement.Update")]
        public string BlackListsEdit(CreateOrUpdateBlackListsInput input)
        {
          return  _BlackListsAppService.BlackListsUpdate(input);
    
        }

    }
}