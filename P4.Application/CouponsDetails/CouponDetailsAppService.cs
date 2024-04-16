using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.CouponsDetails.Dtos;
using Abp.Domain.Repositories;
using P4.Weixin;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;

namespace P4.CouponsDetails
{
    /// <summary>
    /// 
    /// </summary>
    public class CouponDetailsAppService : P4AppServiceBase, ICouponDetailsAppService
    {
        #region Var
        private readonly IRepository<CouponDetail> _couponDetailAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couponDetailAppService"></param>
        public CouponDetailsAppService(IRepository<CouponDetail> couponDetailAppService)
        {
            _couponDetailAppService = couponDetailAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void AddCouponDetail(CouponDetailDto dto)
        {
            _couponDetailAppService.Insert(dto.MapTo<CouponDetail>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllCouponDetailOutput GetAll(SearchCouponDetailInput input)
        {
           

            var ef = _couponDetailAppService.GetAll();

            if (input.filters != null)
            {
                FilterDto _filterDto = JsonConvert.DeserializeObject(input.filters, typeof(FilterDto)) as FilterDto;
                foreach (var entity in _filterDto.rules)
                {
                    switch (entity.field)
                    {
                        case "CouponNo":
                            ef = ef.Where(e => e.CouponNo == entity.data);
                            break;
                        case "CouponType":
                            int temp = int.Parse(entity.data);
                            ef = ef.Where(e => e.CouponType == temp);
                            break;
                        case "tel":
                            ef = ef.Where(e => e.tel == entity.data);
                            break;
                        case "CreationTime":
                            DateTime dt = DateTime.Parse(entity.data);
                            if (entity.op == "lt")//小于
                            {
                                ef = ef.Where(e => e.CreationTime < dt);
                            }
                            else if (entity.op == "gt")
                            {//大于
                                ef = ef.Where(e => e.CreationTime > dt);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            int records = ef.Count();
            return new GetAllCouponDetailOutput()
            {
                rows = ef.OrderByDescending(dic => dic.Id).PageBy(input).ToList().MapTo<List<CouponDetailDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = new CouponDetailDto() { CouponNo = "合计：", OldMoney = ef.Sum(e => e.OldMoney), NewMoney = ef.Sum(e => e.NewMoney), ConsumptionMoney = ef.Sum(e => e.ConsumptionMoney) }
            };
        }
    }
}
