using Abp.Application.Services;
using P4.Weixin.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWeixinAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        WeixinConfigDto GetWeixinConfig();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinConfigOutput GetAllWeixinConfig(SearchWeixinConfigInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void Insert(CreateOrUpdateWeixinConfigInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void Update(CreateOrUpdateWeixinConfigInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinTUserOutput GetWeixinTUserList(SearchWeixinTUserInput input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinUserOutput GetWeixinUserList(SearchWeixinUserInput input);

        /// <summary>
        /// 总微信用户
        /// </summary>
        /// <returns></returns>
        int GetWeixinRegisterCount();

        /// <summary>
        /// 新注册用户
        /// </summary>
        /// <returns></returns>
        int GetWeixinNewCount();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        [HttpGet]
        void SendWeixinSignal(int tenantId);

        /// <summary>
        /// 获取微信订单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        WeixinOrdersDto GetWeixinOrderInfo(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetWeixinOrdersOutput GetWeixinOrderList(SearchWeixinOrdersInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinMonthRuleOutput GetWeixinMonthRuleList(SearchWeixinMonthRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinRechargeRuleOutput GetWeixinRechargeRuleList(SearchWeixinRechargeRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Delete(CreateOrUpdateWeixinMonthRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Modify(CreateOrUpdateWeixinMonthRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Insert(CreateOrUpdateWeixinMonthRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Delete(CreateOrUpdateWeixinRechargeRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Modify(CreateOrUpdateWeixinRechargeRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Insert(CreateOrUpdateWeixinRechargeRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinIdeaOutput GetWeixinIdeaList(SearchWeixinIdeaInput input);

        /// <summary>
        /// 意见反馈
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool InsertIdea(WeixinIdeaDto input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        List<WeixinIdeaDto> GetWeixinIdeaToTop(int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int WeixinIdeaDelete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Reply"></param>
        /// <returns></returns>
        int WeixinIdeaUpdate(int Id, string Reply);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void UpdateWeixinConfig(CreateOrUpdateWeixinConfigInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        DecisionWeixinDto GetDecisionWeixinModel(DateTime BeginTime, DateTime EndTime);
    }
}
