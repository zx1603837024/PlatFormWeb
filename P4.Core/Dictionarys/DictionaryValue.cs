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
    /// <summary>
    /// 字典数据表
    /// </summary>
    [Table("AbpDictionaryValue")]
    public class DictionaryValue : Entity<int>, IMayHaveTenant, IPassivable
    {
        /// <summary>
        /// 字典值编码
        /// </summary>
        [MaxLength(30)]
        public virtual string ValueCode { get; set; }

        /// <summary>
        /// 字典类型Code
        /// 对应字典类型TypeCode
        /// </summary>
        [MaxLength(20)]
        public virtual string TypeCode { get; set; }

        /// <summary>
        /// 字典类型值
        /// </summary>
        [MaxLength(100)]
        protected virtual string TypeValue { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [MaxLength(50)]
        public virtual string ValueData { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 是否为默认值
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLengthAttribute(200)]
        public virtual string Remark { get; set; }
    }
}
