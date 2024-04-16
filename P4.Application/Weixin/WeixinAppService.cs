using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using P4.Weixin.Dtos;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using Abp.SignalR.SignalrService;
using P4.Parks;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System.Data;

namespace P4.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinAppService : ApplicationService,  IWeixinAppService
    {
        #region Var
        private readonly IRepository<WeixinConfig> _abpWeixinConfigRepository;
        private readonly IRepository<TUser> _abpWeixinTUserRepository;
        private readonly IRepository<WeixinUser> _abpWeixinUserRepository;
        private readonly IRepository<WeixinOrder> _abpWeixinOrderRepository;
        private readonly IRepository<MonthRule> _abpWeixinMonthRuleRepository;
        private readonly IRepository<WeixinRechargeRule> _abpWeixinRechargeRuleRepository;
        private readonly IRepository<WeixinIdea> _abpWeixinIdeaRepository;
        private readonly IRepository<Park> _abpParkRepository;
        private readonly IRepository<WeixinBank> _abpWeixinBankRepository;
        private readonly IRepository<WeixinTradeType> _abpWeixinTradeTypeRepository;
        private readonly IRepository<WeixinCurrencyType> _abpWeixinCurrencyTypeRepository;
        private readonly IP4ChatService _p4ChatService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpWeixinConfigRepository"></param>
        /// <param name="abpWeixinTUserRepository"></param>
        /// <param name="p4ChatService"></param>
        /// <param name="abpWeixinUserRepository"></param>
        /// <param name="abpWeixinOrderRepository"></param>
        /// <param name="abpWeixinMonthRuleRepository"></param>
        /// <param name="abpWeixinRechargeRuleRepository"></param>
        /// <param name="abpParkRepository"></param>
        /// <param name="abpWeixinIdeaRepository"></param>
        /// <param name="abpWeixinBankRepository"></param>
        /// <param name="abpWeixinTradeTypeRepository"></param>
        /// <param name="abpWeixinCurrencyTypeRepository"></param>
        public WeixinAppService(IRepository<WeixinConfig> abpWeixinConfigRepository, IRepository<TUser> abpWeixinTUserRepository, 
          IP4ChatService p4ChatService, IRepository<WeixinUser> abpWeixinUserRepository, IRepository<WeixinOrder> abpWeixinOrderRepository,
          IRepository<MonthRule> abpWeixinMonthRuleRepository, IRepository<WeixinRechargeRule> abpWeixinRechargeRuleRepository, 
          IRepository<Park> abpParkRepository, IRepository<WeixinIdea> abpWeixinIdeaRepository, 
          IRepository<WeixinBank> abpWeixinBankRepository, IRepository<WeixinTradeType> abpWeixinTradeTypeRepository, 
          IRepository<WeixinCurrencyType> abpWeixinCurrencyTypeRepository)
        {
            _abpWeixinConfigRepository = abpWeixinConfigRepository;
            _abpWeixinTUserRepository = abpWeixinTUserRepository;
            _p4ChatService = p4ChatService;
            _abpWeixinUserRepository = abpWeixinUserRepository;
            _abpWeixinOrderRepository = abpWeixinOrderRepository;
            _abpWeixinMonthRuleRepository = abpWeixinMonthRuleRepository;
            _abpWeixinRechargeRuleRepository = abpWeixinRechargeRuleRepository;
            _abpParkRepository = abpParkRepository;
            _abpWeixinIdeaRepository = abpWeixinIdeaRepository;
            _abpWeixinBankRepository = abpWeixinBankRepository;
            _abpWeixinTradeTypeRepository = abpWeixinTradeTypeRepository;
            _abpWeixinCurrencyTypeRepository = abpWeixinCurrencyTypeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _abpWeixinConfigRepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinConfigOutput GetAllWeixinConfig(SearchWeixinConfigInput input)
        {
            int records = _abpWeixinConfigRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllWeixinConfigOutput()
            {
                rows = _abpWeixinConfigRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinConfigDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WeixinConfigDto GetWeixinConfig()
        {
            return _abpWeixinConfigRepository.FirstOrDefault(entity => entity.TenantId == AbpSession.TenantId.Value).MapTo<WeixinConfigDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetWeixinNewCount()
        {
            return _abpWeixinTUserRepository.Count(entity => entity.registerDate > DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetWeixinRegisterCount()
        {
            return _abpWeixinTUserRepository.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinTUserOutput GetWeixinTUserList(SearchWeixinTUserInput input)
        {

            int records = _abpWeixinTUserRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllWeixinTUserOutput()
            {
                rows = _abpWeixinTUserRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinTUserDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>

        public void SendWeixinSignal(int tenantId)
        {
            _p4ChatService.CreateChatService().Clients.Group("index" + tenantId).sendIndexMessage(5, "微信XX用户注册成功");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Insert(CreateOrUpdateWeixinConfigInput input)
        {
            _abpWeixinConfigRepository.Insert(input.MapTo<WeixinConfig>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Update(CreateOrUpdateWeixinConfigInput input)
        {
            _abpWeixinConfigRepository.Update(input.MapTo<WeixinConfig>());
        }

        /// <summary>
        /// 微信config
        /// </summary>
        /// <param name="input"></param>
        public void UpdateWeixinConfig(CreateOrUpdateWeixinConfigInput input)
        {

            var model = _abpWeixinConfigRepository.Load(input.Id);
            model.AppId = input.AppId;
            model.AppName = input.AppName;
            model.AppSecret = input.AppSecret;
            model.PayUrl = input.PayUrl;
            model.BackPayUrl = input.BackPayUrl;
            model.BerthStatus = input.BerthStatus;
            model.BerthDetail = input.BerthDetail;
            model.EncodingAESKey = input.EncodingAESKey;
            model.TenantId = input.TenantId;
            model.Token = input.Token;
            model.ZappId = input.ZappId;
            model.privateKey = input.privateKey;
            model.serverUrl = input.serverUrl;
            model.alipayPulicKey = input.alipayPulicKey;
            model.webAppId = input.webAppId;
            model.encryptMessage = input.encryptMessage;
            model.domain = input.domain;
            model.mch_id = input.mch_id;
            model.paternerKey = input.paternerKey;
            model.subscribe_rul = input.subscribe_rul;

            model.MonthlyPayment = input.MonthlyPayment;
            model.DepositCard = input.DepositCard;
            model.RecoverMoneny = input.RecoverMoneny;
            model.OnlineOrdering = input.OnlineOrdering;

            _abpWeixinConfigRepository.Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinUserOutput GetWeixinUserList(SearchWeixinUserInput input)
        {
            int records = _abpWeixinUserRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllWeixinUserOutput()
            {
                rows = _abpWeixinUserRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinUserDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 获取微信支付流水List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetWeixinOrdersOutput GetWeixinOrderList(SearchWeixinOrdersInput input)
        {
            //FilterDto _filterDto = JsonConvert.DeserializeObject(input.filters, typeof(FilterDto)) as FilterDto;
            var sqlFilter = @"
SELECT TotalCount=COUNT(*) OVER(),
       a.Id,
	   a.appid, 
	   a.out_trade_no, 
	   a.openId, 
	   a.mch_id, 
	   a.cash_fee, 
	   a.total_fee, 
	   b.Value fee_type, 
	   CASE a.result_code WHEN 'SUCCESS' THEN '成功' ELSE '失败' END result_code, 
	   a.err_code, 
	   CASE a.is_subscribe WHEN 'Y' THEN '关注' ELSE '未关注' END is_subscribe, 
	   c.Value trade_type, 
	   d.Value bank_type, 
	   a.transaction_id, 
	   a.coupon_id, 
	   a.coupon_fee,
	   a.coupon_count, 
	   a.attach, 
	   a.time_end, 
	   a.couresCount, 
	   a.couresId, 
	   a.url, 
	   a.TenantId, 
	   a.CompanyId
FROM dbo.Weixinorders a WITH(NOLOCK)
     LEFT JOIN AbpWeixinCurrencyTypes b WITH(NOLOCK)ON a.fee_type=b.Value
     LEFT JOIN AbpWeixinTradeTypes c WITH(NOLOCK)ON a.trade_type=c.Value
     LEFT JOIN AbpWeixinBanks d WITH(NOLOCK)ON a.bank_type=d.Value
WHERE 1=1 and a.TenantId = " + AbpSession.TenantId.Value + " and a.CompanyId = " + AbpSession.CompanyId.Value ;
            var filter = "";
            if (input.DateBegin.HasValue)
            {
                filter += $" and  a.time_end >= '{input.DateBegin.Value:yyyyMMddHHmmss}' ";
            }
            if (input.DateEnd.HasValue)
            {
                filter += $" and  a.time_end <= '{input.DateEnd.Value:yyyyMMddHHmmss}' ";
            }
            if (!string.IsNullOrWhiteSpace(input.WeChatOrder))
            {
                filter += $" and a.out_trade_no like '%{input.WeChatOrder.Trim()}%'";
            }

            sqlFilter += filter;
            sqlFilter += $" ORDER BY a.Id DESC OFFSET {(input.page - 1) * input.rows} ROWS FETCH NEXT {input.rows} ROWS ONLY";

            var dt= Abp.DataProcessHelper.GetEntityFromTable<WeixinOrdersTotalCountDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sqlFilter).Tables[0]);
           
            int records = dt.Count>0?dt.First().TotalCount:0;
            if (records == 0)
                return new GetWeixinOrdersOutput();

            var querySumFee = @"SELECT SUM(a.total_fee) 
                                FROM dbo.Weixinorders a WITH(NOLOCK)
                                LEFT JOIN AbpWeixinCurrencyTypes b WITH(NOLOCK)ON a.fee_type=b.Value
                                LEFT JOIN AbpWeixinTradeTypes c WITH(NOLOCK)ON a.trade_type=c.Value
                                LEFT JOIN AbpWeixinBanks d WITH(NOLOCK)ON a.bank_type=d.Value
                                WHERE 1=1 and a.TenantId = " + AbpSession.TenantId.Value + " and a.CompanyId = " + AbpSession.CompanyId.Value + filter;

            var money = Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text,querySumFee);
            List<WeixinOrdersDto> list = new List<WeixinOrdersDto>();
            dt.ForEach(e =>
            {
                list.Add(e);
            });
            return new GetWeixinOrdersOutput()
            {
                rows = list,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = new WeixinOrdersDto() { appid = "合计", openId = records.ToString(), total_fee = Convert.ToDouble(money) }
                
            };
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinMonthRuleOutput GetWeixinMonthRuleList(SearchWeixinMonthRuleInput input)
        {
            int records = _abpWeixinMonthRuleRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllWeixinMonthRuleOutput()
            {
                rows = _abpWeixinMonthRuleRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinMonthRuleDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinRechargeRuleOutput GetWeixinRechargeRuleList(SearchWeixinRechargeRuleInput input)
        {
            int records = _abpWeixinRechargeRuleRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllWeixinRechargeRuleOutput()
            {
                rows = _abpWeixinRechargeRuleRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinRechargeRuleDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Delete(CreateOrUpdateWeixinMonthRuleInput input)
        {
            _abpWeixinMonthRuleRepository.Delete(input.Id);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Modify(CreateOrUpdateWeixinMonthRuleInput input)
        {
            input.ParkId = int.Parse(input.ParkName);

            if (input.ParkId == 0)
                input.ParkName = "不限路段";
            else
                input.ParkName = _abpParkRepository.Load(int.Parse(input.ParkName)).ParkName;
            _abpWeixinMonthRuleRepository.Update(input.MapTo<MonthRule>());
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Insert(CreateOrUpdateWeixinMonthRuleInput input)
        {
            input.ParkId = int.Parse(input.ParkName);
            if (input.ParkId == 0)
                input.ParkName = "不限路段";
            else
                input.ParkName = _abpParkRepository.Load(int.Parse(input.ParkName)).ParkName;
            _abpWeixinMonthRuleRepository.Insert(input.MapTo<MonthRule>());
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Delete(CreateOrUpdateWeixinRechargeRuleInput input)
        {
            _abpWeixinRechargeRuleRepository.Delete(input.Id);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Modify(CreateOrUpdateWeixinRechargeRuleInput input)
        {
            _abpWeixinRechargeRuleRepository.Update(input.MapTo<WeixinRechargeRule>());
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Insert(CreateOrUpdateWeixinRechargeRuleInput input)
        {
            _abpWeixinRechargeRuleRepository.Insert(input.MapTo<WeixinRechargeRule>());
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinIdeaOutput GetWeixinIdeaList(SearchWeixinIdeaInput input)
        {
            int records = _abpWeixinIdeaRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllWeixinIdeaOutput()
            {
                rows = _abpWeixinIdeaRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinIdeaDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int WeixinIdeaDelete(int Id)
        {
            _abpWeixinIdeaRepository.Delete(Id);
            return 1;
        }

        /// <summary>
        /// 微信意见回复
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Reply"></param>
        /// <returns></returns>
        public int WeixinIdeaUpdate(int Id, string Reply)
        {
            var model = _abpWeixinIdeaRepository.Load(Id);
            model.Reply = Reply;
            model.ReplyTime = DateTime.Now;
            _abpWeixinIdeaRepository.Update(model);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<WeixinIdeaDto> GetWeixinIdeaToTop(int count)
        {
            return _abpWeixinIdeaRepository.GetAll().OrderByDescending(entity => entity.createTime).Take(count).ToList().MapTo<List<WeixinIdeaDto>>();
        }

        /// <summary>
        /// 获取微信订单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public WeixinOrdersDto GetWeixinOrderInfo(int Id)
        {
            return _abpWeixinOrderRepository.Load(Id).MapTo<WeixinOrdersDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public DecisionWeixinDto GetDecisionWeixinModel(DateTime BeginTime, DateTime EndTime)
        {
            DecisionWeixinDto entity = new DecisionWeixinDto()
            {
                WeixinRegisterCount = _abpWeixinTUserRepository.Count(),
                WeixinNewRegisterCount = _abpWeixinTUserRepository.Count(entry=>entry.registerDate > BeginTime && entry.registerDate < EndTime),
                WeixinIdeaCount = _abpWeixinIdeaRepository.Count(),
                WeixinRelyIdeaCount = _abpWeixinIdeaRepository.Count(entry=>entry.ReplyTime > BeginTime && entry.ReplyTime < EndTime)
            };
            return entity;
        }

        /// <summary>
        /// 意见反馈
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool InsertIdea(WeixinIdeaDto input)
        {
            _abpWeixinIdeaRepository.Insert(input.MapTo<WeixinIdea>());
            return true;
        }
    }
}
