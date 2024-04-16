using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(MonthlyCarAbnormal))]
    public class MonthlyCarAbnormalDto : EntityDto
    {
        #region BusinessDetail
        /// <summary>
        /// 
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { set; get; }

        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public DateTime CarInTime { get; set; }

        /// <summary>
        /// 车辆出场时间
        /// </summary>
        public DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public decimal FactReceive { get; set; }

        /// <summary>
        /// 是否逃逸
        /// </summary>
        public bool IsEscapePay { get; set; }
        #endregion

        #region MonthlyCar

        /// <summary>
        /// 
        /// </summary>
        public string VehicleOwner { get; set; }

        /// <summary>
        /// 车主电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 包月时段
        /// </summary>
        public string MonthyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        #endregion
    }
}
