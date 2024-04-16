using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace P4.Inducibles.Dtos
{
    [AutoMapTo(typeof(Inducible))]
    public class InsertOrUpdateInducibleInput : EntityDto, IInputDto
    {
        /// <summary>
        /// 诱导名称
        /// </summary>
        [MaxLength(50)]
        public virtual string InducibleName { get; set; }

        /// <summary>
        /// 诱导类型
        /// 1：一级诱导
        /// 2：二级诱导
        /// 3：三级诱导
        /// 4：四级诱导
        /// </summary>
        public virtual Int16 InducibleType { get; set; }

        [MaxLength(80)]
        public virtual string X { get; set; }

        [MaxLength(80)]
        public virtual string Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
