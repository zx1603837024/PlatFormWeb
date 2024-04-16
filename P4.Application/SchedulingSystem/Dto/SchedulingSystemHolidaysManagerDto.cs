using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.SchedulingSystem.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(SchedulingSystemHolidaysManager))]
    public class SchedulingSystemHolidaysManagerDto : EntityDto
    {
        /// <summary>
        /// 假期名称
        /// </summary>
        public virtual string HolidaysName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual string BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual string EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
