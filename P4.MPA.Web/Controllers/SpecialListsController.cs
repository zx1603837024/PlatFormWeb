using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Dictionarys;
using P4.Parks;
using P4.SpecialLists;
using P4.SpecialLists.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    [AbpMvcAuthorize]
    public class SpecialListsController : P4ControllerBase
    {
        #region Var
        private readonly IDictionarysAppService _dictionarysApplicationService;
        private readonly ISpecialListAppSevice _specialListApplicationService;
        private readonly IParkAppService _parkAppService;
        #endregion
        public SpecialListsController(IDictionarysAppService dictionarysApplicationService, ISpecialListAppSevice specialListApplicationService, 
            IParkAppService parkAppService)
        {

            _dictionarysApplicationService = dictionarysApplicationService;
            _specialListApplicationService = specialListApplicationService;
            _parkAppService = parkAppService;
        }
       
         [AbpMvcAuthorize("SpecialCarManagement")]
        // GET: SpecialLists
        public ActionResult SpecialManagement()
        {
            return View();
        }

         public JsonResult GetAllSpecialLists(SpecialListInput entity)
         {
             return this.Json(_specialListApplicationService.GetAllSpecialLists(entity));
         }
         public string GetAllVehicleType()
         {
             StringBuilder strtemp = new StringBuilder("<select>");
             foreach (var model in _dictionarysApplicationService.GetAllValueData(P4Consts.VehicleType))
             {
                 strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
             }
             strtemp.AppendLine("</select>");
             return strtemp.ToString();
         }

         public JsonResult ProcessSpecilaLists(CreateOrUpdateSpecialListsInput input)
         {
             JsonResult returnJson = new JsonResult();
             ErrorInfo info = new ErrorInfo();
             switch (input.oper)
             {
                 case P4Consts.JqGridInsert:
                     info.Message = SpecilaListsInsert(input);
                     break;
                 case P4Consts.JqGridDelete:
                     returnJson = SpecilaListsDelete(input);
                     break;
                 case P4Consts.JqGridUpdate:
                     info.Message = SpecilaListsEdit(input);
                     break;
                 default:
                     break;
             }
             return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
         }



         [AbpMvcAuthorize("SpecialCarManagement.Insert")]
         public string SpecilaListsInsert(CreateOrUpdateSpecialListsInput input)
         {
             return _specialListApplicationService.SpecialListsInsert(input);

         }

         [AbpMvcAuthorize("SpecialCarManagement.Delete")]
         public JsonResult SpecilaListsDelete(CreateOrUpdateSpecialListsInput input)
         {
            _specialListApplicationService.SpecialListsDelete(input);
             return this.Json("123123");
         }
         [AbpMvcAuthorize("SpecialCarManagement.Update")]
         public string SpecilaListsEdit(CreateOrUpdateSpecialListsInput input)
         {
             return _specialListApplicationService.SpecialListsUpdate(input);

         }
         public string GetAllParkName()
         {
             StringBuilder strtemp = new StringBuilder("<select>");
             foreach (var model in _parkAppService.GetParkAll())
             {
                if (AbpSession.ParkIds.Exists(e => e == model.Id))
                     strtemp.AppendFormat(option, model.Id, model.ParkName);
             }
             strtemp.AppendLine("</select>");
             return strtemp.ToString();
         }

    }
}