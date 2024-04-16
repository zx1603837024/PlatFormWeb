using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Menus.Dto
{
    public class TreeMenusOutput : IOutputDto
    {
        public List<MenuTreeDto> rows { get; set; }
    }
}
