using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataLogs.Dtos
{
    public class GetAllDataLogItemsOutput : PagedResultOutput<Abp.DataLog.Datalog>, IOutputDto
    {
        public List<DataLogItemDto> rows { get; set; }
    }
}
