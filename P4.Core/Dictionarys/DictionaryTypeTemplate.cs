using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys
{
    [Table("AbpDictionaryTypeTemplate")]
    public class DictionaryTypeTemplate : Entity<int>
    {
        /// <summary>
        /// 字典类型编号
        /// 相对单个商户的唯一性
        /// </summary>
        [MaxLength(20)]
        public virtual string TypeCode { get; set; }

        /// <summary>
        /// 类型值
        /// </summary>
        [MaxLengthAttribute(30)]
        public virtual string TypeValue { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 商户Id
        /// 如果商户id为1，用于替换商户id
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 是否为默认值
        /// true：记录不能修改、删除
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLengthAttribute(200)]
        public virtual string Remark { get; set; }
    }
}
