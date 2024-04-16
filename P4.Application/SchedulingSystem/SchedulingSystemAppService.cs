using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.SchedulingSystem.Dto;
using Abp.Linq.Extensions;
using System.Linq;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace P4.SchedulingSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class SchedulingSystemAppService : ApplicationService, ISchedulingSystemAppService
    {
        #region Var
        private readonly IRepository<SchedulingSystemEmployeeCheck> _schedulingSystemEmployeeCheckRepository;
        private readonly IRepository<SchedulingSystemHolidaysManager> _schedulingSystemHolidaysManagerRepository;
        private readonly IRepository<SchedulingSystemLeaveAdministration> _schedulingSystemLeaveAdministrationRepository;
        private readonly IRepository<SchedulingSystemScheduleDetail> _schedulingSystemScheduleDetailRepository;
        private readonly IRepository<SchedulingSystemSchedulePreferences> _schedulingSystemSchedulePreferencesRepository;
        private readonly IRepository<SchedulingSystemWorkScheme> _schedulingSystemWorkSchemeRepository;

        #endregion

       /// <summary>
       /// 
       /// </summary>
       /// <param name="schedulingSystemEmployeeCheckRepository"></param>
       /// <param name="schedulingSystemHolidaysManagerRepository"></param>
        public SchedulingSystemAppService(IRepository<SchedulingSystemEmployeeCheck> schedulingSystemEmployeeCheckRepository, IRepository<SchedulingSystemHolidaysManager> schedulingSystemHolidaysManagerRepository)
        {
            _schedulingSystemEmployeeCheckRepository = schedulingSystemEmployeeCheckRepository;
            _schedulingSystemHolidaysManagerRepository = schedulingSystemHolidaysManagerRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int DeleteHoliday(CreateOrUpdateHolidaysInput input)
        {
            _schedulingSystemHolidaysManagerRepository.Delete(input.Id);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int UpdateHoliday(CreateOrUpdateHolidaysInput input)
        {
            var entity = _schedulingSystemHolidaysManagerRepository.Load(input.Id);
            entity.BeginTime = input.BeginTime;
            entity.EndTime = input.EndTime;
            entity.IsActive = input.IsActive;
            entity.HolidaysName = input.HolidaysName;
            _schedulingSystemHolidaysManagerRepository.Update(entity);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSchedulingSystemEmployeeCheckOutput GetAllSchedulingSystemEmployeeCheckList(SearchSchedulingSystemEmployeeCheckInput input)
        {
            var records = _schedulingSystemEmployeeCheckRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllSchedulingSystemEmployeeCheckOutput()
            {
                rows = _schedulingSystemEmployeeCheckRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<SchedulingSystemEmployeeCheckDto>>(),
                records = records,
                total = records / input.rows + 1
            };
        }

        /// <summary>
        /// 排班节假日管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSchedulingSystemHolidaysManagerOutput GetAllSchedulingSystemHolidaysManagerList(SearchSchedulingSystemHolidaysManagerInput input)
        {
            var records = _schedulingSystemHolidaysManagerRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllSchedulingSystemHolidaysManagerOutput()
            {
                rows = _schedulingSystemHolidaysManagerRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<SchedulingSystemHolidaysManagerDto>>(),
                records = records,
                total = records / input.rows + 1
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSchedulingSystemLeaveAdministrationOutput GetAllSchedulingSystemLeaveAdministrationList(SearchSchedulingSystemLeaveAdministrationInput input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSchedulingSystemScheduleDetailOutput GetAllSchedulingSystemScheduleDetailList(SearchSchedulingSystemScheduleDetailInput input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int InsertHoliday(CreateOrUpdateHolidaysInput input)
        {
            SchedulingSystemHolidaysManager entity = new SchedulingSystemHolidaysManager()
            {
                HolidaysName = input.HolidaysName,
                BeginTime = input.BeginTime,
                EndTime = input.EndTime,
                IsActive = true
            };
            _schedulingSystemHolidaysManagerRepository.Insert(entity);
            return 1;
        }
    }
}
