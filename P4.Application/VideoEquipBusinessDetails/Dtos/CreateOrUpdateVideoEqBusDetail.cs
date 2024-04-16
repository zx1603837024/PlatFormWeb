using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace P4.VideoEquipBusinessDetails.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(VideoEquips.VideoEquipBusinessDetail))]
    public class CreateOrUpdateVideoEqBusDetail 
    {
        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }

        public string id { get; set; }

        public int? PostState { get; set; }
    }
}
