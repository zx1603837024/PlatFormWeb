using Abp.Domain.Repositories;
using P4.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.Coupons.Dtos;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using P4.CouponsDetails;
using P4.CouponsDetails.Dtos;

namespace P4.Coupons
{
    /// <summary>
    /// 
    /// </summary>
   public  class CouponAppService : P4AppServiceBase, ICouponAppService
    {
        #region Var
        private readonly IRepository<Coupon> _couponAppService;
        private readonly ICouponDetailsAppService _couponDetailsAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couponAppService"></param>
        /// <param name="couponDetailsAppService"></param>
        public CouponAppService(IRepository<Coupon> couponAppService, ICouponDetailsAppService couponDetailsAppService)
        {
            _couponAppService = couponAppService;
            _couponDetailsAppService = couponDetailsAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Add(CreateOrUpdateCouponInput input)
        {
            Coupon model = input.MapTo<Coupon>();
            model.CouponNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            model.Mark = "平台赠送";
            model.IsActive = false;
            model.CouponType = 4;
            _couponAppService.Insert(model);

            CouponDetailDto dto = new CouponDetailDto();
            dto.Source = "平台赠送";
            dto.CouponNo = model.CouponNo;
            dto.OldMoney = 0;
            dto.ConsumptionMoney = model.Money;
            dto.NewMoney = model.Money;
            dto.Platenumber = "";
            dto.tel = model.tel;
            dto.openId = model.OpenId;
            dto.CouponType = 1;
            _couponDetailsAppService.AddCouponDetail(dto);
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CouponId"></param>
        /// <returns></returns>
        public string Delete(int CouponId)
        {
            CouponDetailDto dto = new CouponDetailDto();
            var model = _couponAppService.Load(CouponId);
            dto.Source = "平台删除";
            dto.CouponNo = model.CouponNo;
            dto.OldMoney = model.Money;
            dto.ConsumptionMoney = model.Money;
            dto.NewMoney = 0;
            dto.Platenumber = "";
            dto.tel = model.tel;
            dto.openId = model.OpenId;
            dto.CouponType = 2;
            _couponDetailsAppService.AddCouponDetail(dto);
            _couponAppService.Delete(CouponId);
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllCouponOutput GetAll(SearchCouponInput input)
        {
            int records = _couponAppService.GetAll().Filters(input).ToList().Count;
            decimal totalmoney = _couponAppService.GetAll().Filters(input).Sum(entity => entity.Money);
            return new GetAllCouponOutput()
            {
                rows = _couponAppService.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList().MapTo<List<CouponDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = new CouponDto() { tel = "合计：", Money = totalmoney,  }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Update(CreateOrUpdateCouponInput input)
        {
            var model = _couponAppService.Load(input.Id);
            if (model.Money != input.Money)
            {
                CouponDetailDto dto = new CouponDetailDto();
                dto.Source = "平台处理";
                dto.CouponNo = model.CouponNo;
                dto.OldMoney = model.Money;
                dto.ConsumptionMoney = System.Math.Abs(model.Money - input.Money);
                dto.NewMoney = input.Money;
                dto.Platenumber = "";
                dto.tel = model.tel;
                dto.openId = model.OpenId;
                dto.CouponType = model.Money > input.Money ? 2 : 1;//消费类型, 1充值,  2消费
                _couponDetailsAppService.AddCouponDetail(dto);
            }
            model.tel = input.tel;
            model.Money = input.Money;
            model.Mark = input.Mark;
            _couponAppService.Update(model);
            return "";
        }
    }
}
