using Abp.Domain.Repositories;
using P4.AliPay.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AliPay
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAliPayOrderRepository : IRepository<AliPayOrder>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<AliPayOrdersDto> GetAliPayOrdersCount(SearchAliPayOrdersInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllAliPayOrdersOutput GetAllAliPayOrderDetailsql(SearchAliPayOrdersInput input);
    }
}
