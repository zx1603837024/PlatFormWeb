using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace P4.Users.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDtoOfJqGrid<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? LastModifierUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  ICollection<UserRole> Roles { get; set; }

        /// <summary>
        /// 
        /// </summary>
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

            set {
                _roleDisplayName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TenantName { get; set; }
        
      
        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName{ get;  set;}

        /// <summary>
        /// 
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        

    }
}
