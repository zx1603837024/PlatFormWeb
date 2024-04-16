using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace P4.OperationPermissions.Dtos
{
    [AutoMapFrom(typeof(OperationPermission))]
    public class OperationPermissionsDto : EntityDto
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
        public string Description { get; set; }

        /// <summary>
        /// 权限是否属于host或者tenant
        /// </summary>
        public Int16 MultiTenancySides { get; set; }

        /// <summary>
        /// 父类编码
        /// 如果为0代表父类
        /// </summary>
        public string FatherCode { get; set; }

        /// <summary>
        /// 是否为功能权限
        /// true为功能权限
        /// false为操作权限
        /// </summary>
        public bool IsFunction { get; set; }
    }
}
