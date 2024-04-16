using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpEscapeGuids")]
    public class EscapeGuid : Entity<int>, IPassivable
    {

        /// <summary>
        /// guid 集合
        /// </summary>
        [MaxLength(800)]
        public string Guids { get; set; }

        /// <summary>
        /// 是否已支付
        /// </summary>
        public bool IsActive { get; set; }
    }
}
