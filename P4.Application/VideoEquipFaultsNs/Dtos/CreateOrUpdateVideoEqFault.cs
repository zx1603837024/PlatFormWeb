using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoEquipFaultsNs.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(VideoEquips.VideoEquipFaults))]
    public class CreateOrUpdateVideoEqFault : EntityDto, IInputDto, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }
    }
}
