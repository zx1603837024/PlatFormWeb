using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 停车场平台包月表
    /// </summary>
    [AutoMapFrom(typeof(ParkingMonthlyRent))]
    public class AbpParkingMonthlyRentDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 停车场ID
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 停车场平台ID
        /// </summary>
        public string ParkingId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTimeStr => BeginTime.ToString("yyyy-MM-dd");

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTimeStr => EndTime.ToString("yyyy-MM-dd");

        /// <summary>
        /// 停车场名称
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int CarType { get; set; }
    }
}
