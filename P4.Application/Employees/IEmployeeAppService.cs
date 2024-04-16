using Abp.Application.Services;
using P4.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Employees;

namespace P4.Employees
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeAppService : IApplicationService
    {
        /// <summary>
        /// 获取收费员管理列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeOutput GetAllEmployeeList(EmployeeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string EmployeeInsert(CreatOrUpdateEmployee input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string EmployeeUpdate(CreatOrUpdateEmployee input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void EmployeeDelete(CreatOrUpdateEmployee input);
        /// <summary>
        /// 返回所有的收费员
        /// </summary>
        /// <returns></returns>
        List<EmployeeDto> GetEmployeeAll();

        /// <summary>
        /// 收费员签到签退添加历史记录
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="ParkID"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkInOrOutTime"></param>
        /// <param name="checkOut"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="BerthsecId"></param>
        void InAndOutEmployeeCheck(int employeeID, int ParkID, bool checkIn, DateTime checkInOrOutTime, bool checkOut , string DeviceCode, int BerthsecId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="ParkID"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkInOrOutTime"></param>
        /// <param name="checkOut"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="berthsecId"></param>
        /// <param name="companyId"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task UpdateEmployeeCheckIn(int employeeID, int ParkID, bool checkIn, DateTime checkInOrOutTime, bool checkOut, string DeviceCode, int berthsecId, int companyId, int tenantId);
        /// <summary>
        /// 根据收费员Id获取收费员信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        void GetEmployeeById(int EmployeeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        EmployeeDto GetEmployeeByUserName(string UserName);

        /// <summary>
        /// 修改收费员信息
        /// </summary>
        /// <param name="employee"></param>
        Task UpdateEmployee(Employee employee);
        /// <summary>
        /// 根据用户名密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="tenantid"></param>
        /// <returns></returns>
        Employee GetEmployeeByNameAndPassword(string username, string password,int tenantid);

        /// <summary>
        /// 批量签退
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        int EmployeeBatchCheckoutBGO(List<int> Ids);

        /// <summary>
        /// 取消对账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int EmployeeNOAccountCheckOutBGO(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<EmployeeSelectDto> GetEmployeeSelectList();

        /// <summary>
        /// 获取收费员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        EmployeeDto Load(long Id);

        /// <summary>
        /// 获取收费员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        EmployeeDto GetEmployeeDto(long Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        EmployeePDADto LoadPDAEmployee(long Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<EmployeeDto> GetEmployeeGps(int Id);

        /// <summary>
        /// 通过巡查账号获取收费员定位
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        List<EmployeeDto> GetEmployeeGps(long InspectorId);
    }
}
