using Abp.Application.Services;
using P4.AliPay.Dto;

namespace P4.AliPay
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAliPayOrderAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllAliPayOrdersOutput GetAllAliPayOrderDetail(SearchAliPayOrdersInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllAliPayOrdersOutput GetAllAliPayOrderDetail1(SearchAliPayOrdersInput input);
    }
}
