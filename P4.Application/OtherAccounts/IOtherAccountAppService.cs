using Abp.Application.Services;
using P4.OtherAccounts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.OtherAccounts
{
    /// <summary>
    /// 车主卡片管理
    /// </summary>
    public interface IOtherAccountAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OtherAccountDto Insert(CreateOrUpdateOtherAccountInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OtherAccountDto Update(CreateOrUpdateOtherAccountInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OtherAccountDto GetAccountByID(int Id);
        /// <summary>
        /// Mone卡充值
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="upMoney"></param>
        /// <returns></returns>
        [HttpGet]
        Task<int> UpdateAccountWallet(int Id, decimal upMoney);



        /// <summary>
        /// 激活卡片
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [HttpGet]
        int UpdateEnabled(string cardNo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="enableTime"></param>
        /// <returns></returns>
        [HttpGet]
        int UpdateEnabled1(string cardNo, string enableTime);
        /// <summary>
        /// 激活卡片 增加车牌和手机号
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="TelePhone"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="EnableTime"></param>
        /// <returns></returns>
        [HttpGet]
        int UpdateEnabled2(string CardNo, string TelePhone, string PlateNumber, string EnableTime);

        /// <summary>
        /// 解除车牌绑定
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        int AbsolvePlatNumber(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="IsEnableAccountName"></param>
        /// <param name="employeeIdInput"></param>
        /// <returns></returns>
        [HttpGet]
        int OtherAccountIsEnabnle(int Id, string IsEnableAccountName, int employeeIdInput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllOtherAccountOutput GetAll(SearchOtherAccountInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetTotal();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        OtherAccountDto AccountLogin(string username, string password);

        /// <summary>
        /// 获取钱包明细记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllDeductionRecordOutput GetAllDeductionRecord(SearchDeductionRecordInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllDeductionRecordOutput GetDynamicReportDeductionRecord(SearchDeductionRecordInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<DeductionRecordDto> GetDynamicRDeductionEchar(SearchDeductionRecordInput input);
        /// <summary>
        /// 根据商户 进行统计商户时间段内总的消费详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<DeductionRecordDto> GetDynamicRDeduStatisEchar(SearchDeductionRecordInput input);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="phoneNum"></param>
        /// <param name="plateNumber"></param>
        /// <param name="sourcetype"></param>
        /// <param name="clien"></param>
        /// <returns></returns>
        [HttpGet]
        string NewCard(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="phoneNum"></param>
        /// <param name="plateNumber"></param>
        /// <param name="sourcetype"></param>
        /// <param name="clien"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        string NewCard(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien, string guid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="phoneNum"></param>
        /// <param name="plateNumber"></param>
        /// <param name="sourcetype"></param>
        /// <param name="clien"></param>
        /// <param name="guid"></param>
        /// <param name="productNo"></param>
        /// <returns></returns>
        [HttpGet]
        string NewCard(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien, string guid, string productNo);

        /// <summary>
        ///
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="phoneNum"></param>
        /// <param name="plateNumber"></param>
        /// <param name="sourcetype"></param>
        /// <param name="clien"></param>
        /// <param name="guid"></param>
        /// <param name="productNo"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet]
        string NewCardAddTime(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien, string guid, string productNo, string time);

        /// <summary>
        /// 金额充值
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpGet]
        string RenewCard(string cardNo, decimal money);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet]
        string RenewCardAddTime(string cardNo, decimal money, string time);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="phone"></param>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        [HttpGet]
        string QueryCard(string cardNo, string phone, string plateNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpGet]
        string ReturnCard(string cardNo, decimal money);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        string ReturnCard(string cardNo, decimal money, string guid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="guid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet]
        string ReturnCardAddTime(string cardNo, decimal money, string guid, string time);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        [HttpGet]
        string CarOwnerRegister(string userPhone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        [HttpGet]
        string GetIdentifyingCode(string userPhone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        [HttpGet]
        string GetIdentifyingCode1(string userPhone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecords(SearchDeductionRecordInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecordsOnlyMonth(SearchDeductionRecordInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetThisAccountAndDeductionRecords(SearchDeductionRecordInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetLastMonthAccountAndDeductionRecords(SearchDeductionRecordInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetNextMonthAccountAndDeductionRecords(SearchDeductionRecordInput input);

        /// <summary>
        /// Mone卡消费详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllOtherAccountOutput GetAllOtherAccountAndPlateNumber(SearchOtherAccountInput input);

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        void CheckEnabled();
    }
}
