using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OperationPermissions
{
    /// <summary>
    /// 操作权限列表
    /// </summary>
    [Table("AbpOperationPermissions")]
    public class OperationPermission : Entity<int>
    {
        /// <summary>
        /// 操作权限名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 是否默认授权
        /// </summary>
        public bool IsGrantedByDefault { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 权限是否属于host或者tenant
        /// </summary>
        public Int16 MultiTenancySides { get; set; }

        [MaxLength(50)]
        public string FatherCode { get; set; }

        /// <summary>
        /// true is Function
        /// false is Operation
        /// </summary>
        public bool IsFunction { get; set; }
    }
}
