using Abp.Application.Services.Dto;
using System;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(MonthlyCar))]
    public class CreateOrUpdateMonthlyCarInput : EntityRequestInput, IOperDto, IInputDto
    {
        /// <summary>
        /// 车主
        /// </summary>
        [MaxLength(20)]
        public string VehicleOwner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 包月时段类型
        /// </summary>
        public string MonthyType { get; set; }

        /// <summary>
        /// 车主电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 续费总金额（包括开卡金额）
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 续费月数
        /// </summary>
        public int Monthy { get; set; }
    }
}
