using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using P4.Parks.Dtos;


namespace P4.Inducibles.Dtos
{
    [AutoMapFrom(typeof(Inducible))]

    public class InducibleDto : EntityDto
    {
        /// <summary>
        /// 诱导名称
        /// </summary>
        [MaxLength(50)]
        public virtual string InducibleName { get; set; }
        /// <summary>
        /// 诱导类型
        /// 1：一级诱导  2：二级诱导  3：三级诱导  4：四级诱导
        /// </summary>
        public virtual Int16 InducibleType { get; set; }

        [MaxLength(80)]
        public virtual string X { get; set; }

        [MaxLength(80)]
        public virtual string Y { get; set; }

        [MaxLength(100)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        public ICollection<InducibleToParkDto> parklist { get; set; }

        public virtual ICollection<InducibleToAD> advert { get; set; }
        /// <summary>
        /// 诱导设备编号
        /// </summary>
        [MaxLength(40)]
        public virtual string EquipmentId { get; set; }
       
    }
}
