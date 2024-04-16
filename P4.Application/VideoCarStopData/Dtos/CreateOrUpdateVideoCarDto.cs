using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoCarStopData.Dtos
{
    [AutoMapTo(typeof(VideoCars.VideoCar))]
    public class CreateOrUpdateVideoCarDto
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
    }
}
