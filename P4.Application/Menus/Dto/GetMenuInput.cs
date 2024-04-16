using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Menus.Dto
{
    public class GetMenuInput : IInputDto
    {
        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string FatherCode { get; set; }


        public int TenantId { get; set; }
    }
}
