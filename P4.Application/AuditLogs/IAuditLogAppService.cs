using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.AuditLogs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AuditLogs
{
    public interface IAuditLogAppService : IApplicationService
    {
        ListResultOutput<AuditLogDto> GetAll(long Id);

        GetAllAuditLogOutput GetAll(SearchAuditLogInput input);
    }
}
