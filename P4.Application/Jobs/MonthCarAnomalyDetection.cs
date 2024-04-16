using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Helper;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class MonthCarAnomalyDetection: PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        #region Var
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IRepository<MonthlyCars.MonthlyCar> _monthRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="monthRepository"></param>
        /// <param name="backgroundJobManager"></param>
        public MonthCarAnomalyDetection(AbpTimer timer, IRepository<MonthlyCars.MonthlyCar> monthRepository, IBackgroundJobManager backgroundJobManager)
        : base(timer)
        {
            _monthRepository = monthRepository;
            _backgroundJobManager = backgroundJobManager;
            Timer.Period = 60000;
        }


        /// <summary>
        /// 
        /// </summary>
        [UnitOfWork]
        protected override void DoWork()
        {
            var monthModel = Abp.DataProcessHelper.GetEntityFromTable<MonthlyCars.MonthlyCar>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "select PlateNumber from AbpMonthlyCars where IsDeleted = 0").Tables[0]);

           // _backgroundJobManager.Enqueue<MonthyProcessJob, List<MonthlyCars.MonthlyCar>>(monthModel);


        }
    }
}
