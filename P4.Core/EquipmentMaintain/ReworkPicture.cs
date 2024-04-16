using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentMaintain
{
    [Table("AbpReworkPicture")]
    public class ReworkPicture : Entity
    {
        /// <summary>
        /// 返修流程id
        /// </summary>
        public virtual int ReworkPictureId { get; set; }

        public byte[] Image { get; set; }
    }
}
