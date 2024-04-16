using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Berthsecs
{
    /// <summary>
    /// 泊位段表
    /// </summary>
    [Table("AbpBerthsecs")]
    public class Berthsec : FullAuditedEntity, IMustHaveTenant, IPassivable, IMustHavePark, IMustHaveRegion, IMustHaveCompany
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(38)]
        public virtual string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int BeginNumeber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int EndNumeber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(500)]
        public virtual string CustomNumeber { get; set; }


        /// <summary>
        /// 是否签到
        /// </summary>
        public virtual bool CheckInStatus { get; set; }

        /// <summary>
        /// 签到/签退   true/false
        /// </summary>
        public virtual bool CheckStatus { get; set; }

        /// <summary>
        /// 是否签退
        /// </summary>
        public virtual bool CheckOutStatus { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public virtual DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 签到人
        /// </summary>
        public virtual long? CheckInEmployeeId { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public virtual DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 签退人
        /// </summary>
        public virtual long? CheckOutEmployeeId { get; set; }

        /// <summary>
        /// 签到设备编号
        /// </summary>
        [MaxLength(40)]
        public virtual string CheckInDeviceCode { get; set; }

        /// <summary>
        /// 签退设备编号
        /// </summary>
        [MaxLength(40)]
        public virtual string CheckOutDeviceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string XPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string YPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 未被签到为false
        /// 被签到为true
        /// </summary>
        public virtual bool UseStatus { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 早班费率Id
        /// </summary>
        public virtual int RateId { get; set; }

        /// <summary>
        /// 中班费率Id
        /// </summary>
        public virtual int? RateId1 { get; set; }

        /// <summary>
        /// 晚班费率Id
        /// </summary>
        public virtual int? RateId2 { get; set; }

        /// <summary>
        /// 泊位段泊位数
        /// </summary>
        [MaxLength(30)]
        public virtual string BerthCount { get; set; }

        /// <summary>
        /// 是否需获取车检器状态
        /// true获取，false不获取
        /// </summary>
        public virtual bool PushStatus { get; set; }

        /// <summary>
        /// 坐标Lng
        /// </summary>
        [MaxLength(30)]
        public virtual string Lat { get; set; }

        /// <summary>
        /// 坐标Lng
        /// </summary>
        [MaxLength(30)]
        public virtual string Lng { get; set; }

        /// <summary>
        /// 通讯时间
        /// </summary>
        public virtual DateTime SignoCommunationTime { get; set; }

        /// <summary>
        /// 进口道闸数
        /// </summary>
        public virtual int? SignoInSize { get; set; }

        /// <summary>
        /// 出口道闸数
        /// </summary>
        public virtual int? SignoOutSize { get; set; }
    }
}
