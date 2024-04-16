using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Regions;
using P4.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Parks
{
    [Table("AbpParks")]
    public class Park : FullAuditedEntity<int, User>, IMustHaveTenant, IMustHaveCompany
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        public virtual int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("RegionId")]
        public virtual Region RegionModel { get; set; }

        /// <summary>
        /// 停车场
        /// </summary>
        [MaxLength(30)]
        public virtual string ParkName { get; set; }

        /// <summary>
        /// 停车场类型
        /// 1：路侧停车场
        /// 2：封闭式停车场
        /// </summary>
        public virtual Int16 ParkType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public virtual string Mark { get; set; }

        public virtual int BerthCount { get; set; }

        public virtual int TenantId { get; set; }

        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }


        /// <summary>
        /// X
        /// </summary>
        [MaxLength(30)]
        public virtual string X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        [MaxLength(30)]
        public virtual string Y { get; set; }

        /// <summary>
        /// 泊位开始编号
        /// </summary>
        [MaxLength(10)]
        public virtual string BeginNumber { get; set; }
        
        /// <summary>
        /// 泊位结束编号
        /// </summary>
        [MaxLength(10)]
        public virtual string EndNumber { get; set; }

        /// <summary>
        /// 其他编号
        /// 用,隔开
        /// </summary>
        [MaxLength(500)]
        public virtual string OtherNumber { get; set; }

        /// <summary>
        /// 智博停车场ID
        /// </summary>
        [MaxLength(50)]
        public virtual string ZhiBoParkId { get; set; }
    }
}
