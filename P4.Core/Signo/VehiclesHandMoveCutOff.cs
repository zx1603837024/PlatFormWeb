using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Signo
{
    /// <summary>
    /// 手动开闸记录列表
    /// </summary>
    [Table("AbpVehiclesHandMoveCutOff")]
    public class VehiclesHandMoveCutOff : Entity<long>, IMustHaveCompany, IMustHaveTenant
    {
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int SRowID { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [MaxLength(50)]
        public virtual string SDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(50)]
        public virtual string SMemo { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        [MaxLength(50)]
        public virtual string SUserNo { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        [MaxLength(50)]
        public virtual string SUserName { get; set; }
        /// <summary>
        /// 开闸类型
        /// </summary>
        [MaxLength(50)]
        public virtual string STypeName { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        [MaxLength(50)]
        public virtual string SPlateNo { get; set; }
        /// <summary>
        /// 车主姓名
        /// </summary>
        [MaxLength(50)]
        public virtual string SCustName { get; set; }
        /// <summary>
        /// 入口通道名称
        /// </summary>
        [MaxLength(50)]
        public virtual string SInLaneName { get; set; }
        /// <summary>
        /// 入口通道编码
        /// </summary>
        [MaxLength(50)]
        public virtual string SInLaneNo { get; set; }
        /// <summary>
        /// 出口通道名称
        /// </summary>
        [MaxLength(50)]
        public virtual string SOutLaneName { get; set; }
        /// <summary>
        /// 出口通道编码
        /// </summary>
        [MaxLength(50)]
        public virtual string SOutLaneNo { get; set; }
        /// <summary>
        /// 是否黑名单
        /// </summary>
        [MaxLength(50)]
        public virtual string SIsBackCar { get; set; }
        /// <summary>
        /// 入口/出口
        /// </summary>
        [MaxLength(50)]
        public virtual string SInOutType { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public virtual int TenantId { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int ParkId { get; set; }
    }
}
