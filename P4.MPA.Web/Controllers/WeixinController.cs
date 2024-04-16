using Abp;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Helper;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Businesses;
using P4.Coupons;
using P4.Coupons.Dtos;
using P4.CouponsDetails;
using P4.CouponsDetails.Dtos;
using P4.Employees;
using P4.Web.Models;
using P4.Web.Models.Weixin;
using P4.Weixin;
using P4.Weixin.Dtos;
using P4.WeixinPushMsg;
using P4.WeixinPushMsg.Dtos;
using P4.WeixinSendMsgModel;
using P4.WeixinSendMsgModel.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 微信相关功能管理
    /// </summary>
    public class WeixinController : P4ControllerBase
    {
        #region Var
        private readonly IWeixinAppService _weixinAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly Users.IUserAppService _userApplicationService;
        private readonly IWeixinHistoryMsgAppService _weixinHistoryMsgAppService;
        private readonly ISettingStore _settingStore;
        private readonly ICouponAppService _couponAppService;
        private readonly IWeixinSendMsgModelAppService _weixinSendMsgModelAppService;
        private readonly ICouponDetailsAppService _couponDetailsAppService;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userApplicationService"></param>
        /// <param name="weixinAppService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="businessAppService"></param>
        /// <param name="weixinHistoryMsgAppService"></param>
        /// <param name="settingStore"></param>
        /// <param name="couponAppService"></param>
        /// <param name="weixinSendMsgModelAppService"></param>
        public WeixinController(Users.IUserAppService userApplicationService, IWeixinAppService weixinAppService, IEmployeeAppService employeeAppService, IBusinessAppService businessAppService, IWeixinHistoryMsgAppService weixinHistoryMsgAppService, ISettingStore settingStore, ICouponAppService couponAppService, IWeixinSendMsgModelAppService weixinSendMsgModelAppService, ICouponDetailsAppService couponDetailsAppService)
        {
            _weixinAppService = weixinAppService;
            _businessAppService = businessAppService;
            _employeeAppService = employeeAppService;
            _userApplicationService = userApplicationService;
            _weixinHistoryMsgAppService = weixinHistoryMsgAppService;
            _settingStore = settingStore;
            _couponAppService = couponAppService;
            _weixinSendMsgModelAppService = weixinSendMsgModelAppService;
            _couponDetailsAppService = couponDetailsAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("MicroPublic")]
        public ActionResult WeixinConfig()
        {
            WeixinSettingModel model = new WeixinSettingModel()
            {
                employeeList = _employeeAppService.GetEmployeeAll(),
                AllOption = nooption,
                weixinconfigmodel = _weixinAppService.GetWeixinConfig(),
                UserList = _userApplicationService.GetUsers().Items.ToList().MapTo<System.Collections.Generic.List<Users.Dto.UserDto>>(),
            };
            //var Model = _weixinAppService.GetWeixinConfig();
            //if(Model !=null)
            //ViewBag.MonthlyPayment = Model.MonthlyPayment;
            //ViewBag.RecoverMoneny = Model.RecoverMoneny;
            //ViewBag.OnlineOrdering = Model.OnlineOrdering;
            //ViewBag.DepositCard = Model.DepositCard;
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult History()
        {
            WeixinModel model = new WeixinModel
            {
                Wexinparkorder = DataProcessHelper.GetEntityFromTable<Wexinparkorder>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 20 * from Wexinparkorder order by id desc").Tables[0])
            };

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult MyArrearage()
        {
            WeixinModel model = new WeixinModel
            {
                Wexinparkorder = DataProcessHelper.GetEntityFromTable<Wexinparkorder>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 20 * from Wexinparkorder order by id desc").Tables[0])
            };

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Payment()
        {
            WeixinModel model = new WeixinModel();

            if (SqlHelper.ExecuteScalar(SqlHelper.connectionString, System.Data.CommandType.Text, "select 1 from Wexinparkorder where caroutime is null") != null)
            {
                model.Wexinparkorder = DataProcessHelper.GetEntityFromTable<Wexinparkorder>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 1 * from Wexinparkorder where caroutime is null").Tables[0]);
                return View("PlatenumberArrearage", model);
            }

            model.Weixinuser = DataProcessHelper.GetEntityFromTable<Weixinuser>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 1 * from Weixinuser").Tables[0])[0];
            model.Weixinplate = DataProcessHelper.GetEntityFromTable<Weixinplate>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select * from Weixinplate order by orders").Tables[0]);
            model.parking = DataProcessHelper.GetEntityFromTable<parking>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 3 * from Weixinparking where status = 0 order by newid()").Tables[0]);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthnumber"></param>
        /// <param name="berthid"></param>
        /// <param name="stoptime"></param>
        /// <returns></returns>
        public JsonResult SavePayment(string berthnumber, string carnumber, string stoptime, string money)
        {
            DateTime dt = DateTime.Now;
            string sql = "insert into Wexinparkorder(berthnumber, carintime, carnumber, money, time) values('" + berthnumber + "', '" + dt + "', '" + carnumber + "', " + money + ", " + stoptime + ") select @@identity";

            var id = SqlHelper.ExecuteScalar(SqlHelper.connectionString, System.Data.CommandType.Text, sql);

            string sql1 = "insert into Weizinorderdetail(parkorderid, time, money, status) values(" + id + ", " + stoptime + ", " + money + ", 1)";
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, sql1);

            string sql2 = "update Weixinparking set carnumber = '" + carnumber + "', carintime = '" + dt + "', status = 1 where berthnumber = '" + berthnumber + "'";
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, sql2);
            return this.Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult PlatenumberArrearage()
        {

            WeixinModel model = new WeixinModel();

            if (SqlHelper.ExecuteScalar(SqlHelper.connectionString, System.Data.CommandType.Text, "select 1 from Wexinparkorder where caroutime is null") != null)
            {
                model.Wexinparkorder = DataProcessHelper.GetEntityFromTable<Wexinparkorder>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 1 * from Wexinparkorder where caroutime is null").Tables[0]);
                return View(model);
            }
            else
            {
                model.Weixinuser = DataProcessHelper.GetEntityFromTable<Weixinuser>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 1 * from Weixinuser").Tables[0])[0];
                model.Weixinplate = DataProcessHelper.GetEntityFromTable<Weixinplate>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select * from Weixinplate order by orders").Tables[0]);
                model.parking = DataProcessHelper.GetEntityFromTable<parking>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 3 * from Weixinparking where status = 0").Tables[0]);
                return View("Payment", model);
            }
        }

        /// <summary>
        /// 续费
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="moeny"></param>
        /// <param name="stoptime"></param>
        /// <returns></returns>
        public JsonResult SavePlatenumberArrearage(int ID, int moeny, int stoptime)
        {
            String sql = "update Wexinparkorder set  time = time + " + stoptime + ", money = money + " + moeny + " where ID = " + ID;
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, sql);
            string sql1 = "insert into Weizinorderdetail(parkorderid, time, money, status) values(" + ID + ", " + stoptime + ", " + moeny + ", 1)";
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, sql1);
            return this.Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            WeixinModel model = new WeixinModel();
            model.Weixinuser = DataProcessHelper.GetEntityFromTable<Weixinuser>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select top 1 * from Weixinuser").Tables[0])[0];
            model.Weixinplate = DataProcessHelper.GetEntityFromTable<Weixinplate>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select * from Weixinplate order by orders").Tables[0]);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Carnumber1"></param>
        /// <param name="Carnumber2"></param>
        /// <param name="Carnumber3"></param>
        /// <returns></returns>
        public JsonResult SaveRegister(string Carnumber1, string Carnumber2, string Carnumber3)
        {
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, "update Weixinuser  set  CarNumber1 = '" + Carnumber1 + "', CarNumber2 = '" + Carnumber2 + "', CarNumber3 = '" + Carnumber3 + "'");
            return this.Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ParkMap()
        {
            WeixinModel model = new WeixinModel();
            model.park = DataProcessHelper.GetEntityFromTable<park>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select * from Weixinpark").Tables[0]);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinTUser")]
        public ActionResult WeixinTUser()
        {
            return View();
        }

        /// <summary>
        /// 微信关注用户
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinUser")]
        public ActionResult WeixinUser()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinUser")]
        public JsonResult GetWeixinUserList(SearchWeixinUserInput input)
        {
            return this.Json(_weixinAppService.GetWeixinUserList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinTUser")]
        public JsonResult GetWeixinTUserList(SearchWeixinTUserInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
            {
                input.filters = input.filters.Replace("registerDateStr", "registerDate").Replace("lastLoginDateStr", "lastLoginDate");
            }
            input.sidx = input.sidx.Replace("registerDateStr", "registerDate").Replace("lastLoginDateStr", "lastLoginDate");
            return Json(_weixinAppService.GetWeixinTUserList(input));
        }

        /// <summary>
        /// 微信支付流水管理
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinOrders")]
        public ActionResult WeixinOrders()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinOrders")]
        public JsonResult GetWeixinOrdersList(SearchWeixinOrdersInput input)
        {
            return this.Json(_weixinAppService.GetWeixinOrderList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinHistoryMsg")]
        public ViewResult WeixinHistoryMsg()
        {
            return View();
        }

        /// <summary>
        /// 微信历史消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinHistoryMsg")]
        public JsonResult GetWeixinPushMsgList(SearchWeixinPushMsgInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
            {
                input.filters = input.filters.Replace("CreationTimeStr", "CreationTime");
            }

            if (!string.IsNullOrWhiteSpace(input.sidx))
            {
                input.sidx = input.sidx.Replace("CreationTimeStr", "CreationTime");
            }
            return Json(_weixinHistoryMsgAppService.GetAllWeixinPushMsgList(input));
        }

        /// <summary>
        /// 微信支付关联记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetWeixinOrderCorrelation(int Id)
        {
            WeixinOrdersDto weixinorderDto = _weixinAppService.GetWeixinOrderInfo(Id);
            return Json(_businessAppService.GetBusinessDetailsListSql(new Businesses.Dtos.GetBusinessDetailsInput() { Guid = weixinorderDto.attach.Split('\"')[3], page = 1, rows = 1000, operateDateBegin = "2016-01-01", operateDateEnd = "2116-01-01" }), JsonRequestBehavior.AllowGet);
        }

        #region 充值规则

        /// <summary>
        /// 车辆包月规则
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinMonthRule")]
        public ActionResult WeixinMonthRule()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinMonthRule")]
        public JsonResult GetWeixinMonthRuleList(SearchWeixinMonthRuleInput input)
        {
            return this.Json(_weixinAppService.GetWeixinMonthRuleList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessWeixinMonthRule(CreateOrUpdateWeixinMonthRuleInput input)
        {
            switch (input.oper)//操作类型
            {
                case "del":
                    return Delete(input);
                case "edit":
                    return Modify(input);
                case "add":
                    return Insert(input);
                default:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo("服务器异常")));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinMonthRule.Delete")]
        private JsonResult Delete(CreateOrUpdateWeixinMonthRuleInput input)
        {
            _weixinAppService.Delete(input);
            return this.Json(new MvcAjaxResponse());
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinMonthRule.Update")]
        private JsonResult Modify(CreateOrUpdateWeixinMonthRuleInput input)
        {
            _weixinAppService.Modify(input);
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinMonthRule.Insert")]
        private JsonResult Insert(CreateOrUpdateWeixinMonthRuleInput input)
        {
            _weixinAppService.Insert(input);
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 账号充值规则
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinRechargeRule")]
        public ActionResult WeixinRechargeRule()
        {
            return View();
        }

        /// <summary>
        /// 账号充值规则
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinRechargeRule")]
        public JsonResult GetWeixinRechargeRuleList(SearchWeixinRechargeRuleInput input)
        {
            return this.Json(_weixinAppService.GetWeixinRechargeRuleList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessWeixinRechargeRule(CreateOrUpdateWeixinRechargeRuleInput input)
        {

            switch (input.oper)//操作类型
            {
                case "del":
                    return Delete(input);
                case "edit":
                    return Modify(input);
                case "add":
                    return Insert(input);
                default:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo("服务器异常")));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinRechargeRule.Delete")]
        private JsonResult Delete(CreateOrUpdateWeixinRechargeRuleInput input)
        {
            _weixinAppService.Delete(input);
            return this.Json(new MvcAjaxResponse());
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinRechargeRule.Update")]
        private JsonResult Modify(CreateOrUpdateWeixinRechargeRuleInput input)
        {
            _weixinAppService.Modify(input);
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinRechargeRule.Insert")]
        private JsonResult Insert(CreateOrUpdateWeixinRechargeRuleInput input)
        {
            _weixinAppService.Insert(input);
            return this.Json(new MvcAjaxResponse());
        }

        #endregion

        #region 微信意见反馈

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinIdea")]
        public ActionResult WeixinIdea()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinIdea")]
        public JsonResult GetWeixinIdeaList(SearchWeixinIdeaInput input)
        {
            if (!String.IsNullOrWhiteSpace(input.filters))
            {
                input.filters = input.filters.Replace("createTimeStr", "createTime").Replace("ReplyTimeStr", "ReplyTime");
            }
            input.sidx = input.sidx.Replace("createTimeStr", "createTime").Replace("ReplyTimeStr", "ReplyTime");
            return this.Json(_weixinAppService.GetWeixinIdeaList(input));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinIdea.Delete")]
        public JsonResult ProcessWeixinIdea(int Id, string Reply, string oper)
        {
            switch (oper)
            {
                case P4Consts.JqGridDelete:
                    _weixinAppService.WeixinIdeaDelete(Id);
                    break;
                case P4Consts.JqGridUpdate:
                    _weixinAppService.WeixinIdeaUpdate(Id, Reply);
                    break;
            }
            return Json(new MvcAjaxResponse());
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetWeixinConfig()
        {
            return this.Json(_weixinAppService.GetWeixinConfig());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult SaveWeixinConfig(CreateOrUpdateWeixinConfigInput input)
        {
            input.TenantId = AbpSession.TenantId.Value;
            _weixinAppService.UpdateWeixinConfig(input);
            return Json(new MvcAjaxResponse(true));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinMessage")]
        public async Task<ActionResult> WeixinMessage()
        {
            var settings = await _settingStore.GetAllListAsync(AbpSession.TenantId.Value, null);
            WeixinMsgModel model = new WeixinMsgModel()
            {
                SendMonthRechargeMsg = settings.FirstOrDefault(entity => entity.Name == "SendMonthRechargeMsg").Value,
                SendMsgOrder = settings.FirstOrDefault(entity => entity.Name == "SendMsgOrder").Value,
                SendMsgOrderOut = settings.FirstOrDefault(entity => entity.Name == "SendMsgOrderOut").Value,
                SendMsgStopCar = settings.FirstOrDefault(entity => entity.Name == "SendMsgStopCar").Value,
                //SendMsgReservePark = settings.FirstOrDefault(entity => entity.Name == "SendMsgReservePark").Value,
                SendRechargeMsg = settings.FirstOrDefault(entity => entity.Name == "SendRechargeMsg").Value
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinMessage")]
        public async Task<ActionResult> SaveWeixinMessage(WeixinMessageModel entity)
        {
            SettingInfo model = new SettingInfo();
            model.TenantId = AbpSession.TenantId.Value;
            model.Name = "SendMonthRechargeMsg";
            model.Value = entity.SendMonthRechargeMsg;
            await _settingStore.UpdateAsync(model);

            model.TenantId = AbpSession.TenantId.Value;
            model.Name = "SendMsgOrder";
            model.Value = entity.SendMsgOrder;
            await _settingStore.UpdateAsync(model);

            model.TenantId = AbpSession.TenantId.Value;
            model.Name = "SendMsgOrderOut";
            model.Value = entity.SendMsgOrderOut;
            await _settingStore.UpdateAsync(model);

            model.TenantId = AbpSession.TenantId.Value;
            model.Name = "SendMsgStopCar";
            model.Value = entity.SendMsgStopCar;
            await _settingStore.UpdateAsync(model);

            model.TenantId = AbpSession.TenantId.Value;
            model.Name = "SendRechargeMsg";
            model.Value = entity.SendRechargeMsg;
            await _settingStore.UpdateAsync(model);

            //model.TenantId = AbpSession.TenantId.Value;
            //model.Name = "SendMsgReservePark";
            //model.Value = entity.SendMsgReservePark;
            await _settingStore.UpdateAsync(model);

            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CouponManager")]
        public ViewResult CouponManager()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CouponManager")]
        public JsonResult GetCouponsList(SearchCouponInput input)
        {
            return this.Json(_couponAppService.GetAll(input));
        }

        /// <summary>
        /// 优惠券明细
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CouponDetail")]
        public ViewResult CouponDetail()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CouponDetail")]
        public JsonResult GetCouponsDetailList(SearchCouponDetailInput input)
        {
            return Json(_couponDetailsAppService.GetAll(input));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessCoupon(CreateOrUpdateCouponInput input)
        {
            switch (input.oper)
            {
                case "add":
                    return AddCoupon(input);
                case "del":
                    return DeleteCoupon(input);
                case "edit":
                    return UpdateCoupon(input);
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse(new ErrorInfo("提交异常，请重试！")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult AddCoupon(CreateOrUpdateCouponInput input)
        {
            input.BeginTime = input.BeginTime.Split(' ')[0];
            input.EndTime = input.EndTime.Split(' ')[0];
            _couponAppService.Add(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult DeleteCoupon(CreateOrUpdateCouponInput input)
        {
            _couponAppService.Delete(input.Id);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult UpdateCoupon(CreateOrUpdateCouponInput input)
        {
            input.BeginTime = input.BeginTime.Split(' ')[0];
            input.EndTime = input.EndTime.Split(' ')[0];
            _couponAppService.Update(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinSendMsgModel")]
        public ViewResult WeixinSendMsgModel()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinSendMsgModel")]
        public JsonResult GetWeixinSendMsgModelList(SearchWeixinSendMsgModelInput input)
        {
            return Json(_weixinSendMsgModelAppService.GetAll(input));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessWeixinSendMsgModel(CreateOrUpdateWeixinSendMsgModelInput input)
        {
            switch (input.oper)
            {
                case "add":
                    return AddWeixinSendMsgModel(input);
                case "del":
                    return DeleteWeixinSendMsgModel(input);
                case "edit":
                    return UpdateWeixinSendMsgModel(input);
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse(new ErrorInfo("提交异常，请重试！")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult AddWeixinSendMsgModel(CreateOrUpdateWeixinSendMsgModelInput input)
        {
            _weixinSendMsgModelAppService.Add(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult DeleteWeixinSendMsgModel(CreateOrUpdateWeixinSendMsgModelInput input)
        {
            _weixinSendMsgModelAppService.Delete(input.Id);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult UpdateWeixinSendMsgModel(CreateOrUpdateWeixinSendMsgModelInput input)
        {
            _weixinSendMsgModelAppService.Update(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }
    }
}