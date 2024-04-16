using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchSensorBeatInput : IInputDto, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime OperateDateBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime OperateDateEnd { get; set; }

        public virtual string filters { get; set; }
    }
}
