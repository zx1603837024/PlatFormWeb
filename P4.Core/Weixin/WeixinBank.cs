using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{

    /// <summary>
    /// 
    /// </summary>
    [Table("AbpWeixinBanks")]
    public class WeixinBank : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string Value { get; set; }
    }
}
