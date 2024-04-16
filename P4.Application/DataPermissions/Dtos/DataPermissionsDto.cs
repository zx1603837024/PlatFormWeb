using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.Authorization;
using Abp.Authorization.DataPermissions;

namespace P4.DataPermissions.Dtos
{
    [AutoMapFrom(typeof(DataPermissionSetting))]
    public class DataPermissionsDto : EntityDto<long>
    {

        
        [MaxLength(200)]
        public string CompanyIds { get; set; }

        /// <summary>
        /// 区域数据权限
        /// </summary>
        [MaxLength(200)]
        public string RegionIds { get; set; }

        /// <summary>
        /// 停车场数据权限
        /// </summary>
        [MaxLength(200)]
        public string ParkIds { get; set; }

        /// <summary>
        /// 泊位段数据权限
        /// </summary>
        [MaxLength(400)]
        public string BerthsecIds { get; set; }

        public int TenantId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        private string _roleDisplayName = null;
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleDisplayName
        {

            get
            {
                if (!string.IsNullOrWhiteSpace(_roleDisplayName))
                    return _roleDisplayName;
                if (Roles == null)
                    return null;
                foreach (var x in Roles)
                {
                    _roleDisplayName += x.RoleId + ",";

                }

                return _roleDisplayName.Substring(0, _roleDisplayName.Length - 1);
            }

            set
            {
                _roleDisplayName = value;
            }

        }
        public string TenantName { get; set; }



        public string LastModifierUserName { get; set; }
        public string CreatorUserName { get; set; }

        public bool IsUserPermission { get; set; }
    }
}
