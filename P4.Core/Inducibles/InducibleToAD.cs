using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inducibles
{
    [Table("AbpInducibleToADs")]
    public class InducibleToAD : Entity, IPassivable
    {
        public virtual int InducibleId { get; set; }

        /// <summary>
        /// 广告语
        /// </summary>
        [MaxLength(500)]
        public virtual string AD { get; set; }


        public virtual DateTime? BeginTime { get; set; }


        public virtual DateTime? EndTime { get; set; }


        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 广告诱导设备编号
        /// </summary>
        [MaxLength(40)]
        public virtual string EquipmentId { get; set; }
    }
}
