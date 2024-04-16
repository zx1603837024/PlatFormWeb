using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(MonthlyCarHistoryDto))]
    public class MonthlyCarHistoryExportDto : EntityDto<long>
    {
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
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 
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
        public string MonthyType { get; set; }


    }
}
