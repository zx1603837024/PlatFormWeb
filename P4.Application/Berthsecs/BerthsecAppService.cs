using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using P4.Berthsecs.Dto;
using P4.Parks;
using Abp.Domain.Uow;
using P4.Berths;
using System.Data;
using P4.Permissions.Dto;
using Abp.Helper;
using System.Data.SqlClient;
using Abp.SignalR.SignalrService;
using P4.SubscribeManager;
using Abp.Configuration;

namespace P4.Berthsecs
{
    /// <summary>
    /// 
    /// </summary>
    public class BerthsecAppService : ApplicationService, IBerthsecAppService
    {
        #region Var
        private readonly IRepository<Berthsec> _berthsecAppService;
        private readonly IRepository<Berths.Berth, long> _berthAppService;
        private readonly IBerthsecRepository _berthsecRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Parks.Park> _parkRepository;
        private readonly IBerthAppService _abpberthAppService;
        private readonly IP4ChatService _p4ChatService;
        private readonly IRepository<Employees.Employee, long> _employeeAppService;
        private readonly ISubscribeAppService _subscribeAppService;
        private readonly ISettingStore _settingStore;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecAppService"></param>
        /// <param name="berthsecRepository"></param>
        /// <param name="berthAppService"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="parkRepository"></param>
        /// <param name="abpberthAppService"></param>
        /// <param name="p4ChatService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="subscribeAppService"></param>
        /// <param name="settingStore"></param>
        public BerthsecAppService(IRepository<Berthsec> berthsecAppService, IBerthsecRepository berthsecRepository, IRepository<Berths.Berth, long> berthAppService, IUnitOfWorkManager unitOfWorkManager, IRepository<Parks.Park> parkRepository, IBerthAppService abpberthAppService, IP4ChatService p4ChatService, IRepository<Employees.Employee, long> employeeAppService, ISubscribeAppService subscribeAppService, ISettingStore settingStore)
        {
            _berthsecAppService = berthsecAppService;
            _berthsecRepository = berthsecRepository;
            _berthAppService = berthAppService;
            _parkRepository = parkRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _abpberthAppService = abpberthAppService;
            _p4ChatService = p4ChatService;
            _employeeAppService = employeeAppService;
            _subscribeAppService = subscribeAppService;
            _settingStore = settingStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dto.GetAllBerthsecsListOutput GetBerthsecList(Dto.SearchBerthsecInput input)
        {
            return _berthsecRepository.GetAllBerthsecsList(input);
        }

        /// <summary>
        /// 道闸
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBerthsecsListOutput GetSignoParkList(SearchBerthsecInput input)
        {
            return _berthsecRepository.GetAllSignoParkList(input);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GetAllBerthsecsListOutput GetBerthsecList()
        {
            return new GetAllBerthsecsListOutput() { rows = _berthsecAppService.GetAll().MapTo<List<BerthsecDto>>() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string InsertBerthsec(Dto.CreateOrUpdateBerthsecInput input)
        {
            if (_berthsecAppService.FirstOrDefault(dic => dic.BerthsecName == input.BerthsecName) != default(Berthsec))
            {
                return input.BerthsecName + "已经存在!";
            }

            Berthsec entity = new Berthsec();
            entity.RateId = int.Parse(input.RateName);
            entity.RateId1 = int.Parse(input.RateName1);
            entity.RateId2 = int.Parse(input.RateName2);
            entity.ParkId = int.Parse(input.ParkName);
            entity.RegionId = int.Parse(input.RegionName);
            entity.BerthsecName = input.BerthsecName;
            var parkModel = _parkRepository.Load(entity.ParkId);
            entity.CompanyId = parkModel.CompanyId;
            entity.Lat = parkModel.X;
            entity.Lng = parkModel.Y;
            entity.IsActive = input.IsActive;
            entity.BerthCount = "0/0";

            entity.SignoCommunationTime = DateTime.Now;

            var id = _berthsecAppService.InsertAndGetId(entity);

            string sql = " update AbpDataPermissions set BerthsecIds = BerthsecIds + '," + id + "' where IsDeleted = 0 and Discriminator = 'UserDataPermissionSetting' and UserId = " + AbpSession.UserId.Value;
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, sql);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  新增了 " + entity.BerthsecName + "泊位段", "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        private void TotalBerthsecCount(int BerthsecId)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec))
            {
                var berthsecisactivecount = _berthAppService.Count(entity => entity.IsActive == true && entity.BerthsecId == BerthsecId);
                var berthseccount = _berthAppService.Count(entity => entity.BerthsecId == BerthsecId);
                var entry = _berthsecRepository.Load(BerthsecId);
                entry.BerthCount = berthsecisactivecount.ToString() + "/" + berthseccount.ToString();
                _berthsecRepository.Update(entry);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParkId"></param>
        private void TotalParkCount(int ParkId)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec))
            {
                var parkidcount = _berthAppService.Count(entity => entity.IsActive == true && entity.ParkId == ParkId);
                var entrypark = _parkRepository.Load(ParkId);
                entrypark.BerthCount = parkidcount;
                _parkRepository.Update(entrypark);
            }
        }

        /// <summary>
        /// 生成新增泊位
        /// 检测泊位号是否已经存在数据库中，如果存在返回true
        /// </summary>
        /// <param name="input"></param>
        /// <param name="newberthlist"></param>
        /// <param name="oldBerthlist"></param>
        /// <returns>1:存在重复编号，2:自定义编号存在非法字符，请转换为数据，3：自定义泊位号不能为负数，99：成功</returns>
        private int CheckBerth(CreateOrUpdateBerthsecInput input, out List<string> newberthlist, out List<Berths.Berth> oldBerthlist)
        {
            oldBerthlist = GetOldBerth(input);
            newberthlist = new List<string>();
            for (int i = input.BeginNumeber; i < input.EndNumeber + 1; i++)
            {
                var BerthNumber = i.ToString();
                if (oldBerthlist.Exists(entity => entity.BerthNumber == BerthNumber && entity.BerthsecId != input.Id))
                    return 1;
                newberthlist.Add(i.ToString());

            }
            if (input.CustomNumeber != null)
            {
                foreach (var s in input.CustomNumeber.Split(','))
                {
                    int temp = 0;
                    if (!int.TryParse(s, out temp))
                        return 2;
                    if (temp < 0)
                        return 3;
                    if (oldBerthlist.Exists(entity => entity.BerthsecId != input.Id && entity.BerthNumber == s))
                        return 1;
                    newberthlist.Add(s);
                }
            }
            return 99;
        }

        /// <summary>
        /// 插入泊位号
        /// </summary>
        /// <param name="newberthlist"></param>
        /// <param name="oldberthlist"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool InsertBerth(List<string> newberthlist, List<Berths.Berth> oldberthlist, CreateOrUpdateBerthsecInput input)
        {
            Berths.Berth berth = new Berths.Berth();

            List<Berth> chen = oldberthlist.Where(E => E.BerthsecId == input.Id).ToList();
            foreach (var str in oldberthlist.Where(E => E.BerthsecId == input.Id).ToList())
            {
                if (newberthlist.Exists(s => s == str.BerthNumber))
                    continue;
                _berthAppService.Delete(str.Id);
                //str.IsActive=false;              
                //_berthAppService.Update(str);
            }

            foreach (string str in newberthlist)
            {
                if (oldberthlist.Exists(entity => entity.BerthNumber == str && entity.BerthsecId == input.Id))
                    continue;
                berth = new Berths.Berth();
                berth.ParkId = int.Parse(input.ParkName);
                berth.RegionId = int.Parse(input.RegionName);
                berth.BerthNumber = str;
                berth.BerthsecId = input.Id;
                berth.BerthStatus = "2";
                berth.IsActive = true;
                berth.ParkStatus = 0;
                _berthAppService.Insert(berth);
            }
            return true;
        }

        /// <summary>
        /// 获取已创建泊位号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<Berths.Berth> GetOldBerth(CreateOrUpdateBerthsecInput input)
        {
            int ParkId = int.Parse(input.ParkName);
            List<Berths.Berth> list = new List<Berths.Berth>();
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec))
            {
                //if (input.oper == "edit")
                //    list = _berthAppService.GetAll().Where(entity => entity.ParkId == ParkId && entity.BerthsecId != input.Id).ToList();
                //else
                // list = _berthAppService.GetAll().Where(entity => entity.ParkId == ParkId).ToList();
                list = _berthAppService.GetAll().Where(entity => entity.ParkId == ParkId).ToList();
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBerthsec(int id)
        {
            Berthsec input = _berthsecAppService.Load(id);
            _berthsecAppService.Delete(id);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  删除了" + input.BerthsecName + "泊位段", "");
        }


        /// <summary>
        /// 修改泊位段管理
        /// </summary>
        /// <param name="input"></param>
        public string ModifyBerthsec(Dto.CreateOrUpdateBerthsecInput input)
        {
            var entity = _berthsecAppService.Load(input.Id);
            if (_berthsecAppService.FirstOrDefault(dic => dic.BerthsecName == input.BerthsecName && dic.Id != input.Id) != default(Berthsec))
            {
                return input.BerthsecName + "已经存在!";
            }

            if (input.ParkName == null)
            {
                return null;
            }
            if (input.RateName == null)
            {
                return null;
            }
            if (input.RegionName == null)
            {
                return null;
            }


            if (entity.UseStatus == true && input.UseStatus == false)
            {
                entity.UseStatus = input.UseStatus;
                entity.CheckStatus = false;
                entity.CheckOutTime = DateTime.Now;
            }
            // entity.BerthCount = newberthlist.Count + "/" + newberthlist.Count;
            entity.ParkId = int.Parse(input.ParkName);
            entity.RateId = int.Parse(input.RateName);
            entity.RateId1 = int.Parse(input.RateName1);
            entity.RateId2 = int.Parse(input.RateName2);
            entity.RegionId = int.Parse(input.RegionName);
            entity.BerthsecName = input.BerthsecName;
            entity.BeginNumeber = input.BeginNumeber;
            entity.EndNumeber = input.EndNumeber;
            entity.CustomNumeber = input.CustomNumeber;
            entity.IsActive = input.IsActive;
            entity.PushStatus = input.PushStatus;
            //InsertBerth(newberthlist, oldBerthlist, input);
            _berthsecAppService.Update(entity);
            //TotalParkCount(entity.ParkId);
            //TotalBerthsecCount(entity.Id);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  修改了 " + entity.BerthsecName + "泊位段", "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public BerthsecDto Load(int Id)
        {
            return _berthsecAppService.FirstOrDefault(Id).MapTo<BerthsecDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LParkIds"></param>
        /// <returns></returns>
        public List<BerthsecDto> LoadListBerhtsec(List<int> LParkIds)
        {
            return _berthsecRepository.LoadListBerhtsec(LParkIds);
        }


        /// <summary>
        /// 获取指定用户泊位段
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public GetAllBerthsecCheckListOutput GetAllBerthsecCheckList(long EmployeeId)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark);
            var employee = _employeeAppService.Load(EmployeeId);
            int Status = 0;
            if (bool.Parse(_settingStore.GetSettingOrNull(employee.TenantId, null, "EmployeesLimit").Value))
            {
                Status = 1;
            }
            return _berthsecRepository.GetAllBerthsecCheckLiist(new BerthsecCheckInput() { EmployeeId = EmployeeId }, Status);
        }


        /// <summary>
        /// 获取泊位段信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BerthsecDto> LoadAsync(int Id)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            var entity = await _berthsecAppService.FirstOrDefaultAsync(Id);
            return entity.MapTo<BerthsecDto>();
        }

        /// <summary>
        /// 获取泊位段列表，禁用过滤数据权限
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<BerthsecDto> GetBerthsecsListByList(List<int> list)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            return _berthsecAppService.GetAllList(entry => list.Contains(entry.Id)).MapTo<List<BerthsecDto>>();
        }

        /// <summary>
        /// 收费员签到，修改泊位段相关数据
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employessID"></param>
        public async Task UpdateBerthsecIn(int berthsecID, string DeviceCode, int employessID, int tenantId)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark);
            var entity = await _berthsecAppService.FirstOrDefaultAsync(berthsecID);
            if (entity != null)
            {
                //签到
                entity.CheckInEmployeeId = employessID;
                entity.CheckInDeviceCode = DeviceCode;
                entity.CheckInTime = DateTime.Now;
                entity.CheckStatus = true;
                entity.UseStatus = true;  //签到为true
                _berthsecAppService.UpdateAsync(entity);
            }
        }
        /// <summary>
        /// 收费员签退，修改泊位段相关数据
        /// </summary>
        public void UpdateBerthsecOut(int berthsecID, string DeviceCode, int employessID)
        {

            // 将所有泊位车检器guid置为空 防止下次登录获取到相同的guid
            List<Berth> berthlist = _berthAppService.GetAll().Where(ber => ber.BerthsecId == berthsecID).ToList();
            //List<Berth> berthlist = _abpberthAppService.GetBerthListByBerthsecid(AbpSession.BerthsecIds[0]);
            if (berthlist != null && berthlist.Count > 0)
            {
                foreach (Berth berth in berthlist)
                {
                    if (berth.SensorGuid != null)
                    {
                        berth.SensorGuid = null;
                        _berthAppService.Update(berth);
                    }

                }
            }
            _berthsecRepository.EmployeeCheckOutBerthsec(berthsecID, DeviceCode, employessID);  //执行sql签退泊位段
            //Berthsec entity = new Berthsec();
            //entity = _berthsecAppService.FirstOrDefault(berthsecID);
            //if (entity != null)
            //{
            //    //签退
            //    entity.CheckOutEmployeeId = employessID;
            //    entity.CheckOutDeviceCode = DeviceCode;
            //    entity.CheckOutTime = DateTime.Now  ;
            //    entity.CheckStatus = false;
            //    entity.UseStatus = false;
            //    _berthsecAppService.Update(entity);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BerthsecDto> GetAllBerthsec()
        {
            return _berthsecRepository.GetAll().MapTo<List<BerthsecDto>>();
        }

        /// <summary>
        /// 后台泊位段 批量签退
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        public int BerthsecBatchCheckOutBGO(List<int> BerthsecId)
        {
            int UserId = Convert.ToInt32(AbpSession.UserId);
            return _berthsecRepository.BerthsecBatchCheckOutBGO(BerthsecId, UserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParkId"></param>
        /// <returns></returns>
        public List<BerthsecDto> GetBerthsecByParkID(int ParkId)
        {
            List<BerthsecDto> listbd = _berthsecRepository.GetAllList().Where(p => p.ParkId == ParkId && AbpSession.BerthsecIds.Contains(p.Id)).MapTo<List<BerthsecDto>>();
            return listbd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        public string GetBerthList(int BerthsecId)
        {
            var berthsec = _berthsecAppService.FirstOrDefault(BerthsecId);


            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, "select * from abpberths with(nolock) where parkid = " + berthsec.ParkId + " and berthsecid in (-1, " + BerthsecId + ") order by id").Tables[0];


            string model = "{6} id: '{0}', pId: '{1}', name: '{2}', checked: {3}, doCheck: {4},open: {5} {7}";
            StringBuilder strs = new StringBuilder();
            foreach (DataRow v in dt.Rows)
            {
                strs.AppendFormat(model, v["Id"].ToString(), 0, v["BerthNumber"].ToString(), v["BerthsecId"].ToString() != "-1" ? "true" : "false", "true", "false", "{", "},");
            }
            return strs.ToString(0, strs.Length - 1);
        }

        /// <summary>
        /// 分配泊位段泊位
        /// </summary>
        /// <param name="savetype"></param>
        /// <param name="chooseberthsecid"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public bool SaveBerthsToBerthsec(string savetype, int chooseberthsecid, string nodes)
        {
            var berthsec = _berthsecAppService.Load(chooseberthsecid);

            nodes = nodes.Replace("checked", "check");
            List<ZTree> zlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ZTree>>(nodes);
            StringBuilder sql = new StringBuilder("update abpberths set LastModificationTime = getdate(), LastModifierUserId = " + AbpSession.UserId + " , berthsecid = -1 where berthsecid = " + chooseberthsecid);

            foreach (var v in zlist)
            {
                sql.Append(" update abpberths set LastModificationTime = getdate(), LastModifierUserId = " + AbpSession.UserId + " , berthsecid = " + chooseberthsecid + " where id = " + v.id);
                sql.Append(" update AbpBusinessDetail set berthsecid = " + chooseberthsecid + " where guid in (select guid from AbpBerths where id = " + v.id + " and BerthStatus = 1)");
                sql.Append(" update AbpSensors set berthsecid = " + chooseberthsecid + " where SensorNumber in (select SensorNumber from AbpBerths where id = " + v.id + ")");
                sql.Append(" update AbpSensorBusinessDetail set berthsecid = " + chooseberthsecid + " where guid in (select guid from Abpsensors where SensorNumber in (select SensorNumber from AbpBerths where id = " + v.id + " and ParkStatus = 1))");
            }

            berthsec.BerthCount = zlist.Count.ToString() + "/" + zlist.Count.ToString();
            if (!berthsec.UseStatus)
            {
                _berthsecAppService.Update(berthsec);
                return (Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, sql.ToString()) > 0 ? true : false);
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public List<BerthsecDto> LoadToSql(List<int> Ids)
        {
            return _berthsecRepository.LoadToSql(Ids);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BerthsecDto> GetUseDataPermissions()
        {
            return _berthsecRepository.GetAllList(entity => AbpSession.BerthsecIds.Contains(entity.Id)).ToList().MapTo<List<BerthsecDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="TenantID"></param>
        /// <param name="checkInOrOutTime"></param>
        /// <param name="CompanyId"></param>
        /// <param name="ParkID"></param>
        /// <param name="VersionNum"></param>
        /// <param name="BatchNo"></param>
        public void EmployeeCheckInPro(List<int> berthsecId, string DeviceCode, int employeeID, int TenantID, DateTime checkInOrOutTime, int CompanyId, int ParkID, string VersionNum, string BatchNo)
        {
            //var temp = AbpSession;
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@berthsecId", string.Join(",", berthsecId)),
                new SqlParameter("@DeviceCode", DeviceCode),
                new SqlParameter("@employeeID", employeeID),
                new SqlParameter("@TenantID", TenantID),
                new SqlParameter("@checkInOrOutTime", checkInOrOutTime),
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@ParkID", ParkID),
                new SqlParameter("@VersionNum", VersionNum),
                new SqlParameter("@BatchNo",BatchNo)
            };
            var employee = _employeeAppService.Load(employeeID);
            var berthsec = _berthsecAppService.Load(berthsecId[0]);
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.StoredProcedure, "Pro_Employee_Checkin", param);
            string message = "收费员:" + employee.TrueName + "(" + employee.UserName + ")，在(" + checkInOrOutTime + ")签到(" + berthsec.BerthsecName + ")";
            _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + TenantID).EmployeeMessage(employee.Id, message, 0);

            _subscribeAppService.SendMessage("CarInOutManagement", message, "");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="checkInOrOutTime"></param>
        public void EmployeeCheckOutPro(int berthsecId, string DeviceCode, int employeeID, DateTime checkInOrOutTime)
        {
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@berthsecId", berthsecId),
                new SqlParameter("@DeviceCode", DeviceCode),
                new SqlParameter("@employeeID", employeeID),
                new SqlParameter("@checkInOrOutTime", checkInOrOutTime)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.StoredProcedure, "Pro_Employee_Checkout", param);
            var employee = _employeeAppService.Load(employeeID);
            var berthsec = _berthsecAppService.Load(berthsecId);
            string message = "收费员:" + employee.TrueName + "(" + employee.UserName + ")，在" + checkInOrOutTime + "签退(" + berthsec.BerthsecName + ")";
            _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.Id, message, 1);
            _subscribeAppService.SendMessage("CarInOutManagement", message, "");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="checkInOrOutTime"></param>
        public void EmployeeCheckOutOulinePro(string berthsecId, string DeviceCode, int employeeID, DateTime checkInOrOutTime)
        {
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@berthsecId", berthsecId),
                new SqlParameter("@DeviceCode", DeviceCode),
                new SqlParameter("@employeeID", employeeID),
                new SqlParameter("@checkInOrOutTime", checkInOrOutTime)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.StoredProcedure, "Pro_Employee_Checkout", param);
            string sql = "select * from AbpEmployees where Id = " + employeeID;
            var employee = Abp.DataProcessHelper.GetEntityFromTable<Employees.Employee>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, sql).Tables[0])[0];
            sql = "select * from AbpBerthsecs where Id in ( " + berthsecId + ")";
            var berthsec = Abp.DataProcessHelper.GetEntityFromTable<Berthsecs.Berthsec>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, sql).Tables[0])[0];
            //_berthsecAppService.Load(berthsecId);
            string message = "收费员:" + employee.TrueName + "(" + employee.UserName + ")，在" + checkInOrOutTime + "签退(" + berthsec.BerthsecName + ")";
            _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + berthsec.TenantId).EmployeeMessage(employee.Id, message, 1);
        }

        /// <summary>
        /// 更新泊位段坐标
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveBerthsecAddress(BerthsecMapDto model)
        {
            var entity = _berthsecAppService.Load(model.Id);
            entity.Lat = model.X;
            entity.Lng = model.Y;
            return _berthsecAppService.Update(entity).Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public BerthsecDto GetBerthsecById(int Id)
        {
            var berthsecDto = _berthsecAppService.Load(Id).MapTo<BerthsecDto>();
            if (string.IsNullOrWhiteSpace(berthsecDto.Lat) || string.IsNullOrWhiteSpace(berthsecDto.Lng))
            {
                var parkDto = _parkRepository.Load(berthsecDto.ParkId);
                berthsecDto.Lat = parkDto.X;
                berthsecDto.Lng = parkDto.Y;
            }
            return berthsecDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BerthsecDto> GetBerthsecMapList(int Id)
        {
            return _berthsecAppService.GetAll().WhereIf(Id > 0, entity => entity.Id == Id).MapTo<List<BerthsecDto>>();
        }
    }
}
