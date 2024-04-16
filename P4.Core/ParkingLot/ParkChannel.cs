using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Parks;
using P4.Regions;
using P4.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.ParkingLot
{
    [Table("AbpParkChannel")]
    public class ParkChannel : FullAuditedEntity<int, User>, IMustHaveTenant, IMustHaveCompany, IMustHavePark
    {
        /// <summary>
        /// 停车场Id
        /// </summary>
        public virtual int ParkId { get; set; }

        /// <summary>
        /// 停车场
        /// </summary>
        [ForeignKey("ParkId")]
        public virtual Park Park { get; set; }

        /// <summary>
        /// 停车场平台通道ID
        /// </summary>
        [MaxLength(50)]
        public virtual string ZhiBoChannelId { get; set; }

        /// <summary>
        /// 停车场类型
        /// </summary>
        [MaxLength(50)]
        public virtual string ChannelCode { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        [MaxLength(50)]
        public virtual string ChannelName { get; set; }

        /// <summary>
        /// 通道方向
        /// </summary>
        [MaxLength(10)]
        public virtual string ChannelDirection { get; set; }

        public virtual int TenantId { get; set; }

        public virtual int CompanyId { get; set; }
    }
}
