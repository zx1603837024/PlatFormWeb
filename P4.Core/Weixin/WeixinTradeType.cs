using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpWeixinTradeTypes")]
    public class WeixinTradeType : Entity
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
