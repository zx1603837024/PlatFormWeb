using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Authorization;
using System.Data;
using Abp.Domain.Uow;
using P4.SubscribeManager;
using P4.Berthsecs;

namespace P4.Employees
{
    /// <summary>
    /// 收费员管理
    /// </summary>
    public class EmployeeAppService : ApplicationService, IEmployeeAppService
    {
        #region Var
        private readonly IRepository<Employee, long> _abpEmployeeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeManager _abpEmployeesManager;
        private readonly IRepository<EmployeeCheck, long> _abpEmployeeCheckRepository;//收费员签到签退
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISubscribeAppService _subscribeAppService;
        private readonly IRepository<Berthsec> _berthsecAppService;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpEmployeeRepository"></param>
        /// <param name="employeeRepository"></param>
        /// <param name="abpEmployeesManager"></param>
        /// <param name="abpEmployeeCheckRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public EmployeeAppService(IRepository<Berthsec> berthsecAppService, IRepository<Employee, long> abpEmployeeRepository, IEmployeeRepository employeeRepository, EmployeeManager abpEmployeesManager,
            IRepository<EmployeeCheck, long> abpEmployeeCheckRepository, IUnitOfWorkManager unitOfWorkManager, ISubscribeAppService subscribeAppService)
        {
            _berthsecAppService = berthsecAppService;
            _abpEmployeeRepository = abpEmployeeRepository;
            _employeeRepository = employeeRepository;
            _abpEmployeesManager = abpEmployeesManager;
            _abpEmployeeCheckRepository = abpEmployeeCheckRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _subscribeAppService = subscribeAppService;
        }

        /// <summary>
        /// 获取收费员管理列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeOutput GetAllEmployeeList(EmployeeInput input)
        {
            return _employeeRepository.GetEmployeeAll(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string EmployeeInsert(CreatOrUpdateEmployee input)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveCompany))
            {
                if (_abpEmployeeRepository.FirstOrDefault(dic => dic.UserName == input.UserName) != default(Employee))
                {
                    return input.UserName + "已经存在";
                }
                if (_abpEmployeeRepository.FirstOrDefault(dic => dic.TrueName == input.TrueName) != default(Employee))
                {
                    return input.TrueName + "已经存在";
                }
            }

            if (string.IsNullOrWhiteSpace(input.CompanyName))
            {
                return "分公司名称不能为空";
            }

            Employee entity = new Employee();
            entity.UserName = input.UserName;
            entity.TrueName = input.TrueName;
            entity.Password = input.Password;
            entity.Address = input.Address;
            entity.BankCard = input.BankCard;
            entity.AccountStatus = input.AccountStatus;
            entity.Telephone = input.Telephone;
            entity.CheckOut = input.CheckOut;
            entity.CompanyId = int.Parse(input.CompanyName);
            _abpEmployeeRepository.Insert(entity);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  新增了 " + entity.UserName + "收费员数据", "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string EmployeeUpdate(CreatOrUpdateEmployee input)
        {
            Employee entity = null;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveCompany))
            {
                entity = _abpEmployeeRepository.Load(input.Id);
                if (_abpEmployeeRepository.FirstOrDefault(dic => dic.UserName == input.UserName && dic.Id != input.Id) != default(Employee))
                {
                    return input.UserName + "已经存在";
                }
                if (_abpEmployeeRepository.FirstOrDefault(dic => dic.TrueName == input.TrueName && dic.Id != input.Id) != default(Employee))
                {
                    return input.TrueName + "已经存在";
                }
            }

            //签退
            if (input.CheckOut == true && entity.CheckOut == false && entity.CheckIn == true)
            {
                entity.CheckOutTime = DateTime.Now;
                entity.CheckIn = false;
                entity.CheckOut = true;
                entity.CheckOuttype = "2";
                entity.CheckOutUserId = AbpSession.UserId;
            }

            if (string.IsNullOrWhiteSpace(input.CompanyName))
            {
                return "分公司名称不能为空";
            }

            entity.UserName = input.UserName;
            entity.TrueName = input.TrueName;
            entity.Password = input.Password;
            entity.Address = input.Address;
            entity.BankCard = input.BankCard;
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.AccountStatus = input.AccountStatus;
            entity.Telephone = input.Telephone;
            _abpEmployeeRepository.Update(entity);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  修改了 " + entity.UserName + "收费员数据", "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void EmployeeDelete(CreatOrUpdateEmployee input)
        {
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  删除了 " + input.UserName + "收费员数据", "");
            _abpEmployeeRepository.Delete(input.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeAll()
        {
            return _employeeRepository.GetAllList().MapTo<List<EmployeeDto>>();
        }



        /// <summary>
        /// 收费员签到
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<Guid> EmployeeCheck(string DeviceCode)
        {

            var time = DateTime.Now;
            var entity = _employeeRepository.Load(AbpSession.UserId.Value);
            entity.BerthsecId = string.Join(",", AbpSession.BerthsecIds);
            entity.CheckIn = true;
            entity.CheckInTime = time;
            entity.ParkId = string.Join(",", AbpSession.ParkIds);

            var employee = await _employeeRepository.UpdateAsync(entity);

            EmployeeCheck employeecheck = new EmployeeCheck()
            {
                BerthsecId = employee.BerthsecId,
                EmployeeId = employee.Id,
                ParkId = employee.ParkId,
                CheckIn = true,
                CheckInTime = time,
                DeviceCode = DeviceCode
            };

            await _abpEmployeeCheckRepository.InsertAsync(employeecheck);
            return Guid.NewGuid();
        }

        /// <summary>
        /// 收费员签退 修改EmployeeCheck记录
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="ParkID"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkInOrOutTime"></param>
        /// <param name="checkOut"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="BerthsecId"></param>
        public void InAndOutEmployeeCheck(int employeeID, int ParkID, bool checkIn, DateTime checkInOrOutTime, bool checkOut, string DeviceCode, int BerthsecId)
        {
            //根据收费员ID 和 签退时间为null 修改记录
            var employeeCheck = _abpEmployeeCheckRepository.GetAllList(a => a.EmployeeId == employeeID && a.CheckOutTime == null).OrderByDescending(entity => entity.Id).ToList();
            EmployeeCheck employeemodel = new EmployeeCheck(); 
            if (employeeCheck != null && employeeCheck.Count > 0)
            {
                employeemodel = employeeCheck[0];
                employeemodel.CheckIn = checkIn;
                employeemodel.CheckOut = checkOut;
                employeemodel.CheckOutTime = checkInOrOutTime;
                _abpEmployeeCheckRepository.Update(employeemodel);
            }
        }
        /// <summary>
        /// 根据收费员Id获取收费员信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public void GetEmployeeById(int EmployeeId)
        {
            //Employee emp = new Employee();
            //emp = _abpEmployeeRepository.FirstOrDefault(entity => entity.Id == EmployeeId);
            //return emp;
            _employeeRepository.UpdateEmployeeCheckOut(EmployeeId);  //执行sql语句签退收费员信息
        }
        /// <summary>
        /// 修改收费员信息
        /// </summary>
        /// <param name="employee"></param>
        public async Task UpdateEmployee(Employee employee)   
        {
            if (employee != null)
            {
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany);
                await _abpEmployeeRepository.UpdateAsync(employee);
            }
        }
        /// <summary>
        /// 根据用户名密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Employee GetEmployeeByNameAndPassword(string username, string password, int tenantid)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany);
            Employee emp = new Employee();
            emp = _abpEmployeeRepository.FirstOrDefault(entity => entity.UserName == username && entity.Password == password && entity.TenantId == tenantid);
            return emp;
        }

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
        public async Task UpdateEmployeeCheckIn(int employeeID, int ParkID, bool checkIn, DateTime checkInOrOutTime, bool checkOut, string DeviceCode, int berthsecId, int companyId, int tenantId)
        {
            _employeeRepository.UpdateEmployeeCheckIn(employeeID, ParkID, checkIn, checkInOrOutTime, checkOut, DeviceCode, berthsecId, companyId, tenantId);
        }

        /// <summary>
        /// 批量签退
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int EmployeeBatchCheckoutBGO(List<int> Ids)
        {
            return _employeeRepository.EmployeeBatchCheckoutBGO(Ids, AbpSession.UserId.Value);
        }

        /// <summary>
        /// 取消对账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int EmployeeNOAccountCheckOutBGO(int Id)
        {
            return _employeeRepository.EmployeeNOAccountCheckOutBGO(Id, AbpSession.UserId.Value);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeSelectDto> GetEmployeeSelectList()
        {
            return _abpEmployeeRepository.GetAllList(entity => entity.AccountStatus == "1").MapTo<List<EmployeeSelectDto>>();
        }

        /// <summary>
        /// 获取收费员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EmployeeDto Load(long Id)
        {
            return _employeeRepository.Load(Id).MapTo<EmployeeDto>();
        }

        /// <summary>
        /// 获取收费员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EmployeeDto GetEmployeeDto(long Id)
        {
            var model = _employeeRepository.FirstOrDefault(Id);
            if(model == null)
            {
                return null;
            }
            else
            {
                return model.MapTo<EmployeeDto>();
            }
        }

        /// <summary>
        /// 获取收费员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EmployeePDADto LoadPDAEmployee(long Id)
        {
            string sql = "select *,Gps.X as Latitude,Gps.Y as Longitude from AbpEmployees left join (select * from AbpEquipmentGps with(nolock) where id in (select max(id) as Id from AbpEquipmentGps group by CreatorUserId)) as Gps on AbpEmployees.Id = Gps.CreatorUserId  where IsDeleted = 0 and AbpEmployees.id = " + Id;
            return Abp.DataProcessHelper.GetEntityFromTable<EmployeePDADto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0])[0];
        }

        /// <summary>
        /// 收费员地图
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeGps(int Id)
        {
            string sqlwhere = " and AbpEmployees.CompanyId in (" + string.Join(",", AbpSession.CompanyIds) + ")";
            if (Id != 0)
                sqlwhere += " and  AbpEmployees.id = " + Id;
            string sql = "select AbpEmployees.*, X, Y, Convert(varchar(19), Gps.CreationTime, 121) as UpLastTime, AbpEquipments.Version from AbpEmployees left join (select * from AbpEquipmentGps with(nolock) where id in (select max(id) as Id from AbpEquipmentGps group by CreatorUserId)) as Gps on AbpEmployees.Id = Gps.CreatorUserId left join AbpEquipments on AbpEquipments.PDA = Gps.PDA where X is not null" + sqlwhere;
            var collection = Abp.DataProcessHelper.GetEntityFromTable<EmployeeDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
            foreach (var item in collection)
            {
                var berthsecIdList = item.BerthsecId.Split(',');
                foreach (var berthsecId in berthsecIdList)
                {
                    if(berthsecId != "")
                    {
                        int id = Convert.ToInt32(berthsecId);
                        item.BerthsecName = _berthsecAppService.Get(id).BerthsecName + ",";
                    }
                }
                item.BerthsecName = item.BerthsecName.Substring(0, item.BerthsecName.Length - 1);
            }
            return collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeGps(long InspectorId)
        {
            string sql = "select AbpEmployees.*, X, Y, Convert(varchar(19), Gps.CreationTime, 121) as UpLastTime, BerthsecName, AbpEquipments.Version from AbpEmployees left join (select * from AbpEquipmentGps with(nolock) where id in (select max(id) as Id from AbpEquipmentGps group by CreatorUserId)) as Gps on AbpEmployees.Id = Gps.CreatorUserId left join AbpBerthsecs on AbpEmployees.BerthsecId = AbpBerthsecs.id left join AbpEquipments on AbpEquipments.PDA = Gps.PDA where X is not null and AbpEmployees.Id in (select CheckInEmployeeId from AbpBerthsecs where Id in (select BerthsecId from AbpWorkGroupInspectorsBerthsecs where WorkGroupId in (select WorkGroupId from AbpWorkGroupInspectors where InspectorsId = " + InspectorId +")))";

            return Abp.DataProcessHelper.GetEntityFromTable<EmployeeDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public EmployeeDto GetEmployeeByUserName(string UserName)
        {
            return _employeeRepository.FirstOrDefault(entity => entity.UserName == UserName).MapTo<EmployeeDto>();
        }
    }
}
