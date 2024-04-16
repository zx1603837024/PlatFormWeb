using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using P4.Employees.Dtos;
using P4.Users;
using P4.Users.Dto;
using P4.WorkGroups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WorkGroups.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(WorkGroup))]
    public class WorkGroupDto : EntityDto<int>
    {
        /// <summary>
        /// 工作組名稱
        /// </summary>
        [MaxLength(50)]
        public string WorkGroupName { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TenantName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 早班(1)，中班(2)，晚班(3)状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? LastModifierUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> Berthsecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<EmployeeDto> Employees { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string _employees = null;

        /// <summary>
        /// 
        /// </summary>
        private string _berthsec = null;

        /// <summary>
        /// 
        /// </summary>
        public string EmployeesString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_employees))
                    return _employees;
                if (Employees == null || Employees.Count == 0)
                    return null;
                foreach (var employee in Employees)
                {
                    _employees += employee.TrueName + ",";
                }
                return _employees.Substring(0, _employees.Length - 1);
            }
            set
            {
                _employees = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BerthSecsString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_berthsec))
                    return _berthsec;
                if (Berthsecs == null || Berthsecs.Count == 0)
                    return null;
                foreach (var berthsec in Berthsecs)
                {
                    _berthsec += berthsec.BerthsecName + ",";
                }
                return _berthsec.Substring(0, _berthsec.Length - 1);
            }

            set
            {
                _berthsec = value;
            }

        }
    }
}
