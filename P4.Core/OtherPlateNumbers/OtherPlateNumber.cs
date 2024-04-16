using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.OtherAccounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherPlateNumbers
{
    [Table("ExtOtherPlateNumber")]
    public class OtherPlateNumber : Entity<long>, IFullAudited, IPassivable
    {
        public virtual long AssignedOtherAccountId { get; set; }

        /// <summary>
        /// 车牌列表
        /// </summary>
        [ForeignKey("AssignedOtherAccountId")]
        public virtual OtherAccount OtherAccountRelateNumber { get; set; }

        [MaxLength(8)]
        public virtual string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CarColor { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public virtual int CarType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual long? DeleterUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}
