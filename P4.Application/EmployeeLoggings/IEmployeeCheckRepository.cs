using Abp.Domain.Repositories;
using P4.EmployeeLoggings.Dtos;
using P4.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EmployeeLoggings
{ 

    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeCheckRepository : IRepository<EmployeeCheck, long>
    {
        /// <summary>
        /// 收费员日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeCheckOutput GetAllEmployeeChecklist(EmployeeCheckInput input);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<EmployeeCheckDto> GetAllEmployeeCheckToplist(long Id);

    }
}
