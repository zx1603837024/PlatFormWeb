using Abp.Application.Services;
using P4.CouponsDetails.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CouponsDetails
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICouponDetailsAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllCouponDetailOutput GetAll(SearchCouponDetailInput input);

        /// <summary>
        /// 增加优惠券明细
        /// </summary>
        /// <param name="dto"></param>
        void AddCouponDetail(CouponDetailDto dto);
    }
}
