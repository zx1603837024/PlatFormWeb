using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataLogs.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllDataLogsOutput : PagedResultOutput<Abp.DataLog.Datalog>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DataLogDto> rows { get; set; }
    }
}
