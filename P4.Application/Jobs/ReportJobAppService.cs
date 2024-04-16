using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Quartz;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.TestJobs;

namespace P4.Jobs
{
    public class ReportJobAppService : ApplicationService, IReportJobAppService
    {
        #region Var
        private readonly IRepository<Job> _jobAppService;
        #endregion


        public ReportJobAppService(IRepository<Job> jobAppService)
        {
            _jobAppService = jobAppService;
        }

        public void Run()
        {
            _jobAppService.Insert(new Job() { InTime = DateTime.Now, JobContent = "测试内容" + DateTime.Now });
        }
    }
}
