using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ExportCommon
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpExportCodes")]
    public class ExportCode : Entity, IPassivable
    {

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public virtual string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
