﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OperationLog.Dtos
{
    public class GetAllOperationLogOutput : PagedResultOutput<Abp.DataLog.OperationLog>, IOutputDto
    {
        public List<OperationLogDto> rows { get; set; }
    }
}
