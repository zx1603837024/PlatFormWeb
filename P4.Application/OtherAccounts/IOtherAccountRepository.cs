using Abp.Domain.Repositories;
using P4.OtherAccounts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOtherAccountRepository : IRepository<OtherAccount, long>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="phone"></param>
        /// <param name="plateNumber"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        OtherAccountDto QueryCard(string cardNo, string phone, string plateNumber, int TenantId);
        /// <summary>
        /// 动态Mone消费详情报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllDeductionRecordOutput GetDynamicReportDeductionRecord(SearchDeductionRecordInput input);
        /// <summary>
        /// Mone消费详情报表  Echar
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<DeductionRecordDto> GetDynamicRDeductionEchar(SearchDeductionRecordInput input);


        /// <summary>
        /// 根据商户 进行统计商户时间段内总的消费详情
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<DeductionRecordDto> GetDynamicRDeduStatisEchar(SearchDeductionRecordInput input,int? tenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecords(SearchDeductionRecordInput input,int?tenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecordsOnlyMonth(SearchDeductionRecordInput input, int? tenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetThisAccountAndDeductionRecords(SearchDeductionRecordInput input, int? tenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetLastMonthAccountAndDeductionRecords(SearchDeductionRecordInput input, int? tenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<AccountAndDeductionRecordsDto> GetNextMonthAccountAndDeductionRecords(SearchDeductionRecordInput input, int? tenantID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllOtherAccountOutput GetAllOtherAccountAndPlateNumber(SearchOtherAccountInput input);

        /// <summary>
        /// 
        /// </summary>
        void CheckEnabled();
       
    }
}
