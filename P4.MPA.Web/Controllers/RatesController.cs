using Abp.Domain.Repositories;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Berthsecs;
using P4.Dictionarys;
using P4.Dictionarys.Dtos;
using P4.Rates;
using P4.Rates.Dtos;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 费率管理
    /// </summary>
    [AbpMvcAuthorize]
    public class RatesController : P4ControllerBase
    {

        #region Var
        private readonly IRatesAppService _rateApplicationService;
        private readonly IDictionarysAppService _dictionarysAppService;
        private readonly IRepository<Rate, int> _abpRateRepository;
        //private readonly BerthsecAppService _abpBerthsecAppService;
        //JavaScriptSerializer js = new JavaScriptSerializer();         

        #endregion

            /// <summary>
            /// 
            /// </summary>
            /// <param name="rateApplicationService"></param>
            /// <param name="dictionarysAppService"></param>
            /// <param name="abpRateRepository"></param>
            /// <param name="_abpBerthsecAppService"></param>
        public RatesController(IRatesAppService rateApplicationService, IDictionarysAppService dictionarysAppService,
            IRepository<Rate, int> abpRateRepository, BerthsecAppService _abpBerthsecAppService)
        {
            _rateApplicationService = rateApplicationService;
            _dictionarysAppService = dictionarysAppService;
            _abpRateRepository = abpRateRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("RatesManagement")]
        public ActionResult RateManagement()
        {
            //Id	ValueCode	TypeCode	ValueData	TenantId	IsActive	Remark	IsDefault	Order
            //81	0	A10010	分钟	NULL	1	按分钟计费	1	1
            //82	1	A10010	小时	NULL	1	按小时计费	1	2
            //83	2	A10010	车次	NULL	1	按车次计费	1	3
            //74	0	A10008	所有	NULL	1	所有车辆
            //75	1	A10008	大型车	NULL	1	大型车
            //76	2	A10008	小型车	NULL	1	小型车
            //77	3	A10008	摩托车	NULL	1	摩托车
            //78	0	A10009	按时	NULL	1	按时收费
            //80	1	A10009	按次	NULL	1	按次收费
            RateSelectModel selectmodel = new RateSelectModel();

            List<GetAllValueDataDto> dislist = _dictionarysAppService.GetAllValueData(P4Consts.VehicleType);
              dislist.AddRange(_dictionarysAppService.GetAllValueData(P4Consts.RateType));
              dislist.AddRange(_dictionarysAppService.GetAllValueData(P4Consts.ChargeType));
            selectmodel.cartype = "{";
            selectmodel.carfee="{";
            selectmodel.ratefee="{";
            if (dislist != null && dislist.Count > 0)
            { 
                foreach (var item in dislist)
                {
                    if (item.TypeCode == P4Consts.VehicleType)
                        //selectmodel.cartype += "'" + item.ValueData + "'" + ":" + "'" + item.ValueCode.ToString() + "'" + ",";
                         selectmodel.cartype +=  item.ValueData + ":" +  item.ValueCode.ToString() + ",";
                    else if (item.TypeCode == P4Consts.RateType)
                        selectmodel.ratefee +=  item.ValueData  + ":" + item.ValueCode.ToString()  + ",";
                    else if (item.TypeCode == P4Consts.ChargeType)
                        selectmodel.carfee +=item.ValueData  + ":"  + item.ValueCode.ToString() + ",";
                }
                selectmodel.cartype =selectmodel.cartype.Length==1? "{":selectmodel.cartype.Substring(0, selectmodel.cartype.Length - 1);
                selectmodel.ratefee = selectmodel.ratefee.Length == 1 ? "{" : selectmodel.ratefee.Substring(0, selectmodel.ratefee.Length - 1);
                selectmodel.carfee = selectmodel.carfee.Length == 1 ? "{" : selectmodel.carfee.Substring(0, selectmodel.carfee.Length - 1);
            }
            selectmodel.cartype += "}"; selectmodel.ratefee += "}"; selectmodel.carfee += "}";
            selectmodel.HasCompany = AbpSession.CompanyId.HasValue;
            //dislist.AddRange(_dictionarysAppService.GetAllValueData("A10009"));
            //dislist.AddRange(_dictionarysAppService.GetAllValueData("A10010"));
            return View(selectmodel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult GetAllRateList(RateInput entity)
        {
            return Json(_rateApplicationService.GetAllRateList(entity));
        }

        /// <summary>
        /// 根据WorkGroupID获取未分配工作组的收费员和泊位段以及已分配给该工作组的收费员和泊位段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetRateById(string id)
        {

            int rateId = 0;
            int.TryParse(id, out rateId);
            RateDto ratedto = _rateApplicationService.GetRateById(rateId);
            return this.Json(ratedto);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult ProcessRate(RateSaveOrUpdateDto input, RateModel model)
        {

            JsonResult returnJson = new JsonResult();
            switch (input.oper)
            {
                case "del":
                    returnJson = Delete(input);
                    break;
                case "add":
                    returnJson = Insert(model);
                    break;
                case "edit":
                    {
                        model.Id = input.Id;
                        returnJson = Edit(model);
                    }

                    break;
                default:
                    break;
            }
            return returnJson;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult Delete(RateSaveOrUpdateDto input)
        {


            if (_rateApplicationService.Delete(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("有泊位段在使用该费率，请先解除绑定！")), JsonRequestBehavior.AllowGet);
            else
                return this.Json(new MvcAjaxResponse(true), JsonRequestBehavior.AllowGet);
            //_rateApplicationService.Delete(input);
            //return this.Json("12121");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Insert(RateModel model)
        {
            if (_rateApplicationService.Insert(model) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("费率名称重复！")));
            else
                return this.Json(new MvcAjaxResponse(true));
            //_rateApplicationService.Insert(model);
            //return this.Json("12121");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Edit(RateModel model)
        {
            if (_rateApplicationService.Edit(model) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("费率名称不可重复")));
            else if (_rateApplicationService.Edit(model) == 2)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("费率已分配泊位段，不可更改启用状态")));
            else 
                return this.Json(new MvcAjaxResponse(true));
  
            //_rateApplicationService.Edit(model);
            //return this.Json("12121");
        } 

        /// <summary>
        /// 测试费率
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="inCarTime"></param>
        /// <param name="outCarTime"></param>
        /// <param name="carType"></param>
        /// <param name="RateId"></param>
        /// <returns></returns>
        public JsonResult RateCalculate(int berthsecID, DateTime inCarTime, DateTime outCarTime, int carType, int RateId)
        {
            RateCalculateModel s = _rateApplicationService.RateCalculate(berthsecID, inCarTime, outCarTime, carType, RateId, 1, "", 1);
            return this.Json(s, JsonRequestBehavior.AllowGet);
        }
    }

}