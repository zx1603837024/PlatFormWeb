using Abp.Application.Services;
using P4.OperationLog.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OperationLog
{
    public interface IOperationLogAppService : IApplicationService
    {
        GetAllOperationLogOutput GetAllOperationLogList(OperationLogInput input);

        GetAllOperationLogOutput GetOperationLogToTopList(int count);

        void DeleteOperationLog(long Id);
    }
}
