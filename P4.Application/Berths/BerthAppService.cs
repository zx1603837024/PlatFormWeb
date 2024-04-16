using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using P4.Berths.Dtos;
using P4.Sensors;
using P4.Businesses;
using Abp.Authorization;
using System.Data;
using Abp.Auditing;
using Abp.Configuration;
using P4.Employees.Dtos;
using P4.Employees;

namespace P4.Berths
{
    /// <summary>
    /// 泊位管理
    /// </summary>
    public class BerthAppService : ApplicationService, IBerthAppService
    {
        #region Var
        private readonly IRepository<Berth, long> _abpBerthRepository;
        private readonly IRepository<Berthsecs.Berthsec> _berthsecRepository;
        private readonly IRepository<Sensor> _SensorRepository;
        private readonly IRepository<BusinessDetail, long> _businessDetailRepository;
        private readonly IRepository<Parks.Park> _parkRepository;
        private readonly IBerthRepository _bethRepository;
        private readonly ISettingStore _settingStore;
        private readonly IEmployeeAppService _employeeApplicationService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeApplicationService"></param>
        /// <param name="abpBerthRepository"></param>
        /// <param name="bethRepository"></param>
        /// <param name="berthsecRepository"></param>
        /// <param name="parkRepository"></param>
        /// <param name="SensorRepository"></param>
        /// <param name="businessDetailRepository"></param>
        /// <param name="settingStore"></param>
        public BerthAppService(IEmployeeAppService employeeApplicationService, IRepository<Berth, long> abpBerthRepository, IBerthRepository bethRepository,
            IRepository<Berthsecs.Berthsec> berthsecRepository, IRepository<Parks.Park> parkRepository, IRepository<Sensor> SensorRepository, IRepository<BusinessDetail, long> businessDetailRepository, ISettingStore settingStore)
        {
            _employeeApplicationService = employeeApplicationService;
            _businessDetailRepository = businessDetailRepository;
            _abpBerthRepository = abpBerthRepository;
            _bethRepository = bethRepository;
            _berthsecRepository = berthsecRepository;
            _parkRepository = parkRepository;
            _SensorRepository = SensorRepository;
            _settingStore = settingStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBerthOutput GetAllBethlist(BerthInput input)
        {
            return _bethRepository.GetBerthAll(input, AbpSession.TenantId, AbpSession.CompanyId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string BerthUpdate(CreatOrUpdateBerthInput input)
        {
            var entity2 = _SensorRepository.FirstOrDefault(dic => dic.SensorNumber == input.SensorNumber);
            if (entity2 == default(Sensor))
            {
                return input.SensorNumber + "不存在该车检器编号！";
            }
            else if ((_abpBerthRepository.FirstOrDefault(dic => dic.SensorNumber == input.SensorNumber && dic.Id != input.Id) != default(Berth))
                && input.SensorNumber != null)
            {
                return input.SensorNumber + "该车检器已经绑定！";
            }

            var entity = _abpBerthRepository.Load(input.Id);
            if (input.SensorNumber != null)
            {
                entity2.ParkId = entity.ParkId;
                entity2.RegionId = entity.RegionId;
                entity2.TenantId = entity.TenantId;
                entity2.BerthsecId = entity.BerthsecId;
                entity2.CompanyId = entity.CompanyId;
                entity2.BerthNumber = entity.BerthNumber;
                _SensorRepository.Update(entity2);
            }

            entity.IsActive = input.IsActive;
            entity.SensorNumber = input.SensorNumber;
            _abpBerthRepository.Update(entity);
            return null;
        }




        /// <summary>
        /// 收费员获取泊位数据
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public List<BerthDto> GetAllBerths()
        {
            var temp = _bethRepository.GetAllBerths(AbpSession.BerthsecIds);
            //var entity = _abpBerthRepository.GetAllList(entry => entry.IsActive == true);
            //return entity.MapTo<List<BerthDto>>();
            return temp;
        }



        /// <summary>
        /// 地感数据同步接口
        /// </summary>
        /// <param name="SyncTime"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [DisableAuditing]
        public List<BerthSensorDto> GetBerthsSyn(string SyncTime)
        {
            var berthsecs = _berthsecRepository.GetAllList(entry => AbpSession.BerthsecIds.Contains(entry.Id) && entry.PushStatus);
            if (berthsecs == null)
                return null;
            List<int> list = new List<int>();
            foreach (var model in berthsecs)
                list.Add(model.Id);
            return _bethRepository.GetAllBerthSensor(SyncTime, list);
        }

        /// <summary>
        /// 地感数据同步接口New
        /// </summary>
        /// <param name="SyncTime"></param>
        /// <param name="Berthesclist"></param>
        /// <returns></returns>
       //[DisableAuditing]
        public List<BerthSensorDto> GetBerthsSynNew(string SyncTime, string Berthesclist)
        {
            List<int> list = new List<int>();
            var IDs = Berthesclist.Split(',');
            foreach (var str in IDs)
            {
                if (str != "")
                    list.Add(int.Parse(str));
            }

            var test = AbpSession.BerthsecIds;
            var berthsecs = _berthsecRepository.GetAllList(entry => list.Contains(entry.Id) && entry.PushStatus);
            if (berthsecs == null)
                return null;

            List<int> BerthsecIDList = new List<int>();
            foreach (var model in berthsecs)
                BerthsecIDList.Add(model.Id);

            return _bethRepository.GetAllBerthSensor(SyncTime, BerthsecIDList);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="berth"></param>
        /// <returns></returns>
        public int UpdateBerthSensorGuid(Berth berth)
        {
            return _abpBerthRepository.Update(berth) == null ? 0 : 1;
        }

        /// <summary>
        /// 获取泊位数据
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        public List<Berth> GetBerthListByBerthsecid(int berthsecid)
        {
            return _abpBerthRepository.GetAll().Where(entity => entity.BerthsecId == berthsecid).ToList();
        }

        /// <summary>
        /// 通过泊位段id获取数据
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        public List<BerthDto> GetBerthsByBerthsecid(int berthsecid)
        {
            return _abpBerthRepository.GetAll().Where(entity => entity.IsActive == true && entity.BerthsecId == berthsecid).ToList().MapTo<List<BerthDto>>();
        }

        /// <summary>
        /// 获取详细泊位段detail
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <param name="CheckInEmployeeId"></param>
        /// <param name="CheckOutEmployeeId"></param>
        /// <returns></returns>
        public EmployeeGps GetBerthsecGpsDetail(int BerthsecId, long CheckInEmployeeId, long CheckOutEmployeeId)
        {
            EmployeeGps model = new EmployeeGps();
            var expr = _bethRepository.GetAllList();         

            model.BerthCount = expr.Where(entity => entity.BerthsecId == BerthsecId).Count();
            model.BerthUseCount = expr.Where(entity => entity.BerthsecId == BerthsecId && entity.BerthStatus == "2").Count();
            var berthsecModel = _berthsecRepository.Load(BerthsecId);
            if (berthsecModel.CheckInTime > berthsecModel.CheckOutTime)
            {
                model.CheckOutName = null;
            }
            else
            {
                model.CheckInName = null;
            }
            if (CheckInEmployeeId != 0)
            {
                var data = _employeeApplicationService.GetEmployeeDto(CheckInEmployeeId);
                if(data != null)
                {
                    model.CheckInName = data.TrueName;
                }
            }
            if(CheckOutEmployeeId != 0)
            {
                var data = _employeeApplicationService.GetEmployeeDto(CheckOutEmployeeId);
                if(data != null)
                {
                    model.CheckOutName = data.TrueName;
                }
            }

            return model;
        }

        /// <summary>
        /// 查询系统再停车辆信息
        /// 需判断商户代码，与分公司代码
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        public List<BerthDto> GetBerthInfoByPlateNumber(string plateNumber)
        {

            string sql = "select * from AbpBerths with(nolock) where IsActive = 1 and TenantId = " + AbpSession.TenantId.Value + " and CompanyId = " + AbpSession.CompanyId.Value + " and BerthStatus = 1 and RelateNumber = '" + plateNumber + "' and BerthsecId <> -1";
            var berthdtoList = Abp.DataProcessHelper.GetEntityFromTable<BerthDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
            if (berthdtoList.Count > 0)
                return berthdtoList;
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthids"></param>
        /// <returns></returns>
        public int BerthToSensorNolock(List<int> berthids)
        {
            string ids = string.Join(",", berthids);
            string sql = "update A set A.TenantId = NULL, A.CompanyId = NULL, A.RegionId = NULL, A.ParkId = NULL , A.BerthsecId = NULL, A.BerthNumber = NULL from AbpSensors as A left join AbpBerths as B on A.SensorNumber = B.SensorNumber where B.Id in (" + ids + ")  update AbpBerths set SensorNumber = null where id in  (" + ids + ")";
            return Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 通过id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public BerthDto GetBerthGuidByBerthId(int Id)
        {
            return _abpBerthRepository.Load(Id).MapTo<BerthDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <returns></returns>
        public List<BerthDto> GetBerthList(int berthsecId)
        {
            return _abpBerthRepository.GetAll().OrderBy(entity => entity.BerthNumber).Where(entry => entry.BerthsecId == berthsecId).MapTo<List<BerthDto>>();
        }
    }
}
