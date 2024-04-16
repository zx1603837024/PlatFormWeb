using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(MonthlyCarHistory))]
    public class MonthlyCarHistoryDto : EntityDto<long>
    {
        /// <summary>
        /// 关联主表MonthlyCar中Id字段
        /// </summary>
        public int MonthlyCarId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreationUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VehicleOwner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 车主电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PayStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PayStatusName {
            get {
                switch (PayStatus)
                {
                    case 1:
                        return "现金";
                    case 2:
                        return "刷卡支付";
                    case 3:
                        return "微信支付";
                    case 4:
                        return "账号支付";
                    case 5:
                        return "未付";
                    case 6:
                        return "支付宝支付";
                    case 7:
                        return "其他";
                    default:
                        return "未知";
                }
            }
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MonthyType { get; set; }

    }
}
