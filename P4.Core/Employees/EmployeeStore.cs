using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Employees
{
    public class EmployeeStore : Abp.Authorization.Employees.AbpEmployeeStore<Employee>
    {
        public EmployeeStore(IRepository<Employee, long> employeeRepository, IAbpSession session, IUnitOfWorkManager unitOfWorkManager, ICacheManager cacheManager)
            : base(employeeRepository, session, unitOfWorkManager, cacheManager)
        {

        }
    }
}
