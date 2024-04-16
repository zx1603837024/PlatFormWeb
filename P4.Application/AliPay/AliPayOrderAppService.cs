using Abp.Domain.Repositories;
using P4.AliPay.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Application.Services;

namespace P4.AliPay
{
    /// <summary>
    /// 
    /// </summary>
    public class AliPayOrderAppService :ApplicationService, IAliPayOrderAppService
    {
        private readonly IRepository<AliPayOrder> _abpAliPayOrderRepository;
        private readonly IAliPayOrderRepository _aliPayOrderAppServiceRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpAliPayOrderRepository"></param>
        /// <param name="aliPayOrderAppServiceRepository"></param>
        public AliPayOrderAppService(IRepository<AliPayOrder> abpAliPayOrderRepository, IAliPayOrderRepository aliPayOrderAppServiceRepository)
       {
           _abpAliPayOrderRepository = abpAliPayOrderRepository;
           _aliPayOrderAppServiceRepository = aliPayOrderAppServiceRepository;
       }


        /// <summary>
        /// 查询支付流水
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllAliPayOrdersOutput GetAllAliPayOrderDetail(SearchAliPayOrdersInput input)
        {
            List<AliPayOrdersDto> BBBB = _aliPayOrderAppServiceRepository.GetAliPayOrdersCount(input);
            AliPayOrderUserData aoud = new AliPayOrderUserData();
            aoud.trade_no = "金额汇总";
            if (BBBB.Count > 0)
            {
                aoud.total_amount = BBBB[0].total_amount;
            }
            int records = _abpAliPayOrderRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllAliPayOrdersOutput()
            {
                rows = _abpAliPayOrderRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<AliPayOrdersDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = aoud
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllAliPayOrdersOutput GetAllAliPayOrderDetail1(SearchAliPayOrdersInput input)
        {

            input.TenantId = AbpSession.TenantId;

            input.CompanyIds = AbpSession.CompanyIds;

            return _aliPayOrderAppServiceRepository.GetAllAliPayOrderDetailsql(input);
        }
    }
}
