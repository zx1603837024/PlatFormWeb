using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SpecialLists.Dtos
{
    [AutoMapFrom(typeof(SpecialList))]
    public class SpecialListsDto : EntityDto<int>
    {
        public string VehicleType { get; set; }

        public  string Discount { get; set; }
        public string CarOwner { get; set; }
        public string Remarks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }
        public bool IsActive { get; set; }
        //特殊类型  1 优惠时间段折扣车辆
        public  int? SpecilType { get; set; }
        //优惠开始时间
        public  string BeginDiscountTime { get; set; }
        //优惠结束时间
        public  string EndDiscountTime { get; set; }
        public  int? ParkId { get; set; }
        public  string ParkName { get; set; }
    }
}
