using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Icons
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpIcons")]
    public class Icon : Entity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string IconUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Test { get; set; }
    }
}
