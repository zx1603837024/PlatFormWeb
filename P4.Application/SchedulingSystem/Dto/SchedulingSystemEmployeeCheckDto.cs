using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SchedulingSystem.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(SchedulingSystemEmployeeCheck))]
    public class SchedulingSystemEmployeeCheckDto : EntityDto
    {
        /// <summary>
        /// 收费员编号
        /// </summary>
        public int EmployeesId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(30)]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string TrueName { get; set; }

        /// <summary>
        /// 泊位段id
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 上班路段
        /// </summary>
        [MaxLength(50)]
        public string BerthsecName { get; set; }

        /// <summary>
        /// 上班时间
        /// </summary>
        public DateTime WorkTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WorkTimeStr
        {
            get { return WorkTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 规定上班时间
        /// </summary>
        public string WorkRuleTime { get; set; }

        /// <summary>
        /// 上班时间是否异常
        /// </summary>
        public int WorkStatus { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public DateTime? OffTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OffTimeStr
        {
            get
            {
                if (OffTime.HasValue)
                {
                    return OffTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return "";
            }
        }

        /// <summary>
        /// 规定下班时间
        /// </summary>
        public string OffRuleTime { get; set; }

        /// <summary>
        /// 下班时间是否异常
        /// </summary>
        public int OffStatus { get; set; }

        /// <summary>
        /// 中途是否上下班
        /// </summary>
        public int HalfwayStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }
    }
}
