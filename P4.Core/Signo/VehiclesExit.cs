using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Signo
{
    /// <summary>
    /// 卡口信息/车道信息
    /// </summary>
    [Table("AbpVehiclesExit")]
    public class VehiclesExit : Entity<long>
    {
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int SDevId { get; set; }
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
        /// 编辑时间
        /// </summary>
        [MaxLength(50)]
        public virtual string SDevMonDate { get; set; }
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
        public virtual int? TenantId { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int? CompanyId { get; set; }
       
        /// <summary>
        /// 停车场ID
        /// </summary>
        public virtual int? ParkID { get; set; }
    }
}
