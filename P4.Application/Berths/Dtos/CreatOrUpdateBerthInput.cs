using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berths.Dtos
{
    /// <summary>
    /// 
    /// </summary>
   public  class CreatOrUpdateBerthInput: EntityRequestInput, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorNumber { get; set; }

    }
}
