using Abp.Authorization;
using Abp.Domain.Repositories;
using P4.Equipments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using P4.Companys;
using P4.InspectorCharges.Dtos;

namespace P4.Equipments
{

    /// <summary>
    /// 设备管理
    /// </summary>
    public class EquipmentAppService : P4AppServiceBase, IEquipmentAppService
    {
        #region Var
        private readonly IRepository<EquipmentBeat, long> _equipmentBeatAppService;
        private readonly IRepository<EquipmentGps, long> _EquipmentGpsAppService;
        private readonly IRepository<Equipment, long> _equipmentAppService;
        private readonly IRepository<OperatorsCompany> _companyAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Equipment, long> _abpEquipmentRepository;
        private readonly IEquipmentRepository _EquipmentRepository;
        private readonly IRepository<EquipmentMaintenance, long> _abpEquipmentMaintenanceRepository;
        private readonly IRepository<AndroidLog> _abpAndroidLogRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipmentBeatAppService"></param>
        /// <param name="EquipmentGpsAppService"></param>
        /// <param name="abpEquipmentRepository"></param>
        /// <param name="EquipmentRepository"></param>
        /// <param name="abpEquipmentMaintenanceRepository"></param>
        /// <param name="companyAppService"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpAndroidLogRepository"></param>
        public EquipmentAppService(IRepository<EquipmentBeat, long> equipmentBeatAppService, IRepository<EquipmentGps, long> EquipmentGpsAppService, IRepository<Equipment, long> abpEquipmentRepository, IEquipmentRepository EquipmentRepository, IRepository<EquipmentMaintenance, long> abpEquipmentMaintenanceRepository, IRepository<OperatorsCompany> companyAppService, IUnitOfWorkManager unitOfWorkManager, IRepository<AndroidLog> abpAndroidLogRepository)
        {
            _equipmentBeatAppService = equipmentBeatAppService;
            _EquipmentGpsAppService = EquipmentGpsAppService;
            _equipmentAppService = abpEquipmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _abpEquipmentRepository = abpEquipmentRepository;
            _EquipmentRepository = EquipmentRepository;
            _abpEquipmentMaintenanceRepository = abpEquipmentMaintenanceRepository;
            _companyAppService = companyAppService;
            _abpAndroidLogRepository = abpAndroidLogRepository;
        }

        /// <summary>
        /// 设备心跳包上传
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<long> InsertEquipBeat()
        {
            var id = await _equipmentBeatAppService.InsertAndGetIdAsync(new EquipmentBeat() { PDA = AbpSession.DeviceCode });
            return id;
        }

        /// <summary>
        /// Gps接口
        /// </summary>
        /// <returns></returns>
        public GetGpsOutput GetGps(string x, string y)
        {
           
              //添加Gps数据
            _EquipmentGpsAppService.Insert(new EquipmentGps() { PDA = AbpSession.DeviceCode, X = x, Y = y });
            return new GetGpsOutput()
            {
                Items = _EquipmentGpsAppService.GetAll().Where(g => g.X == x && g.Y == y).ToList().MapTo<List<GpsDto>>()
            };
        }

        
        /// <summary>
        /// 查询pda设备列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEquipmentOutput GetAllEquipmentOutputList(EquipmentInput input)
        {
            return _EquipmentRepository.GetAllEquipmentOutputList(input);
        }

        /// <summary>
        /// 新增pda设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Insert(CreateOrUpdateEquipmentInput input)
        {
            if (_equipmentAppService.FirstOrDefault(dic => dic.PDA == input.PDA && dic.Id != input.Id) != default(Equipment))
            {//设备编号不能重复
                return "设备编号" + input.PDA + "已经存在!";
            }
            if (input.UseStatus != 0 && input.EmployeeName == "0")//有领用或返修记录领用用户不能为空
            {
                return "请选择领用用户!";
            }
            Equipment entity = new Equipment();
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.PDA = input.PDA;
            entity.Type = input.Type;
            entity.Sim = input.Sim;
            entity.SD = input.SD;
            entity.Pasm = input.Pasm;
            entity.Printers = input.Printers;
            entity.IsActive = input.IsActive;
            entity.Standard = input.Standard;
            entity.Scan = input.Scan;
            entity.UseStatus = input.UseStatus;
            entity.EmployeeId = int.Parse(input.EmployeeName);
            entity.Remark = input.Remark;
            if (input.UseStatus != 0)//有领用或返修记录获取当前时间
            {
                entity.GetTime = DateTime.Now;
            }
            _EquipmentRepository.Insert(entity);
            if (input.UseStatus != 0)//有领用或返修记录往pda维护列表增加记录
            {
                var id = _abpEquipmentRepository.InsertAndGetId(entity);
                input.Id = int.Parse(id.ToString());
                InsertEquipmentMaintenance(input);
            }
            return null;
        }

        /// <summary>
        /// 删除一条pda设备
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _EquipmentRepository.Delete(Id);
        }

        /// <summary>
        /// 新增pda维护
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool InsertEquipmentMaintenance(CreateOrUpdateEquipmentInput input)
        {
            EquipmentMaintenance entity = new EquipmentMaintenance();
            entity.EquipmentId = input.Id;
            entity.PDA = input.PDA;
            entity.Ramark = input.Remark;
            entity.UseStatus = input.UseStatus;
            _abpEquipmentMaintenanceRepository.Insert(entity);
            return true;
        }

        /// <summary>
        /// 编辑pda设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Modify(CreateOrUpdateEquipmentInput input)
        {
            if (_equipmentAppService.FirstOrDefault(dic => dic.PDA == input.PDA && dic.Id != input.Id) != default(Equipment))
            {//设备编号不能重复
                return "设备编号" + input.PDA + "已经存在!";
            }
            if (input.UseStatus != 0 && input.EmployeeName == "0")//有领用或返修记录领用用户不能为空
            {
                return "请选择领用用户!";
            }
            int employeeid = int.Parse(input.EmployeeName);
            if (input.UseStatus != 0 && _equipmentAppService.FirstOrDefault(dic => dic.UseStatus == input.UseStatus && dic.Id == input.Id && dic.EmployeeId != employeeid) != default(Equipment))
            {
                return "设备已经被领用，不能重复领用!";
            }
            var entity = _EquipmentRepository.Load(input.Id);
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.TenantId = _companyAppService.Load(entity.CompanyId).TenantId;
            entity.PDA = input.PDA;
            entity.Type = input.Type;
            entity.Sim = input.Sim;
            entity.SD = input.SD;
            entity.Pasm = input.Pasm;
            entity.Printers = input.Printers;
            entity.IsActive = input.IsActive;
            entity.Standard = input.Standard;
            entity.IsUpgrade = input.IsUpgrade;
            entity.Scan = input.Scan;
            entity.UseStatus = input.UseStatus;
            entity.EmployeeId = int.Parse(input.EmployeeName);
            entity.Remark = input.Remark;
            if (input.UseStatus != 0 && _equipmentAppService.FirstOrDefault(dic => dic.UseStatus != input.UseStatus && dic.Id == input.Id) != default(Equipment))
            {//修改使用状态获取当前时间,修改其他信息时不改变时间
                entity.GetTime = DateTime.Now;
            }
            if (input.UseStatus == 0)
            {//改为未使用，重置领用时间
                entity.GetTime = null;
            }
            _EquipmentRepository.Update(entity);
            if (input.UseStatus != 0)//有领用或返修记录往pda维护列表增加记录
            {
                var id = _abpEquipmentRepository.InsertOrUpdateAndGetId(entity);
                input.Id = int.Parse(id.ToString());
                if (input.UseStatus != 0 && _equipmentAppService.FirstOrDefault(dic => dic.UseStatus == input.UseStatus && dic.Id == input.Id && dic.EmployeeId == employeeid) != default(Equipment))
                { }//领用状态、领用人员不变时修改其他信息不做操作
                else
                {
                    InsertEquipmentMaintenance(input);//往pda维护列表增加记录
                }
                
            }
            return null;
        }

        /// <summary>
        /// 获取pda维护列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEquipmentMaintenanceOutput GetAllEquipmentMaintenanceOutputList(EquipmentMaintenanceInput input)
        {
            return _EquipmentRepository.GetAllEquipmentMaintenanceOutputList(input);
        }

        /// <summary>
        /// 删除一条pda维护列表数据
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteEquipmentMaintenance(int Id)
        {
            _abpEquipmentMaintenanceRepository.Delete(Id);
        }

        /// <summary>
        /// 更新设备版本号
        /// </summary>
        /// <param name="PDA"></param>
        /// <param name="Version"></param>
        /// <param name="tenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public async Task UpdateVersion(string PDA, string Version, int tenantId, int CompanyId)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany);
            var equipment = await _equipmentAppService.FirstOrDefaultAsync(entity => entity.PDA == PDA);
            if (equipment != default(Equipment))
            {
                equipment.TenantId = tenantId;
               
                equipment.CompanyId = CompanyId;

                if (Version != equipment.Version)
                {
                    equipment.UpgradeTime = DateTime.Now;
                }
                equipment.Version = Version;
                _equipmentAppService.Update(equipment);
            }
        }
        /// <summary>
        /// 新增PDA设备记录
        /// </summary>
        /// <param name="DeviceCode"></param>
        /// <param name="VersionNum"></param>
        /// <param name="tenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public async Task InsertEquipment(string DeviceCode, string VersionNum, int tenantId, int CompanyId)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany);
            var equipment = await _equipmentAppService.FirstOrDefaultAsync(entity => entity.PDA == DeviceCode);
            if (equipment == default(Equipment))
            {
                equipment = new Equipment();
                equipment.PDA = DeviceCode;
                equipment.UseStatus = 1;
                equipment.EmployeeId = AbpSession.UserId;
                equipment.IsDeleted = false;
                equipment.LastModificationTime = DateTime.Now;
                equipment.LastModifierUserId = AbpSession.UserId;
                equipment.CreationTime = DateTime.Now;
                equipment.CreatorUserId = AbpSession.UserId;
                equipment.Type = 1; //收费机
                equipment.Printers = true; //是否有打印机
                equipment.Version = VersionNum; //版本号
                equipment.CompanyId = CompanyId;
                equipment.TenantId = tenantId;
                equipment.Remark = "";
                equipment.IsActive = true;
                equipment.TenantId = tenantId;
                await _equipmentAppService.InsertAsync(equipment);
            }
            //else
            //{
            //    equipment.CompanyId = CompanyId;
            //    equipment.TenantId = tenantId;
            //    equipment.Version = VersionNum;
            //    _equipmentAppService.UpdateAsync(equipment);
            //}
        }

        /// <summary>
        /// 获取设备使用状态饼状图数据
        /// </summary>
        /// <returns></returns>
        public List<EquipmentDto> GetEquipmentNumGroupByUseStatus()
        {
            int? tenantID = AbpSession.TenantId;
            return _EquipmentRepository.GetEquipmentNumGroupByUseStatus(tenantID);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CargoInfoExport()
        {
            int? tenantID = AbpSession.TenantId;
            _EquipmentRepository.CargoInfoExport(tenantID);
        }

        /// <summary>
        /// 人员轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GpsDto> GetEquipmentGpsList(InspectorChargeInput input)
        {
            long Id = long.Parse(input.employeeIdInput);
            DateTime operateDateBegin = DateTime.Parse(input.operateDateBegin);
            DateTime operateDateEnd = DateTime.Parse(input.operateDateEnd).AddDays(1);
            var index = input.rows * 10;
            var model = _EquipmentGpsAppService.GetAllList(entity => entity.CreatorUserId == Id && entity.CreationTime > operateDateBegin && entity.CreationTime < operateDateEnd).OrderBy(entry => entry.Id).Skip(index).Take(10).MapTo<List<GpsDto>>();
            foreach (var item in model)
            {
                item.Time = item.CreationTime.ToString("yyyy年MM月dd日hh时mm分ss秒");
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void InsertAndroidLog(AndroidLogDto input)
        {
            _abpAndroidLogRepository.Insert(input.MapTo<AndroidLog>());
        }
    }
}
