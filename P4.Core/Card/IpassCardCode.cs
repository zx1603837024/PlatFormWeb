using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Card
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpIpassCardCodes")]
    public class IpassCardCode : Entity, IPassivable
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string Code { get; set; }
       

        /// <summary>
        /// 
        /// </summary>
        
       

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string Value { get; set; }

        public bool IsActive { get; set; }
    }
}
