using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Signo
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpParkReservation")]
    public class ParkReservation : Entity<long>
    {
        /// <summary>
        /// 预订订单
        /// </summary>
        [MaxLength(50)]
        public virtual string OrderId { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        [MaxLength(20)]
        public virtual string PlateNumber { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(20)]
        public virtual string PhoneNumber { get; set; }
        /// <summary>
        /// 预订进场时间
        /// </summary>
        [MaxLength(20)]
        public virtual string ReserveEnterDateTime { get; set; }
        /// <summary>
        /// 停车时长
        /// </summary>
        [MaxLength(20)]
        public virtual string ReserveDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public virtual string OpeanId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int ParkId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
    }
}
