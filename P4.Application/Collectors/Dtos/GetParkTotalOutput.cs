using Abp.Application.Services.Dto;
using P4.Parks.Dtos;
using P4.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    public class GetParkTotalOutput : GetReportTotalOutput, IOutputDto
    {
        public List<BerthsecReport> BerthsecReportList { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public virtual Int32 StopTime { get; set; }

        public int ParkId { get; set; }
        public string ParkName { get; set; }

    }
}
