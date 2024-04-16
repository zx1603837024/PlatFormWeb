using Abp.Domain.Repositories;
using P4.Businesses.Dtos;
using P4.Reports;
using P4.StaticReport.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Collectors.Dtos;
using P4.Employees;
using P4.Parks;
using P4.Berthsecs;
using P4.Companys;
using Abp.Linq.Extensions;

namespace P4.StaticReport
{
    public class StaticReportAppService : P4AppServiceBase, IStaticReportAppService
    {
        #region var
        private readonly IRepository<EmployeeReport> _abpEmployeeReportRepository;
        private readonly IRepository<BerthsecReport> _abpBerthsecReportRepository;
        private readonly IRepository<Employee, long> _abpEmployeeRepository;
        private readonly IRepository<Park> _abpParkRepository;
        private readonly IRepository<Berthsec> _abpBerthsecRepository;
        private readonly IRepository<OperatorsCompany> _abpOperatorsCompanyRepository;
        #endregion
        public StaticReportAppService(IRepository<EmployeeReport> abpEmployeeReportRepository, IRepository<BerthsecReport> abpBerthsecReportRepository,
            IRepository<Employee, long> abpEmployeeRepository, IRepository<Park> abpParkRepository,
            IRepository<Berthsec> abpBerthsecRepository, IRepository<OperatorsCompany> abpOperatorsCompanyRepository)
        {
            _abpEmployeeReportRepository = abpEmployeeReportRepository;
            _abpBerthsecReportRepository = abpBerthsecReportRepository;
            _abpEmployeeRepository = abpEmployeeRepository;
            _abpParkRepository = abpParkRepository;
            _abpBerthsecRepository = abpBerthsecRepository;
            _abpOperatorsCompanyRepository = abpOperatorsCompanyRepository;
        }
        /// <summary>
        /// 获取收费员Echar数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <returns></returns>
        public List<GetEmployeeReportTotalOutput> GetEmployeeReportEchar(StaticReportInput input)
        {
            GetMoneyInput getmoneyinput = new GetMoneyInput();
            getmoneyinput.BeginTime = input.begindt;
            getmoneyinput.EndTime = input.enddt;
            List<GetEmployeeReportTotalOutput> employeeCountList = new List<GetEmployeeReportTotalOutput>();
            var employeelist = _abpEmployeeReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).GroupBy(ber => ber.EmployeeId).OrderBy(ber => ber.Key).PageBy(input).ToList();
            //var employeelist = employeeReportlist.GroupBy(e => e.EmployeeId);
            if(!string.IsNullOrWhiteSpace(input.employeeIdInput)&&input.employeeIdInput!="0")
            {
                long employeeid = Convert.ToInt64(input.employeeIdInput);
                 employeelist = _abpEmployeeReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime&&ber.EmployeeId==employeeid).GroupBy(ber => ber.EmployeeId).OrderBy(ber => ber.Key).PageBy(input).ToList();
            }
            foreach (var employee in employeelist)
            {
                employeeCountList.Add(new GetEmployeeReportTotalOutput()
                {
                    EmployeeId = employee.Key,
                    EmployeeName = _abpEmployeeRepository.FirstOrDefault(em => em.Id == employee.Key) == null ? "" : _abpEmployeeRepository.FirstOrDefault(em => em.Id == employee.Key).TrueName,
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
            return employeeCountList;
        }
        /// <summary>
        /// 获取收费员jqgrid数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <param name="employeeIdInput"></param>
        /// <param name="employeeNameInput"></param>
        /// <returns></returns>
        public GetAllEmployeeReportOutput GetEmployeeReportGrid(StaticReportInput input)
        {
            List<GetEmployeeReportTotalOutput> employeeCountList = GetEmployeeReportEchar(input);
            int records = _abpEmployeeReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt).GroupBy(ber => ber.EmployeeId).ToList().Count;
           if(!string.IsNullOrWhiteSpace(input.employeeIdInput)&&input.employeeIdInput!="0")
           {
               long employeeid = Convert.ToInt64(input.employeeIdInput);
               records = _abpEmployeeReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt&&ber.EmployeeId==employeeid).GroupBy(ber => ber.EmployeeId).ToList().Count;
           }
            return new GetAllEmployeeReportOutput()
            {
                rows = employeeCountList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 获取停车场Echar数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <returns></returns>
        public List<GetParkTotalOutput> GetParkReportEchar(StaticReportInput input)
        {

            GetMoneyInput getmoneyinput = new GetMoneyInput();
            getmoneyinput.BeginTime = input.begindt;
            getmoneyinput.EndTime = input.enddt;
            List<GetParkTotalOutput> parkList = new List<GetParkTotalOutput>();
            var parks = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).GroupBy(ber => ber.ParkId).OrderBy(ber => ber.Key).PageBy(input).ToList();
            if(!string.IsNullOrWhiteSpace(input.parkIdInput)&&input.parkIdInput!="0")
            {
                int parkid = Convert.ToInt32(input.parkIdInput);
                parks = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime&&ber.ParkId==parkid).GroupBy(ber => ber.ParkId).OrderBy(ber => ber.Key).PageBy(input).ToList();
            }

            //var parks = berthsecReportList.GroupBy(bts => bts.ParkId);
            foreach (var park in parks)
            {
                parkList.Add(new GetParkTotalOutput()
                {
                    //BerthsecReportList=park,
                    ParkId = park.Key,
                    ParkName = _abpParkRepository.FirstOrDefault(pk => pk.Id == park.Key) == null ? "" : _abpParkRepository.FirstOrDefault(pk => pk.Id == park.Key).ParkName,
                    //ParkName=park.
                    Prepaid = Convert.ToDecimal(park.Sum(p => p.Prepaid)),
                    StopTime = Convert.ToInt32(park.Sum(p => p.StopTime)),
                    Receivable = Convert.ToDecimal(park.Sum(p => p.Receivable)),
                    FactReceive = Convert.ToDecimal(park.Sum(p => p.FactReceive)),
                    Arrearage = Convert.ToDecimal(park.Sum(p => p.Arrearage)),
                    Repayment = Convert.ToDecimal(park.Sum(p => p.Repayment)),
                    SensorsStopTime = Convert.ToInt32(park.Sum(p => p.SensorsStopTime)),
                    SensorsReceivable = Convert.ToDecimal(park.Sum(p => p.SensorsReceivable)),
                    Cash = Convert.ToDecimal(park.Sum(p => p.Cash)),
                    ByCard = (Convert.ToDecimal(park.Sum(p => p.FactReceive)) - Convert.ToDecimal(park.Sum(p => p.Cash)))
                });
            }
            return parkList;
        }
        /// <summary>
        /// 获取时间段停车场Echar数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GetParkTotalOutput> GetTimeBucketParkReportEchar(StaticReportInput input)
        {
            GetMoneyInput getmoneyinput = new GetMoneyInput();
            getmoneyinput.BeginTime = input.begindt;
            //string begintime = getmoneyinput.BeginTime.ToString("yyyy/M/d");
            //getmoneyinput.EndTime = input.enddt;
            List<GetParkTotalOutput> parkList = new List<GetParkTotalOutput>();
            var TimeBucketPark = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= input.beginEnddt).GroupBy(ber => new { ber.ParkId, ber.Time }).OrderBy(ber => ber.Key).PageBy(input).ToList();
            if (!string.IsNullOrWhiteSpace(input.parkIdInput) && input.parkIdInput != "0")
            {
                int parkId = Convert.ToInt32(input.parkIdInput);
                TimeBucketPark = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= input.beginEnddt && ber.ParkId == parkId).GroupBy(ber => new { ber.ParkId, ber.Time }).OrderBy(ber => ber.Key).PageBy(input).ToList();
            }
            foreach (var park in TimeBucketPark)
            {
                parkList.Add(new GetParkTotalOutput()
                {
                    //BerthsecReportList=park,
                    ParkId = park.Key.ParkId,
                    ParkName = _abpParkRepository.FirstOrDefault(pk => pk.Id == park.Key.ParkId) == null ? "" : _abpParkRepository.FirstOrDefault(pk => pk.Id == park.Key.ParkId).ParkName,
                    //ParkName=park.
                    Prepaid = Convert.ToDecimal(park.Sum(p => p.Prepaid)),
                    StopTime = Convert.ToInt32(park.Sum(p => p.StopTime)),
                    StopTimes = Convert.ToInt32(park.Sum(p => p.StopTimes)),
                    Receivable = Convert.ToDecimal(park.Sum(p => p.Receivable)),
                    FactReceive = Convert.ToDecimal(park.Sum(p => p.FactReceive)),
                    Arrearage = Convert.ToDecimal(park.Sum(p => p.Arrearage)),
                    Time = park.Key.Time.ToString(),
                    Repayment = Convert.ToDecimal(park.Sum(p => p.Repayment)),
                    Cash = Convert.ToDecimal(park.Sum(p => p.Cash)),
                    ByCard = (Convert.ToDecimal(park.Sum(p => p.FactReceive)) - Convert.ToDecimal(park.Sum(p => p.Cash)))
                    //SensorsStopTime = Convert.ToInt32(park.Sum(p => p.SensorsStopTime)),
                    //SensorsReceivable = Convert.ToDecimal(park.Sum(p => p.SensorsReceivable))
                });
            }
            return parkList;
        }
        /// <summary>
        /// 获取停车场Grid数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <returns></returns>
        public GetAllParkReportOutput GetParkReportGrid(StaticReportInput input)
        {
            List<GetParkTotalOutput> parkCountList = GetParkReportEchar(input);

            int records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt).GroupBy(ber => ber.ParkId).ToList().Count;
            if(!string.IsNullOrWhiteSpace(input.parkIdInput)&&input.parkIdInput!="0")
            {
                int parkid = Convert.ToInt32(input.parkIdInput);
                records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt&&ber.ParkId==parkid).GroupBy(ber => ber.ParkId).ToList().Count;
            }
            return new GetAllParkReportOutput()
            {
                rows = parkCountList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 获取时间段停车场Grid数据
        /// </summary>
        /// <returns></returns>
        public GetAllParkReportOutput GetTimeBucketParkReport(StaticReportInput input)
        {
            List<GetParkTotalOutput> parkCountList = GetTimeBucketParkReportEchar(input);
            int records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.beginEnddt).GroupBy(ber => new { ber.ParkId, ber.Time }).ToList().Count;
            if (!string.IsNullOrWhiteSpace(input.parkIdInput) && input.parkIdInput != "0")
            {
                int parkId = Convert.ToInt32(input.parkIdInput);
                records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.beginEnddt && ber.ParkId == parkId).GroupBy(ber => new { ber.ParkId, ber.Time }).ToList().Count;
            }
            return new GetAllParkReportOutput()
            {
                rows = parkCountList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }



        /// <summary>
        /// 获取泊位段Echar数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <returns></returns>
        public List<GetBerthsecReportTotalOutput> GetBerthsecReportEchar(StaticReportInput input)
        {
            GetMoneyInput getmoneyinput = new GetMoneyInput();
            getmoneyinput.BeginTime = input.begindt;
            getmoneyinput.EndTime = input.enddt;
            List<GetBerthsecReportTotalOutput> berthsecList = new List<GetBerthsecReportTotalOutput>();
            var berthsecs = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).GroupBy(ber => ber.BerthsecId).OrderBy(ber => ber.Key).PageBy(input).ToList();
            if(!string.IsNullOrWhiteSpace(input.BerthsecIdInput)&&input.BerthsecIdInput!="0")
            {
              int  BerthsecId = Convert.ToInt32(input.BerthsecIdInput);
                berthsecs = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime&&ber.BerthsecId==BerthsecId).GroupBy(ber => ber.BerthsecId).OrderBy(ber => ber.Key).PageBy(input).ToList();
            }
            
            //var berthsecs = berthsecReportList.GroupBy(bts => bts.BerthsecId);

            foreach (var berthsec in berthsecs)
            {
                double a= 0;
                double factreceive = (double)(berthsec.Sum(ber => ber.FactReceive));
                double receivable = (double)(berthsec.Sum(ber => ber.Receivable));
                if (receivable != 0)
                {
                    a = factreceive / receivable;
                }
                double b = 0;
                double berthsecbycard = (double)((berthsec.Sum(oc => oc.FactReceive)) - (berthsec.Sum(oc => oc.Cash)));
                if (receivable != 0)
                {
                    b = berthsecbycard / receivable;
                }
                berthsecList.Add(new GetBerthsecReportTotalOutput()
                {
                    //BerthsecReportList=park,
                    BerthsecId = berthsec.Key,
                    BerthsecName = _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key) == null ? "" : _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key).BerthsecName,
                    //ParkName=park.
                    Prepaid = Convert.ToDecimal(berthsec.Sum(ber => ber.Prepaid)),
                    StopTime = Convert.ToInt32(berthsec.Sum(ber => ber.StopTime)),
                    Receivable = Convert.ToDecimal(berthsec.Sum(ber => ber.Receivable)),
                    FactReceive = Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)),
                    Arrearage = Convert.ToDecimal(berthsec.Sum(ber => ber.Arrearage)),
                    Repayment = Convert.ToDecimal(berthsec.Sum(ber => ber.Repayment)),
                   //FactPercent = (Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)) / Convert.ToDecimal(berthsec.Sum(ber => ber.Receivable))).ToString(),
                  
                    FactPercent = string.Format("{0:P}", a),
                    ByCardPercent=string.Format("{0:P}",b),
                   
                   //FactPercent="1",
                    
                    CarInCount =_abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).GroupBy(ber=>ber.BerthsecId).ToList().Count(),
                    CarOutCount =_abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).ToList().Count,
                    Cash = Convert.ToDecimal(berthsec.Sum(ber => ber.Cash)),
                     BerthsecByCard= (Convert.ToDecimal(berthsec.Sum(oc => oc.FactReceive)) - Convert.ToDecimal(berthsec.Sum(oc => oc.Cash))),
                    SensorsStopTime = Convert.ToInt32(berthsec.Sum(ber => ber.SensorsStopTime)),
                    SensorsReceivable = Convert.ToDecimal(berthsec.Sum(ber => ber.SensorsReceivable))
                });
            }
            return berthsecList;
        }
        /// <summary>
        /// 获取时间段泊位段Echar数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GetBerthsecReportTotalOutput> GetTBucketBerthsecEchar(StaticReportInput input)
        {
            GetMoneyInput getmoneyinput = new GetMoneyInput();
            getmoneyinput.BeginTime = input.begindt;
            getmoneyinput.EndTime = input.enddt;
            input.rows = 2400;
            List<GetBerthsecReportTotalOutput> berthsecList = new List<GetBerthsecReportTotalOutput>();

            int berthsecID = Convert.ToInt32(input.BerthsecIdInput);
            var berthsecs = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime && ber.BerthsecId == berthsecID).GroupBy(ber => new { ber.BerthsecId, ber.Time }).OrderBy(ber => ber.Key).PageBy(input).ToList();


            if (input.operateDateBegin == input.operateDateEnd)
            {
                foreach (var berthsec in berthsecs)
                {
                    berthsecList.Add(new GetBerthsecReportTotalOutput()
                    {
                        //BerthsecReportList=park,
                        BerthsecId = berthsec.Key.BerthsecId,
                        BerthsecName = _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key.BerthsecId) == null ? "" : _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key.BerthsecId).BerthsecName,
                        //ParkName=park.
                        Prepaid = Convert.ToDecimal(berthsec.Sum(ber => ber.Prepaid)),  //该时间段泊位段预付
                        StopTime = Convert.ToInt32(berthsec.Sum(ber => ber.StopTime)),//该时间段泊位段停车时长
                        Receivable = Convert.ToDecimal(berthsec.Sum(ber => ber.Receivable)),//该时间段泊位段出场应收
                        FactReceive = Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)),//该时间段泊位段实收
                        Arrearage = Convert.ToDecimal(berthsec.Sum(ber => ber.Arrearage)),//该时间段泊位段欠费
                        Cash = Convert.ToDecimal(berthsec.Sum(ber => ber.Cash)),
                        ByCard = (Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)) - Convert.ToDecimal(berthsec.Sum(ber => ber.Cash))),
                        // Repayment = Convert.ToDecimal(berthsec.Sum(ber => ber.Repayment))//该时间段泊位段补缴
                        Time = berthsec.Key.Time.ToString()   //时间段
                    });
                }
            }
            else
            {
                foreach (var berthsec in berthsecs)
                {
                    berthsecList.Add(new GetBerthsecReportTotalOutput()
                    {
                        //BerthsecReportList=park,
                        BerthsecId = berthsec.Key.BerthsecId,
                        BerthsecName = _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key.BerthsecId) == null ? "" : _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key.BerthsecId).BerthsecName,
                        //ParkName=park.
                        Prepaid = Convert.ToDecimal(berthsec.Sum(ber => ber.Prepaid)),  //该时间段泊位段预付
                        StopTime = Convert.ToInt32(berthsec.Sum(ber => ber.StopTime)),//该时间段泊位段停车时长
                        Receivable = Convert.ToDecimal(berthsec.Sum(ber => ber.Receivable)),//该时间段泊位段出场应收
                        FactReceive = Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)),//该时间段泊位段实收
                        Arrearage = Convert.ToDecimal(berthsec.Sum(ber => ber.Arrearage)),//该时间段泊位段欠费
                        Cash = Convert.ToDecimal(berthsec.Sum(ber => ber.Cash)),
                        ByCard = (Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)) - Convert.ToDecimal(berthsec.Sum(ber => ber.Cash))),
                        // Repayment = Convert.ToDecimal(berthsec.Sum(ber => ber.Repayment))//该时间段泊位段补缴
                        Time = berthsec.Key.Time.ToShortDateString()   //时间段
                    });
                }

                var temp = berthsecList.GroupBy(ber => new { ber.BerthsecId, ber.Time }).ToList();
                berthsecList = new List<GetBerthsecReportTotalOutput>();
                foreach (var berthsec in temp)
                {
                    berthsecList.Add(new GetBerthsecReportTotalOutput()
                    {
                        //BerthsecReportList=park,
                        BerthsecId = berthsec.Key.BerthsecId,
                        BerthsecName = _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key.BerthsecId) == null ? "" : _abpBerthsecRepository.FirstOrDefault(ber => ber.Id == berthsec.Key.BerthsecId).BerthsecName,
                        //ParkName=park.
                        Prepaid = Convert.ToDecimal(berthsec.Sum(ber => ber.Prepaid)),  //该时间段泊位段预付
                        StopTime = Convert.ToInt32(berthsec.Sum(ber => ber.StopTime)),//该时间段泊位段停车时长
                        Receivable = Convert.ToDecimal(berthsec.Sum(ber => ber.Receivable)),//该时间段泊位段出场应收
                        FactReceive = Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)),//该时间段泊位段实收
                        Arrearage = Convert.ToDecimal(berthsec.Sum(ber => ber.Arrearage)),//该时间段泊位段欠费
                        Cash = Convert.ToDecimal(berthsec.Sum(ber => ber.Cash)),
                        ByCard = (Convert.ToDecimal(berthsec.Sum(ber => ber.FactReceive)) - Convert.ToDecimal(berthsec.Sum(ber => ber.Cash))),
                        // Repayment = Convert.ToDecimal(berthsec.Sum(ber => ber.Repayment))//该时间段泊位段补缴
                        Time = berthsec.Key.Time   //时间段
                    });
                }

            }
            return berthsecList;
        }
        /// <summary>
        /// 获取泊位段Grid数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <returns></returns>
        public GetAllBerthsecReportOutput GetBerthsecReportGrid(StaticReportInput input)
        {

            List<GetBerthsecReportTotalOutput> berthsecCountList = GetBerthsecReportEchar(input);
            int records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt).GroupBy(ber => ber.BerthsecId).ToList().Count;
            if(!string.IsNullOrWhiteSpace(input.BerthsecIdInput)&&input.BerthsecIdInput!="0")
            {
                int berthsecid = Convert.ToInt32(input.BerthsecIdInput);
                records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt&&ber.BerthsecId==berthsecid).GroupBy(ber => ber.BerthsecId).ToList().Count;
            }
            return new GetAllBerthsecReportOutput()
            {
                rows = berthsecCountList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 
        /// 获取时间段泊位段Grid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBerthsecReportOutput GetTBucketBerthsecGrid(StaticReportInput input)
        {
            List<GetBerthsecReportTotalOutput> berthsecCountList = GetTBucketBerthsecEchar(input);
            int records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.beginEnddt).GroupBy(ber => new { ber.BerthsecId, ber.Time }).ToList().Count;
            if (!string.IsNullOrWhiteSpace(input.BerthsecIdInput) && input.BerthsecIdInput != "0")
            {
                int berthsecID = Convert.ToInt32(input.BerthsecIdInput);
                records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.beginEnddt && ber.BerthsecId == berthsecID).GroupBy(ber => new { ber.BerthsecId, ber.Time }).ToList().Count;
            }
            return new GetAllBerthsecReportOutput()
            {
                rows = berthsecCountList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 获取分公司Echar数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <returns></returns>
        public List<GetOperatorsCompanyTotalOutput> GetOperatorsCompanyReportEchar(StaticReportInput input)
        {
            GetMoneyInput getmoneyinput = new GetMoneyInput();
            getmoneyinput.BeginTime = input.begindt;
            getmoneyinput.EndTime = input.enddt;
            List<GetOperatorsCompanyTotalOutput> operatorsCompanyList = new List<GetOperatorsCompanyTotalOutput>();
            var operatorsCompanys = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).GroupBy(ber => ber.CompanyId).OrderBy(ber => ber.Key).PageBy(input).ToList();
           if(!string.IsNullOrWhiteSpace(input.CompanyIdInput)&&input.CompanyIdInput!="0")
           {
               int companyid = Convert.ToInt32(input.CompanyIdInput);
               operatorsCompanys = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime&&ber.CompanyId==companyid).GroupBy(ber => ber.CompanyId).OrderBy(ber => ber.Key).PageBy(input).ToList();
           }
            //var operatorsCompanys = berthsecReportList.GroupBy(bts => bts.CompanyId);
            foreach (var operatorsCompany in operatorsCompanys)
            {
                operatorsCompanyList.Add(new GetOperatorsCompanyTotalOutput()
                {
                    //BerthsecReportList=park,
                    CompanyId = operatorsCompany.Key,
                    CompanyName = _abpOperatorsCompanyRepository.FirstOrDefault(oc => oc.Id == operatorsCompany.Key) == null ? "" : _abpOperatorsCompanyRepository.FirstOrDefault(pk => pk.Id == operatorsCompany.Key).CompanyName,
                    //ParkName=park.
                    Prepaid = Convert.ToDecimal(operatorsCompany.Sum(oc => oc.Prepaid)),
                    StopTime = Convert.ToInt32(operatorsCompany.Sum(oc => oc.StopTime)),
                    Receivable = Convert.ToDecimal(operatorsCompany.Sum(oc => oc.Receivable)),
                    FactReceive = Convert.ToDecimal(operatorsCompany.Sum(oc => oc.FactReceive)),
                    Arrearage = Convert.ToDecimal(operatorsCompany.Sum(oc => oc.Arrearage)),
                    Repayment = Convert.ToDecimal(operatorsCompany.Sum(oc => oc.Repayment)),
                    Cash=Convert.ToDecimal(operatorsCompany.Sum(oc=>oc.Cash)),
                    ByCard = (Convert.ToDecimal(operatorsCompany.Sum(oc => oc.FactReceive))- Convert.ToDecimal(operatorsCompany.Sum(oc => oc.Cash))),
                    SensorsStopTime = Convert.ToInt32(operatorsCompany.Sum(oc => oc.SensorsStopTime)),
                    SensorsReceivable = Convert.ToDecimal(operatorsCompany.Sum(oc => oc.SensorsReceivable))
                });
            }
            return operatorsCompanyList;
        }
        /// <summary>
        /// 获取分公司Grid数据
        /// </summary>
        /// <param name="begindt"></param>
        /// <param name="enddt"></param>
        /// <returns></returns>
        public GetAllOperatorsCompanyReportOutput GetOperatorsCompanyReportGrid(StaticReportInput input)
        {
            List<GetOperatorsCompanyTotalOutput> operatorsCompanyCountList = GetOperatorsCompanyReportEchar(input);

            int records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt).GroupBy(ber => ber.CompanyId).ToList().Count;
            if(!string.IsNullOrWhiteSpace(input.CompanyIdInput)&&input.CompanyIdInput!="0")
            {
                int companyid = Convert.ToInt32(input.CompanyIdInput);
                records = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= input.begindt && ber.Time <= input.enddt).GroupBy(ber => ber.CompanyId).ToList().Count;
            }
            return new GetAllOperatorsCompanyReportOutput()
            {
                rows = operatorsCompanyCountList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
