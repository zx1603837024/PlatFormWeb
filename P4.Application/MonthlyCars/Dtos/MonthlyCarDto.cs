using Abp.Application.Services.Dto;
using System;
using Abp.AutoMapper;
namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(MonthlyCar))]
    public class MonthlyCarDto : EntityDto
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
        /// 包月卡生效的停车场
        /// </summary>
        public string ParkIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MonthyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public string BeginTimeStr
        {
            get
            {
                if (BeginTime != null)
                {
                    return BeginTime.ToString("yyyy-MM-dd");
                }
                return null;
            }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string EndTimeStr
        {
            get
            {
                if (EndTime != null)
                {
                    return EndTime.ToString("yyyy-MM-dd");
                }
                return null;
            }
        }
    }
}
