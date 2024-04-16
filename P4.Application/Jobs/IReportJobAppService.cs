using Abp.Application.Services;
using Abp.Quartz.Scheduler;

namespace P4.Jobs
{
    public interface IReportJobAppService : IApplicationService, IJobBase
    {

    }
}
