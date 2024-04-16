using Abp.Domain.Repositories;
using P4.BigScreen.Dtos;
using P4.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Employees
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee, long>
    {
        /// <summary>
        /// 获取收费员坐标
        /// </summary>
        /// <returns></returns>
        List<EmpolyeePoint> GetEmpolyeePoints();

        /// <summary>
        /// 获取单收费员坐标
        /// </summary>
        /// <returns></returns>
        EmpolyeePoint GetEmpolyeePointModel(int Employeeid);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeOutput GetEmployeeAll(EmployeeInput input);

        /// <summary>
        /// 收费员签到
        /// </summary>
        void UpdateEmployeeCheckIn(int employeeID, int ParkID, bool checkIn, DateTime checkInOrOutTime, bool checkOut, string DeviceCode, int berthsecId, int companyId, int tenantId);

        /// <summary>
        /// 收费员签退
        /// </summary>
        void UpdateEmployeeCheckOut(int employessId);
        /// <summary>
        /// 后台收费员批量签退
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int EmployeeBatchCheckoutBGO(List<int> Ids, long userId);
        /// <summary>
        /// 后台收费员取消对账
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        int EmployeeNOAccountCheckOutBGO(int Id,long operatorId);
    }
}
