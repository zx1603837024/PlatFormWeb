using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Permissions.Dto
{
    [AutoMapFrom(typeof(PermissionSetting))]
    public class CreateOrUpdateRolePermissionsInput : EntityRequestInput, IOperDto
    {
        public string oper { get; set; }
        public string Name
        { get; set; }

        public bool IsGranted
        { get; set; }


        public int RoleId
        { get; set; }

        public Int32 UserId
        { get; set; }

        public string Discriminator
        { get; set; }

        public int MultiTenancySide
        { get; set; }
       
    }
}
