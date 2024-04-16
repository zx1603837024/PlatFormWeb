using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Collectors.Dtos;
using P4.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Berthsecs;
using P4.Sensors;
using P4.Berths;
using P4.Sensors.Dtos;
using P4.Reports;
using P4.Parks.Dtos;
using P4.Parks;
using P4.Dictionarys;
using P4.Weixin;
using P4.Businesses;
using P4.MonthlyCars;
using Abp.Domain.Uow;

namespace P4.Collectors
{
    /// <summary>
    /// 
    /// </summary>
    public class CollectorAppService : ApplicationService, ICollectorAppService
    {

        #region Var
        private readonly IRepository<Employee, long> _abpEmployeeRepository;
        private readonly IRepository<Berth, long> _abpBerthRepository;
        private readonly IRepository<Berthsec> _abpBerthsecRepository;
        private readonly IRepository<Sensor> _abpSensorRepository;
        private readonly IRepository<BerthsecReport> _abpBerthsecReportRepository;
        private readonly IRepository<EmployeeReport> _abpEmployeeReportRepository;
        private readonly IRepository<Park> _abpParkRepository;
        private readonly IRepository<DictionaryValue> _abpDictionaryValueRepository;
        private readonly ICollectorRepository _collectorRepository;
        private readonly IRepository<TUser> _abpWeixinTUserRepository;
        private readonly IRepository<BusinessDetail, long> _abpBusinessDetailRepository;
        private readonly IRepository<MonthlyCarHistory,long> _monthlyCarHistoryRepository;
        private readonly IRepository<MonthlyCar> _monthlyCarRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpCollectorRepository"></param>
        /// <param name="abpBerthRepository"></param>
        /// <param name="abpSensorRepository"></param>
        /// <param name="abpBerthsecReportRepository"></param>
        /// <param name="abpEmployeeReportRepository"></param>
        /// <param name="abpParkRepository"></param>
        /// <param name="abpDictionaryValueRepository"></param>
        /// <param name="collectorRepository"></param>
        /// <param name="abpWeixinTUserRepository"></param>
        /// <param name="abpBusinessDetailRepository"></param>
        /// <param name="abpBerthsecRepository"></param>
        /// <param name="monthlyCarHistoryRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public CollectorAppService(IRepository<Employee, long> abpCollectorRepository,
            IRepository<Berth, long> abpBerthRepository, IRepository<Sensor> abpSensorRepository,
            IRepository<BerthsecReport> abpBerthsecReportRepository, IRepository<EmployeeReport> abpEmployeeReportRepository,
            IRepository<Park> abpParkRepository, IRepository<DictionaryValue> abpDictionaryValueRepository,
            ICollectorRepository collectorRepository, IRepository<TUser> abpWeixinTUserRepository,
            IRepository<BusinessDetail, long> abpBusinessDetailRepository, IRepository<Berthsec> abpBerthsecRepository, IRepository<MonthlyCarHistory, long> monthlyCarHistoryRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<MonthlyCar> monthlyCarRepository)
        {
            _monthlyCarRepository = monthlyCarRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _monthlyCarHistoryRepository = monthlyCarHistoryRepository;
            _abpEmployeeRepository = abpCollectorRepository;
            _abpBerthRepository = abpBerthRepository;
            _abpSensorRepository = abpSensorRepository;
            _abpBerthsecReportRepository = abpBerthsecReportRepository;
            _abpEmployeeReportRepository = abpEmployeeReportRepository;
            _abpParkRepository = abpParkRepository;
            _abpDictionaryValueRepository = abpDictionaryValueRepository;
            _collectorRepository = collectorRepository;
            _abpWeixinTUserRepository = abpWeixinTUserRepository;
            _abpBusinessDetailRepository = abpBusinessDetailRepository;
            _abpBerthsecRepository = abpBerthsecRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MonthCarChainDto GetMonthCarCountChainDto()
        {
            MonthCarChainDto model = new MonthCarChainDto();
            DateTime dateTime = DateTime.Now;//现在
            DateTime startMonth = dateTime.AddDays(1 - dateTime.Day); //本月月初

            DateTime lastdateTime = dateTime.AddMonths(-1);//上月           
            DateTime startlastMonth = lastdateTime.AddDays(1 - lastdateTime.Day); //本月月初
            var monthCarNow = _monthlyCarHistoryRepository.GetAllList(entity => entity.CreationTime <= dateTime && entity.CreationTime>= startMonth);
            var monthCarHistory = _monthlyCarHistoryRepository.GetAllList(entity => entity.CreationTime >= startlastMonth && entity.CreationTime <= lastdateTime);
            model.MonthCarNowCount = monthCarNow.Count;
            if (model.MonthCarNowCount > 0)
            {
                model.MonthCarNowMoney = monthCarNow.Sum(e => e.Money);
            }
            else
            {
                model.MonthCarNowMoney = 0;
            }
            model.MonthCarHistoryCount = monthCarHistory.Count;
            if (model.MonthCarHistoryCount > 0)
            {
                model.MonthCarHistoryMoney = monthCarHistory.Sum(e => e.Money);
            }
            else
            {
                model.MonthCarHistoryMoney = 0;
            }

            if(model.MonthCarHistoryCount != 0)
            {
                if (model.MonthCarNowCount - model.MonthCarHistoryCount >= 0)
                {
                    model.CountPercentUp = (model.MonthCarNowCount - model.MonthCarHistoryCount) * 100 / model.MonthCarHistoryCount;
                }
                else
                {
                    model.CountPercentDown = (model.MonthCarHistoryCount - model.MonthCarNowCount) * 100 / model.MonthCarHistoryCount;
                }
            }
            else
            {
                model.CountPercentUp = model.MonthCarNowCount*100;
            }
            if (model.MonthCarHistoryMoney != 0)
            {
                if (model.MonthCarNowMoney - model.MonthCarHistoryMoney >= 0)
                {
                    model.MoneyPercentUp = Convert.ToInt32((model.MonthCarNowMoney - model.MonthCarHistoryMoney) * 100 / model.MonthCarHistoryMoney);
                }
                else
                {
                    model.MoneyPercentDown = Convert.ToInt32((model.MonthCarHistoryMoney - model.MonthCarNowMoney) * 100 / model.MonthCarHistoryMoney);
                }
            }
            else
            {
                model.MoneyPercentUp = Convert.ToInt32(model.MonthCarNowMoney  * 100);
            }         
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete);
            //model.MonthCarTotalMoney = _monthlyCarHistoryRepository.GetAllList().Sum(e => e.Money);
            model.MonthCarTotalMoney = _monthlyCarRepository.GetAllList().Sum(e => e.Money);
            model.MonthCarHistoryMoney = Convert.ToInt32(model.MonthCarHistoryMoney);
            model.MonthCarNowMoney = Convert.ToInt32(model.MonthCarNowMoney);
            return model;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GetCollectorCheckTotalOutput GetCollectorCheckCount()
        {
           DateTime now =DateTime.Now.Date;
            return new GetCollectorCheckTotalOutput()
            {
                Count = _abpEmployeeRepository.Count(T => T.AccountStatus == "1"),
                CheckInCout = _abpEmployeeRepository.Count(T => T.CheckIn == true && T.CheckInTime >= now),
                CheckOutCount = _abpEmployeeRepository.Count(T => T.CheckOut == true && T.CheckOutTime >= now)
               //Items = _abpEmployeeRepository.GetAll().Take(5).ToList().MapTo<List<CollectorDto>>()
            };
        }
        /// <summary>
        /// 获取泊位段在停率
        /// </summary>
        /// <returns></returns>
        public GetBerthCheckTotalOutput GetBerthCheckTotalCount()
        {
            return new GetBerthCheckTotalOutput()
            {
                BerthCount = _abpBerthRepository.Count(),
                BerthOnUseCount = _abpBerthRepository.Count(T => T.BerthStatus != "2"),
                BerthOffUseCount = _abpBerthRepository.Count(T => T.BerthStatus == "2")
            };
        }
        /// <summary>
        /// 获取车检器在线率
        /// </summary>
        /// <returns></returns>
        public GetSensorCheckTotalOutput GetSensorCheckTotalCount()
        {
            DateTime dt = DateTime.Now.AddMinutes(-P4Consts.SensorDayOnline);

            var query = _abpSensorRepository.GetAll();

            if (AbpSession.TenantId.HasValue)
                query = query.Where(entity => entity.TenantId == AbpSession.TenantId.Value);

            return new GetSensorCheckTotalOutput()
            {
                SensorCount = query.Count(),
                SensorOnLineCount = query.Count(T => (T.BeatDatetime.Value > dt) || T.ParkStatus == 1)
            };
        }

        /// <summary>
        /// 道闸在线略
        /// </summary>
        /// <returns></returns>
        public GetsignoOnlineOutput GetsignoOnlineOutput()
        {
            DateTime dt = DateTime.Now.AddMinutes(-P4Consts.SignoOnline);
            var query = _abpBerthsecRepository.GetAllList();
            return new GetsignoOnlineOutput()
            {
                SignoCount = query.Count(),
                SignoOnLineCount = query.Count(T => (T.SignoCommunationTime > dt))            
            };
        }







        /// <summary>
        /// 获取泊位段收费数据统计
        /// </summary>
        /// <returns></returns>
        public GetBerthsecReportTotalOutput GetBerthsecBussinessCount()
        {
            DateTime endTime = DateTime.Now;
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            List<BerthsecReport> berthsecReportList = _abpBerthsecReportRepository.GetAllList(ber => ber.Time >= beginTime && ber.Time <= endTime);

            var tempmodel

            = new GetBerthsecReportTotalOutput()
            {
                PrepaidMoney = Convert.ToDecimal(berthsecReportList.Sum(bts => bts.Prepaid)),
                StopTimes = Convert.ToInt32(berthsecReportList.Sum(bts => bts.StopTimes)),
                StopTime = Convert.ToInt32(berthsecReportList.Sum(bts => bts.StopTime)),
                EscapeStopTimes = Convert.ToInt32(berthsecReportList.Sum(bts => bts.EscapeStopTimes)),
                Cash = Convert.ToDecimal(berthsecReportList.Sum(bts => bts.Cash)),

                SensorsStopTime = Convert.ToInt32(berthsecReportList.Sum(bts => bts.SensorsStopTime)),
                SensorsReceivable = Convert.ToDecimal(berthsecReportList.Sum(bts => bts.SensorsReceivable)),

                //Receivable = Convert.ToDecimal(berthsecReportList.Sum(bts => bts.Receivable)),
                //FactReceive = Convert.ToDecimal(berthsecReportList.Sum(bts => bts.FactReceive)),
                //Arrearage = Convert.ToDecimal(berthsecReportList.Sum(bts => bts.Arrearage)),
                //Repayment = Convert.ToDecimal(berthsecReportList.Sum(bts => bts.Repayment)),

                Receivable = Convert.ToDecimal(_abpBusinessDetailRepository.GetAllList(entity => entity.CarPayTime > beginTime && entity.Money.HasValue).Sum(entry => entry.Money.Value)),             //  应收
                Arrearage = Convert.ToDecimal(_abpBusinessDetailRepository.GetAllList(entity => entity.Status == 3 && entity.EscapeTime > beginTime).Sum(entry => entry.Arrearage)),                   //  欠费
                Repayment = Convert.ToDecimal(_abpBusinessDetailRepository.GetAllList(entity => entity.Status == 4 && entity.CarRepaymentTime > beginTime && entity.Repayment.HasValue).Sum(entry => entry.Repayment.Value))//  欠费补缴
            };

            tempmodel.FactReceive = tempmodel.Receivable - tempmodel.Arrearage;            //  实收
            return tempmodel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GetBerthsecReportTotalOutput GetIndexMoney()
        {
            DateTime endTime = DateTime.Now;
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

            return new GetBerthsecReportTotalOutput()
            {
                Receivable = Convert.ToDecimal(_abpBusinessDetailRepository.GetAllList(entity => entity.CarOutTime > beginTime).Sum(entry => entry.Money)),             //  应收
                FactReceive = Convert.ToDecimal(_abpBusinessDetailRepository.GetAllList(entity => entity.Status == 2 && entity.CarPayTime > beginTime).Sum(entry => entry.FactReceive)),            //  实收
                Arrearage = Convert.ToDecimal(_abpBusinessDetailRepository.GetAllList(entity => entity.Status == 3 && entity.EscapeTime > beginTime).Sum(entry => entry.Arrearage)),               //  欠费
                Repayment = Convert.ToDecimal(_abpBusinessDetailRepository.GetAllList(entity => entity.Status == 4 && entity.CarRepaymentTime > beginTime).Sum(entry => entry.Repayment))//  欠费补缴
            };
        }

        /// <summary>
        /// 获取收费员收费数据统计
        /// </summary>
        /// <returns></returns>

        public List<GetEmployeeReportTotalOutput> GetEmployeeBussinessCount()
        {
            DateTime endTime = DateTime.Now;
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            List<GetEmployeeReportTotalOutput> employeeList = new List<GetEmployeeReportTotalOutput>();
            List<EmployeeReport> employeeReportList =
                _abpEmployeeReportRepository.GetAll()
                    .Where(ber => ber.Time >= beginTime && ber.Time <= endTime)
                    .ToList();
            var employees = employeeReportList.GroupBy(bts => bts.EmployeeId);
            foreach (var employee in employees)
            {
                employeeList.Add(new GetEmployeeReportTotalOutput
                {
                    EmployeeId = employee.Key,
                    EmployeeName =
                        (_abpEmployeeRepository.FirstOrDefault(em => em.Id == employee.Key) ??
                         new Employee() {TrueName = "未知"}).TrueName,
                    Prepaid = Convert.ToDecimal(employee.Sum(bts => bts.Prepaid)),
                    StopTime = Convert.ToInt32(employee.Sum(bts => bts.StopTime)),
                    Receivable = Convert.ToDecimal(employee.Sum(bts => bts.Receivable)),
                    FactReceive = Convert.ToDecimal(employee.Sum(bts => bts.FactReceive)),
                    Arrearage = Convert.ToDecimal(employee.Sum(bts => bts.Arrearage)),
                    Repayment = Convert.ToDecimal(employee.Sum(bts => bts.Repayment)),
                    SensorsStopTime = Convert.ToInt32(employee.Sum(bts => bts.SensorsStopTime)),
                    SensorsReceivable = Convert.ToDecimal(employee.Sum(bts => bts.SensorsReceivable))
                });
            }
            return employeeList.OrderByDescending(p => p.FactReceive).Take(10).ToList();
        }

        /// <summary>
        /// 获取停车场收费数据统计
        /// </summary>
        /// <returns></returns>

        public List<GetParkTotalOutput> GetParkBussinessCount()
        {


            DateTime endTime = DateTime.Now;
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            List<GetParkTotalOutput> parkList = new List<GetParkTotalOutput>();
            List<BerthsecReport> berthsecReportList = _abpBerthsecReportRepository.GetAllList(ber => ber.Time >= beginTime && ber.Time <= endTime);
            var parks = berthsecReportList.GroupBy(bts => bts.ParkId);
            foreach (var park in parks)
            {
                parkList.Add(new GetParkTotalOutput()
                {
                    //BerthsecReportList=park,
                    ParkId = park.Key,
                    ParkName =
                        (_abpParkRepository.FirstOrDefault(pk => pk.Id == park.Key) ?? new Park() {ParkName = "未知"})
                            .ParkName,
                    //ParkName=park.
                    Prepaid = Convert.ToDecimal(park.Sum(p => p.Prepaid)),
                    StopTime = Convert.ToInt32(park.Sum(p => p.StopTime)),
                    Receivable = Convert.ToDecimal(park.Sum(p => p.Receivable)),
                    FactReceive = Convert.ToDecimal(park.Sum(p => p.FactReceive)),
                    Arrearage = Convert.ToDecimal(park.Sum(p => p.Arrearage)),
                    Repayment = Convert.ToDecimal(park.Sum(p => p.Repayment)),
                    SensorsStopTime = Convert.ToInt32(park.Sum(p => p.SensorsStopTime)),
                    SensorsReceivable = Convert.ToDecimal(park.Sum(p => p.SensorsReceivable))
                });
            }
            return parkList.OrderByDescending(p => p.FactReceive).Take(6).ToList();

        }
        /// <summary>
        /// 获取当月收费统计
        /// </summary>
        /// <returns></returns>
        public List<GetMonthTotalOutput> GetMonthBussinessCount()
        {
            DateTime now = DateTime.Now;
            int nowmonth = now.Month;
            DateTime day1 = new DateTime(now.Year, now.Month, 1);
            DateTime beginTime = day1;
            DateTime endTime = now;

            List<GetMonthTotalOutput> monthlist = new List<GetMonthTotalOutput>();
            List<BerthsecReport> berthsecReportList = _abpBerthsecReportRepository.GetAllList(ber => ber.Time >= beginTime && ber.Time <= endTime);
            var MonthDatas = berthsecReportList.GroupBy(bts => bts.Time.Day);

            foreach (var monthdata in MonthDatas)
            {
                monthlist.Add(new GetMonthTotalOutput()
                {
                    monthday = monthdata.Key,
                    Receivable = Convert.ToDecimal(monthdata.Sum(p => p.Receivable)),
                    FactReceive = Convert.ToDecimal(monthdata.Sum(p => p.FactReceive)),
                    Arrearage = Convert.ToDecimal(monthdata.Sum(p => p.Arrearage))

                });
            }
            return monthlist;

        }

        /// <summary>
        /// 获取今年收费统计
        /// </summary>
        /// <returns></returns>
        public GetYearTotalOutput GetYearBussinessCount()
        {
            return _collectorRepository.GetYearBussinessCount();
            //DateTime now = DateTime.Now;
            //int nowyear = now.Year;
            //DateTime day1 = new DateTime(now.Year, 1, 1);
            //DateTime beginTime = day1;
            //DateTime endTime = now;
            //List<BerthsecReport> berthsecReportList = _abpBerthsecReportRepository.GetAllList(ber => ber.Time >= beginTime && ber.Time <= endTime);
            //return new GetYearTotalOutput()
            //{
            //    YearNum = nowyear,
            //    Receivable = Convert.ToDecimal(berthsecReportList.Sum(p => p.Receivable)),
            //    FactReceive = Convert.ToDecimal(berthsecReportList.Sum(p => p.FactReceive)),
            //    Arrearage = Convert.ToDecimal(berthsecReportList.Sum(p => p.Arrearage)),
            //    Repayment = Convert.ToDecimal(berthsecReportList.Sum(p => p.Repayment))
            //};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GetWeixinOutput GetWeixinCount()
        {
            GetWeixinOutput output = new GetWeixinOutput();
            output.AllRegisterCount = GetWeixinRegisterCount();
            output.NewRegisterCount = GetWeixinNewCount();
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetWeixinNewCount()
        {

            double hour = -double.Parse(DateTime.Now.Hour.ToString());
            double minute = -double.Parse(DateTime.Now.Minute.ToString());

            DateTime dt = DateTime.Now.AddHours(hour).AddMinutes(minute);

            return _abpWeixinTUserRepository.Count(entity => entity.registerDate >= dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetWeixinRegisterCount()
        {
            return _abpWeixinTUserRepository.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TitleModelDto TitleModelDto()
        {
            var todayTime = DateTime.Now.Date;
            var monthTime = todayTime.AddDays(1 - todayTime.Day);
            var todayData = _abpBusinessDetailRepository.GetAllList(entity => entity.Status == 2 && entity.CarPayTime >= todayTime);
            var monthData = _abpBusinessDetailRepository.GetAllList(entity => entity.Status == 2 && entity.CarPayTime >= monthTime);
            TitleModelDto titleModelDto = new TitleModelDto
            {
                TodayOrderSize = todayData.Count,
                MonthOrderSize = monthData.Count
            };
            if (titleModelDto.TodayOrderSize == 0)
            {
                titleModelDto.TodayMoney = 0;
            }
            else
            {
                titleModelDto.TodayMoney = todayData.Sum(entry => entry.FactReceive);
            }
            if(titleModelDto.MonthOrderSize == 0)
            {
                titleModelDto.MonthMoney = 0;
            }
            else
            {
                titleModelDto.MonthMoney = monthData.Sum(entry => entry.FactReceive);
            }
            return titleModelDto;
        }
    }
}
