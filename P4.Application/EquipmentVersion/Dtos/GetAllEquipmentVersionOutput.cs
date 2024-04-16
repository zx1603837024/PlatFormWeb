using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentVersion.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllEquipmentVersionOutput : PagedResultOutput<P4.Equipments.EquipmentVersion>, IOperDto, IOutputDto
    {

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<EquipmentVersionDto> rows { get; set; }
    }
}
