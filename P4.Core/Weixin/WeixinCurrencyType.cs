using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{
    [Table("AbpWeixinCurrencyTypes")]
    public class WeixinCurrencyType : Entity
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
