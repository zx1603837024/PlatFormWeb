using Abp.Application.Services;
using P4.SchedulingSystem.Dto;

namespace P4.SchedulingSystem
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISchedulingSystemAppService : IApplicationService
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        GetAllSchedulingSystemEmployeeCheckOutput GetAllSchedulingSystemEmployeeCheckList(SearchSchedulingSystemEmployeeCheckInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSchedulingSystemHolidaysManagerOutput GetAllSchedulingSystemHolidaysManagerList(SearchSchedulingSystemHolidaysManagerInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int InsertHoliday(CreateOrUpdateHolidaysInput input);

        /// <summary>
        /// 删除假期管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int DeleteHoliday(CreateOrUpdateHolidaysInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int UpdateHoliday(CreateOrUpdateHolidaysInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSchedulingSystemLeaveAdministrationOutput GetAllSchedulingSystemLeaveAdministrationList(SearchSchedulingSystemLeaveAdministrationInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSchedulingSystemScheduleDetailOutput GetAllSchedulingSystemScheduleDetailList(SearchSchedulingSystemScheduleDetailInput input);
    }
}
