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
    [Table("AbpParkingMonthlyRent")]
    public class ParkingMonthlyRent:FullAuditedEntity<int>,IMustHavePark,IMustHaveTenant,IMustHaveCompany
    {
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
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int CarType { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [ForeignKey("ParkId")]
        public virtual Park Park { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 分公司ID
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        [ForeignKey("TenantId")]
        public virtual MultiTenancy.Tenant Tenant { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Companys.OperatorsCompany Company { get; set; }
    }
}
