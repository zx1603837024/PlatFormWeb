using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Employees
{
    /// <summary>
    /// 收费员管理
    /// </summary>
    public class EmployeeManager : Abp.Authorization.Employees.AbpEmployeesManager<Employee>
    {
        public EmployeeManager(EmployeeStore employeeStore, IUnitOfWorkManager unitOfWorkManager, IIocResolver iocResolver, ISettingStore settingStore)
            : base(employeeStore, unitOfWorkManager, iocResolver, settingStore) 
        {
        
        }
    }
}
