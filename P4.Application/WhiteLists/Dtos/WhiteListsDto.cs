using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WhiteLists.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(WhiteList))]
    public class WhiteListsDto : EntityDto<int>
    {

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarOwner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RelateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Rebate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
