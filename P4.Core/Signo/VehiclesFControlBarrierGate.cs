using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Signo
{
    /// <summary>
    /// 道闸反控
    /// </summary>
    [Table("AbpVehiclesFControlBarrierGate")]
    public class VehiclesFControlBarrierGate : Entity<long>, IMustHaveCompany, IMustHaveTenant
    {
        /// <summary>
        /// 序号
        /// </summary>
        public virtual long SId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [MaxLength(50)]
        public virtual string SPicFile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(50)]
        public virtual string SMemo { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [MaxLength(50)]
        public virtual string SDatetime { get; set; }
        /// <summary>
        /// 状态 0升 1降
        /// </summary>
        public virtual int SStatus { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [MaxLength(50)]
        public virtual string SStatusStr { get; set; }
        /// <summary>
        /// 卡口名称
        /// </summary>
        [MaxLength(50)]
        public virtual string SInName { get; set; }
        /// <summary>
        /// 入场卡口编码
        /// </summary>
        [MaxLength(50)]
        public virtual string SInChnlIP { get; set; }
        /// <summary>
        /// 出场卡口编码
        /// </summary>
        [MaxLength(50)]
        public virtual string SOutChnlIP { get; set; }
        /// <summary>
        /// 入场车道名称
        /// </summary>
        [MaxLength(50)]
        public virtual string SInLaneName { get; set; }
        /// <summary>
        /// 入场车道编码
        /// </summary>
        [MaxLength(50)]
        public virtual string SInLaneNo { get; set; }
        /// <summary>
        /// 入场卡口状态
        /// </summary>
        public virtual int SInDevStatus { get; set; }
        /// <summary>
        /// 出场车道名称
        /// </summary>
        [MaxLength(50)]
        public virtual string SOutLaneName { get; set; }
        /// <summary>
        /// 出场车道编码
        /// </summary>
        [MaxLength(50)]
        public virtual string SOutLaneNo { get; set; }
        /// <summary>
        /// 出场卡口状态
        /// </summary>
        public virtual int SOutDevStatus { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public virtual int TenantId { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }
        
        /// <summary>
        /// 停车场
        /// </summary>
        public virtual string ParkId { get; set; }
    }
}
