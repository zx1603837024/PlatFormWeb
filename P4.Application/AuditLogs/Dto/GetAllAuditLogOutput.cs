using Abp.Application.Services.Dto;
using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AuditLogs.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllAuditLogOutput : PagedResultOutput<AuditLog>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AuditLogDto> rows { get; set; }
    }
}
