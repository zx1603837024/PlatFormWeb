using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(MonthlyCarDto))]
    public class MonthlyCarExportDto : EntityDto
    {
        /// <summary>
        /// 车主
        /// </summary>
        public string VehicleOwner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 车主电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public string BeginTimeStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string EndTimeStr { get; set; }

        public string MonthyType { get; set; }
    }
}
