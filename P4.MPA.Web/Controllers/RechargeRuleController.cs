using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.RechargeRule;
using P4.RechargeRule.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class RechargeRuleController : P4ControllerBase
    {
        #region Var
        private IRechargeRuleAppService _rechargeRuleAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rechargeRuleAppService"></param>
        public RechargeRuleController(IRechargeRuleAppService rechargeRuleAppService)
        {
            _rechargeRuleAppService = rechargeRuleAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("RechargeRuleManager")]
        public ActionResult RechargeRuleManager()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllRechargeRuleList(SearchRechargeRuleInput input)
        {
            return this.Json(_rechargeRuleAppService.GetAll(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessRechargeRule(CreateOrUpdateRechargeRuleInput input)
        {
            JsonResult returnJson = new JsonResult();
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    InsertRechargeRule(input);
                    break;
                case P4Consts.JqGridDelete:
                    DeleteRechargeRule(input);
                    break;
                case P4Consts.JqGridUpdate:
                    UpdateRechargeRule(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("RechargeRuleManager.Insert")]
        public JsonResult InsertRechargeRule(CreateOrUpdateRechargeRuleInput input)
        {
            _rechargeRuleAppService.Insert(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize("RechargeRuleManager.Update")]
        public JsonResult UpdateRechargeRule(CreateOrUpdateRechargeRuleInput input)
        {
            _rechargeRuleAppService.Update(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize("RechargeRuleManager.Delete")]
        public JsonResult DeleteRechargeRule(CreateOrUpdateRechargeRuleInput input)
        {
            _rechargeRuleAppService.Delete(input.Id);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }
    }
}