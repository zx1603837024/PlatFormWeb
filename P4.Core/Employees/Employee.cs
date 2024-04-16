using Abp.Authorization.Employees;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Employees
{
    /// <summary>
    /// 收费员表
    /// </summary>
    public class Employee : AbpEmployee
    {
        /// <summary>
        /// 后台签退用户
        /// </summary>
        [ForeignKey("CheckOutUserId")]
        public virtual Users.User User { get; set; }

        /// <summary>
        /// 收费员类型
        /// 1：道路停车收费员
        /// 2：岗亭收费员
        /// </summary>
        public virtual  int EmployeeType { get; set; }
    }
}
