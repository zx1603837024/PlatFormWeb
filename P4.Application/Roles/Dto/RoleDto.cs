using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Authorization;
using System;

namespace P4.Roles.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Role))]
    public class RoleDto : EntityDto<int>
    {
      
        //TenantId, Name, DisplayName, LastModificationTime, LastModifierUserId, CreationTime, CreatorUserId, IsStatic, IsDefault
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int TenantId { get; set; }

        /// <summary>
        /// 分公司id
        /// </summary>
        public int? CompanyId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public int LastModifierUserId { get; set; }

        public DateTime CreationTime { get; set; }

        public long CreatorUserId { get; set; }

        public bool IsStatic { get; set; }

        public bool IsDefault { get; set; }


        public string TenantName { get; set; }

        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }
    }

    /// <summary>
    /// 角色
    /// </summary>
    [AutoMapFrom(typeof(Role))]
    public class RoleDto_TotalCount: RoleDto
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
