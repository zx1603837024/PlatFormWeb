using Abp.Application.Services;
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
    /// 收费员日志
    /// </summary>
    public class EmployeeCheckAppService : ApplicationService, IEmployeeCheckAppService
    {
        #region Var
        private readonly IRepository<EmployeeCheck, long> _abpEmployeeCheckRepository;
        private readonly IEmployeeCheckRepository _EmployeeCheckRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpEmployeeCheckRepository"></param>
        /// <param name="EmployeeCheckRepository"></param>
        public EmployeeCheckAppService(IRepository<EmployeeCheck, long> abpEmployeeCheckRepository, IEmployeeCheckRepository EmployeeCheckRepository)        
        {
            _abpEmployeeCheckRepository = abpEmployeeCheckRepository;
            _EmployeeCheckRepository = EmployeeCheckRepository;
        }

        /// <summary>
        /// 收费员日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllEmployeeCheckOutput GetAllEmployeeChecklist(Dtos.EmployeeCheckInput input)
        {
            return _EmployeeCheckRepository.GetAllEmployeeChecklist(input);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _EmployeeCheckRepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<EmployeeCheckDto> GetAllEmployeeCheckToplist(long Id)
        {
            return _EmployeeCheckRepository.GetAllEmployeeCheckToplist(Id);
        }
    }
}
