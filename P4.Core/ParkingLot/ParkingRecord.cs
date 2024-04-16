using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Employees;
using P4.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace P4.ParkingLot
{
    /// <summary>
    /// 停车场停车记录
    /// </summary>
    [Table("AbpParkingRecord")]
    public class ParkingRecord : FullAuditedEntity<long>, IMustHaveTenant, IMustHaveCompany, IMustHavePark,IMustHaveRegion
    {
        /// <summary>
        /// Guid 业务ID
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 停车场平台ID
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 分公司ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 停车场ID
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 停车场名称
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 入场通道名称
        /// </summary>
        public string CarInChannelName { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public short OrderStatus { get; set; }

        /// <summary>
        /// 车辆驶入时间
        /// </summary>
        public DateTime? CarInTime { get; set; }

        /// <summary>
        /// 车辆驶出时间
        /// </summary>
        public DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 出场通道名称
        /// </summary>
        public string CarOutChannelName { get; set; }

        /// <summary>
        /// 车辆类型 月卡 临时车
        /// </summary>
        public int? CarType { get; set; }

        /// <summary>
        /// 停车时间（分钟）
        /// </summary>
        public int? StopTime { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal? PayableAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal? FactReceive { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal? DiscountMoney { get; set; }

        /// <summary>
        /// 折扣方式
        /// </summary>
        public short? DiscountType { get; set; }

        /// <summary>
        /// 折扣来源ID
        /// </summary>
        public long? DiscountSourceID { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public short? PayStatus { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public short? PayType { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayOrderBatchNo { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 入场通道
        /// </summary>
        public int? CarInChannelId { get; set; }

        /// <summary>
        /// 出场通道
        /// </summary>
        public int? CarOutChannelId { get; set; }

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

        /// <summary>
        /// 区域ID
        /// </summary>
        public virtual int RegionId { get;set; }

        /// <summary>
        /// 区域
        /// </summary>
        [ForeignKey("RegionId")]
        public virtual Regions.Region Region { get; set; }
    }
}
