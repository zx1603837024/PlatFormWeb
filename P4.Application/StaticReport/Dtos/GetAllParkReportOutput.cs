﻿using Abp.Application.Services.Dto;
using P4.Collectors.Dtos;
using P4.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
   public class GetAllParkReportOutput : PagedResultOutput<BerthsecReport>, IOutputDto
    {
        public List<GetParkTotalOutput> rows { get; set; }
    }

}