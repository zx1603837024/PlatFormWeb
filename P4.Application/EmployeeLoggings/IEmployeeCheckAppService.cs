using Abp.Application.Services;
using P4.EmployeeLoggings.Dtos;
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
    public interface IEmployeeCheckAppService : IApplicationService
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);
    }
}
