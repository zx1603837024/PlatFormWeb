using Abp.Application.Services;
using P4.Coupons.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Coupons
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICouponAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllCouponOutput GetAll(SearchCouponInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Add(CreateOrUpdateCouponInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CouponId"></param>
        /// <returns></returns>
        string Delete(int CouponId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Update(CreateOrUpdateCouponInput input);
    }
}
