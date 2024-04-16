using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Employees;

using P4.Inspectors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Parks;
using P4.Berthsecs;
using Abp.Domain.Uow;
using P4.WorkGroupInspectors;
using P4.Berthsecs.Dto;
using System.Data;
using P4.SubscribeManager;

namespace P4.Inspectors
{
    /// <summary>
    /// 巡查员
    /// </summary>
    public class InspectorAppService : ApplicationService, IInspectorAppService
    {
        #region Var
        private readonly IRepository<Inspector, long> _abpInspectorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IInspectorRepository _inspectorRepository;
        private readonly IRepository<InspectorTask, long> _abpInspectorTaskRepository;
        private readonly IRepository<Park> _abpParkRepository;
        private readonly IRepository<Berthsec> _abpBerthsecRepository;
        private readonly IRepository<InspectorCheck, long> _abpInspectorCheckRepository;

        private readonly IWorkGroupInspectorsAppService _abpWorkGroupInspectorsRepository;
        private readonly ISubscribeAppService _subscribeAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpInspectorRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="inspectorRepository"></param>
        /// <param name="abpInspectorTaskRepository"></param>
        /// <param name="abpParkRepository"></param>
        /// <param name="abpBerthsecRepository"></param>
        /// <param name="abpInspectorCheckRepository"></param>
        /// <param name="abpWorkGroupInspectorsRepository"></param>
        public InspectorAppService(IRepository<Inspector, long> abpInspectorRepository, IUnitOfWorkManager unitOfWorkManager, IInspectorRepository inspectorRepository, IRepository<InspectorTask, long> abpInspectorTaskRepository, IRepository<Park> abpParkRepository, IRepository<Berthsec> abpBerthsecRepository, IRepository<InspectorCheck, long> abpInspectorCheckRepository, IWorkGroupInspectorsAppService abpWorkGroupInspectorsRepository, ISubscribeAppService subscribeAppService)
        {
            _abpInspectorRepository = abpInspectorRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _inspectorRepository = inspectorRepository;
            _abpInspectorTaskRepository = abpInspectorTaskRepository;
            _abpParkRepository = abpParkRepository;
            _abpBerthsecRepository = abpBerthsecRepository;
            _abpInspectorCheckRepository = abpInspectorCheckRepository;

            _abpWorkGroupInspectorsRepository = abpWorkGroupInspectorsRepository;
            _subscribeAppService = subscribeAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllInspectorOutput GetAllInspectorList(InspectorInput input)
        {
            return _inspectorRepository.GetInspectorAll(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string InspectorInsert(CreatOrUpdateInspector input)
        {
            if (_abpInspectorRepository.FirstOrDefault(dic => dic.UserName == input.UserName) != default(Inspector))
            {
                return input.UserName + "已经存在";
            }
            if (_abpInspectorRepository.FirstOrDefault(dic => dic.TrueName == input.TrueName) != default(Inspector))
            {
                return input.TrueName + "已经存在";
            }

            if (string.IsNullOrWhiteSpace(input.CompanyName))
            {
                return "分公司名称不能为空";
            }



            Inspector entity = new Inspector();
            entity.UserName = input.UserName;
            entity.TrueName = input.TrueName;
            entity.Password = input.Password;
            entity.Address = input.Address;
            entity.BankCard = input.BankCard;
            entity.AccountStatus = input.AccountStatus;
            entity.Telephone = input.Telephone;
            entity.CheckOut = input.CheckOut;
            entity.CompanyId = int.Parse(input.CompanyName);
            _abpInspectorRepository.Insert(entity);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  新增了 " + input.UserName + "巡查员", "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string InspectorUpdate(CreatOrUpdateInspector input)
        {
            var entity = _abpInspectorRepository.Load(input.Id);
            if (_abpInspectorRepository.FirstOrDefault(dic => dic.UserName == input.UserName && dic.Id != input.Id) != default(Inspector))
            {
                return input.UserName + "已经存在";
            }
            if (_abpInspectorRepository.FirstOrDefault(dic => dic.TrueName == input.TrueName && dic.Id != input.Id) != default(Inspector))
            {
                return input.TrueName + "已经存在";
            }

            if (input.CheckOut == true)
            {
                entity.CheckOut = input.CheckOut;
                entity.CheckOuttype = "1";
            }
            else
            {
                entity.CheckOut = input.CheckOut;
                entity.CheckOuttype = "2";
            }
            entity.UserName = input.UserName;
            entity.TrueName = input.TrueName;
            entity.Password = input.Password;
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.Address = input.Address;
            entity.BankCard = input.BankCard;
            entity.AccountStatus = input.AccountStatus;
            entity.Telephone = input.Telephone;
            _abpInspectorRepository.Update(entity);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  修改了 " + input.UserName + "巡查员", "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void InspectorDelete(CreatOrUpdateInspector input)
        {
            _abpInspectorRepository.Delete(input.Id);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  删除了 " + input.UserName + "巡查员", "");
        }

        /// <summary>
        /// 获取指定条数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetInspectorByTopOutput GetInspectorByTopList(GetInspectorByTopInput input)
        {
            return new GetInspectorByTopOutput()
            {
                rows = _abpInspectorRepository.GetAll().OrderByDescending(entity => entity.CheckInTime).Take(input.Count).ToList().MapTo<List<InspectorDto>>()
            };
        }

        /// <summary>
        /// 获取巡查任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllInspectorTasksOutput GetAllInspectorTasksList(InspectorTasksInput input)
        {
            return _inspectorRepository.GetAllInspectorTasksList(input);
        }

        /// <summary>
        /// 获取巡查反馈任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllInspectorTaskFeedbacksOutput GetAllInspectorTaskFeedbacksList(InspectorTaskFeedbacksInput input)
        {
            return _inspectorRepository.GetAllInspectorTaskFeedbacksList(input);
        }

        /// <summary>
        /// 获取巡查员
        /// </summary>
        /// <returns></returns>
        public List<InspectorDto> GetInspectorAll()
        {
            return _inspectorRepository.GetAllList().MapTo<List<InspectorDto>>();
        }

        /// <summary>
        /// 新增巡查员任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Insert(CreatOrUpdateInspectorTasks input)
        {
            InspectorTask entity = new InspectorTask();
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.ParkId = input.ParkName;
            entity.InspectorId = int.Parse(input.InspectorName);
            entity.Status = input.Status;
            entity.Remark = input.Remark;
            entity.EffectiveTime = DateTime.Now;
            _abpInspectorTaskRepository.Insert(entity);
            return null;
        }

        /// <summary>
        /// 修改巡查员任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Modify(CreatOrUpdateInspectorTasks input)
        {
            var entity = _abpInspectorTaskRepository.Load(input.Id);
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.ParkId = input.ParkName;
            entity.InspectorId = int.Parse(input.InspectorName);
            entity.Status = input.Status;
            entity.Remark = input.Remark;
            entity.EffectiveTime = DateTime.Now;
            _abpInspectorTaskRepository.Update(entity);
            return null;
        }

        /// <summary>
        /// 删除巡查员任务
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _abpInspectorTaskRepository.Delete(Id);
        }

        /// <summary>
        /// 根据巡查员ID获取所有的任务停车场
        /// </summary>
        /// <param name="inspectorId"></param>
        public InspectorRPB GetParkIdInspectorTask(long inspectorId)
        {
            InspectorRPB irpb = new InspectorRPB();
            List<int> regionId = new List<int>();
            List<string> parkid = new List<string>();
            List<int> berthsecId = new List<int>();
            var InspectorList = _abpInspectorTaskRepository.GetAll().Where(entity => entity.InspectorId == inspectorId && (entity.Status == 1 || entity.Status == 3)).GroupBy(a => a.ParkId).ToList();
            foreach (var a in InspectorList)
            {
                parkid.Add(a.Key);//停车场
                int pId = Convert.ToInt32(a.Key);
                var abppark = _abpParkRepository.GetAll().Where(pa => pa.Id == pId).ToList();
                foreach (var b in abppark)
                {
                    regionId.Add(b.RegionId);  //区域
                }
                List<Berthsec> berthsec = new List<Berthsec>();
                var abpregion = berthsec;
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark))  //泊位段禁用过滤器
                {
                    abpregion = _abpBerthsecRepository.GetAll().Where(b => b.ParkId == pId).ToList();
                }
                if (abpregion != null)
                {
                    foreach (var c in abpregion)
                    {
                        berthsecId.Add(c.Id);  //泊位段
                    }
                }
            }
            irpb.regionId = regionId;//区域
            irpb.parkId = parkid;  //停车场
            irpb.berthsecId = berthsecId;//泊位段
            return irpb;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <param name="parkId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        public void InspectorCheckInPark(long InspectorId, List<string> parkId, string DeviceCode, int CompanyId, int TenantId)
        {
            _inspectorRepository.InspectorCheckInPark(InspectorId, parkId, DeviceCode, CompanyId, TenantId);
        }


        /// <summary>
        /// 巡查员签退停车场历史表
        /// </summary>
        public void InspectorCheckOutPark()
        {
            List<int> parkIds = new List<int>();
            List<int> ParkIDs = AbpSession.ParkIds;  //停车场
            parkIds = ParkIDs;
            long userId = Convert.ToInt64(AbpSession.UserId);//用户id
            if (parkIds != null)
            {
                foreach (var park in parkIds)
                {
                    var inspectorCheck = _abpInspectorCheckRepository.FirstOrDefault(a => a.ParkId == park && a.InspectorId == userId && a.CheckOutTime == null && a.CheckIn == true);
                    if (inspectorCheck != null)  //该停车场正常签到  可以签退
                    {
                        inspectorCheck.CheckIn = false;
                        inspectorCheck.CheckOut = true;
                        inspectorCheck.CheckOutTime = DateTime.Now;
                        _abpInspectorCheckRepository.Update(inspectorCheck);
                    }
                }
            }
        }
        /// <summary>
        /// 巡查员任务上传（包含图片 接口）
        /// </summary>
        /// <param name="inspectorTaskId"></param>
        /// <param name="berthNumber"></param>
        /// <param name="berthsecId"></param>
        /// <param name="employeeId"></param>
        /// <param name="TaskContent"></param>
        /// <param name="Remark"></param>
        public void InspectorTaskUp(long inspectorTaskId, string berthNumber, int berthsecId, int employeeId, string TaskContent, string Remark)
        {
            InspectorTaskFeedback inspectorTF = new InspectorTaskFeedback() 
            {
            InspectorTaskId=inspectorTaskId,  //任务ID
            BerthNumber=berthNumber,     //泊位编号
            BerthsecId=berthsecId,     //泊位段Id
            EmployeeId=employeeId,  //收费员ID
            TaskContent=TaskContent,   //任务反馈内容
            Remark=Remark    //备注
            };
        }
        /// <summary>
        /// 获取所有巡查员任务列表
        /// </summary>
        /// <returns></returns>
        public List<InspectorTask> GetAllInspectorTask()
        {
            var inspectorTaksList = _abpInspectorTaskRepository.GetAll().Where(entity=>entity.IsDeleted==false).ToList();
            return inspectorTaksList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inspectorId"></param>
        /// <returns></returns>
        public InspectorRPB GetBerthsecListByWorkgroup(long inspectorId)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark);  //禁用过滤器
            var workgroup = _abpWorkGroupInspectorsRepository.GetWorkGroupListByInspectorId(inspectorId);
            InspectorRPB irpb = new InspectorRPB();
            if (!workgroup.IsActive)
                return irpb;
            List<int> regionId = new List<int>();
            List<string> parkid = new List<string>();
            List<int> berthsecId = new List<int>();
            foreach (BerthsecDto entity in workgroup.Berthsecs.Where(entry => entry.isselected == "selected"))
            {
                regionId.Add(entity.RegionId);
                parkid.Add(entity.ParkId.ToString());
                berthsecId.Add(entity.Id);
            }
            irpb.regionId = regionId;
            irpb.parkId = parkid;
            irpb.berthsecId = berthsecId;
            irpb.berthsec = workgroup.Berthsecs.Where(entry => entry.isselected == "selected").ToList();
            return irpb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <param name="inspectorRPB"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        public void InspectorCheckInPark(long InspectorId, InspectorRPB inspectorRPB, string DeviceCode, int CompanyId, int TenantId)
        {
            _inspectorRepository.InspectorCheckInPark(InspectorId, inspectorRPB, DeviceCode, CompanyId, TenantId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<InspectorDto> GetAllInsperctorMap(int Id)
        {
          
            string sqlwhere = " and AbpInspectors.CompanyId in (" + string.Join(",", AbpSession.CompanyIds) + ")";
            if (Id != 0)
                sqlwhere += " and  AbpInspectors.id = " + Id;
            return Abp.DataProcessHelper.GetEntityFromTable<InspectorDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select AbpInspectors.*, X, Y, Convert(varchar(19), Gps.CreationTime, 121) as UpLastTime, BerthsecName, AbpEquipments.Version from AbpInspectors left join (select * from AbpEquipmentGps with(nolock) where id in (select max(id) as Id from AbpEquipmentGps group by CreatorUserId)) as Gps on AbpInspectors.Id = Gps.CreatorUserId left join AbpBerthsecs on AbpInspectors.BerthsecId = AbpBerthsecs.id left join AbpEquipments on AbpEquipments.PDA = Gps.PDA where X is not null" + sqlwhere).Tables[0]);
        
    }
    }
}
