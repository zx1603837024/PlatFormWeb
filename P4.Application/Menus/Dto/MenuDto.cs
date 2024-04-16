using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Menus.Dto
{
    [AutoMapFrom(typeof(Menu))]
    public class MenuDto : EntityDto<int>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string FatherCode { get; set; }

        public int Level { get; set; }

        public string LocalizableString { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public bool RequiresAuthentication { get; set; }

        public string RequiredPermissionName { get; set; }

        public int Order { get; set; }

        public List<string > promissName { get; set; }
    }
}
