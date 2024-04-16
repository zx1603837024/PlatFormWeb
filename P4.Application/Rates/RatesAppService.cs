using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Rates.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using P4.Berthsecs;
using System.Web.Script.Serialization;
using Abp.AutoMapper;
using P4.Businesses;
using P4.Sensors.Dtos;
using System.Data;
using System.Data.SqlClient;

namespace P4.Rates
{
    /// <summary>
    /// 
    /// </summary>
    public class RatesAppService : ApplicationService, IRatesAppService
    {
        #region  Var
        private readonly IRepository<Rate, int> _abpRateRepository;
        private readonly IRepository<Berthsec> _abpBerthsecRepository;
        private readonly IRepository<BusinessDetail, long> _abpBusinessDetailRepository;
        private int freecount = 0;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpRateRepository"></param>
        /// <param name="abpBerthsecRepository"></param>
        /// <param name="abpBusinessDetailRepository"></param>
        public RatesAppService(IRepository<Rate, int> abpRateRepository, IRepository<Berthsec> abpBerthsecRepository,
            IRepository<BusinessDetail, long> abpBusinessDetailRepository)
        {
            _abpRateRepository = abpRateRepository;
            _abpBerthsecRepository = abpBerthsecRepository;
            _abpBusinessDetailRepository = abpBusinessDetailRepository;
            freecount = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllRateOutput GetAllRateList(RateInput input)
        {

            int records = _abpRateRepository.GetAll().Filters(input).ToList().Count;

            List<RateDto> rateDtoList = new List<RateDto>();

            List<Rate> rateliset = _abpRateRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList();
            if (rateliset == null)
                return null;
            foreach (Rate rate in rateliset)
            {
                RateDto ratedto = new RateDto();
                ratedto.Id = rate.Id;
                ratedto.LastModifierUserName = rate.LastModifierUser == null ? null : rate.LastModifierUser.Name;
                ratedto.TenantName = rate.Tenant == null ? null : rate.Tenant.Name;
                ratedto.TenantId = rate.TenantId;
                ratedto.CompanyId = rate.CompanyId;
                ratedto.CompanyName = rate.OperatorsCompany == null ? null : rate.OperatorsCompany.CompanyName;
                ratedto.CreatorUserName = rate.CreatorUser == null ? null : rate.CreatorUser.Name;
                ratedto.RateName = rate.RateName;
                ratedto.RateJson = rate.RateJson;
                ratedto.RatePDA = rate.RatePDA;
                ratedto.Remark = rate.Remark;
                ratedto.IsActive = rate.IsActive;
                ratedto.CreationTime = rate.CreationTime;
                ratedto.LastModificationTime = rate.LastModificationTime;

                rateDtoList.Add(ratedto);
            }
            return new GetAllRateOutput()
            {
                //rows = _abpRoleRepository.GetAll().OrderByDescending(dic => dic.Id).PageBy(input).ToList().MapTo<List<RoleDto>>(),
                rows = rateDtoList,
                records = records,
                total = records / input.rows + 1
            };


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RateDto GetRateById(int id)
        {
            Rate rate = _abpRateRepository.FirstOrDefault(dic => dic.Id == id);
            RateDto ratedto = new RateDto();
            ratedto.Id = rate.Id;
            ratedto.LastModifierUserName = rate.LastModifierUser == null ? null : rate.LastModifierUser.Name;
            ratedto.TenantName = rate.Tenant == null ? null : rate.Tenant.Name;
            ratedto.TenantId = rate.TenantId;
            ratedto.CompanyId = rate.CompanyId;
            ratedto.CompanyName = rate.OperatorsCompany?.CompanyName;
            ratedto.CreatorUserName = rate.CreatorUser == null ? null : rate.CreatorUser.Name;
            ratedto.RateName = rate.RateName;
            ratedto.RateJson = rate.RateJson;
            ratedto.RatePDA = rate.RatePDA;
            ratedto.Remark = rate.Remark;
            ratedto.IsActive = rate.IsActive;
            ratedto.CreationTime = rate.CreationTime;
            ratedto.LastModificationTime = rate.LastModificationTime;
            return ratedto;

        }

        /// <summary>
        /// 删除费率
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Delete(RateSaveOrUpdateDto input)
        {
            var entity = _abpRateRepository.Load(input.Id);
            if (_abpBerthsecRepository.FirstOrDefault(dic => dic.RateId == input.Id) != default(Berthsec))
                return 1;
            _abpRateRepository.Delete(input.Id);
            return 0;
        }

        /// <summary>
        /// 编辑费率
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Edit(RateModel model)
        {
            Rate rate = _abpRateRepository.Load(model.Id);

            if (rate.RateName != model.RateName && _abpRateRepository.FirstOrDefault(dic => dic.RateName == model.RateName) != default(Rate))
                return 1;
            if (_abpBerthsecRepository.FirstOrDefault(dic => dic.RateId == model.Id) != default(Berthsec) && model.IsActive == false)
                return 2;
            JavaScriptSerializer js = new JavaScriptSerializer();
            string rateJson = js.Serialize(model);


            rate.Id = model.Id;
            rate.RateName = model.RateName;
            rate.RateJson = rateJson;
            rate.RatePDA = "";
            rate.Remark = model.Remark;
            rate.IsActive = model.IsActive;
            return _abpRateRepository.Update(rate) == null ? 1 : 0;

        }

        /// <summary>
        /// 费率增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(RateModel model)
        {
            if (_abpRateRepository.FirstOrDefault(dic => dic.RateName == model.RateName) != default(Rate))
            {
                //已存在typeCode
                return 1;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();


            string rateJson = js.Serialize(model);

            Rate rate = new Rate
            {
                RateName = model.RateName,
                RateJson = rateJson,
                RatePDA = "",
                Remark = model.Remark,
                IsActive = model.IsActive
            };
            return _abpRateRepository.Insert(rate) == null ? 1 : 0;
        }

        #region 正式版本费率Id不要 增加车牌号码
        /// <summary>
        /// 备注：正式版本费率Id不要 增加车牌号码
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="inCarTime"></param>
        /// <param name="outCarTime"></param>
        /// <param name="cartype"></param>
        /// <param name="RateId"></param>
        /// <returns></returns>
        //public RateCalculateModel RateCalculate(int berthsecID, DateTime inCarTime, DateTime outCarTime, int cartype, int RateId)
        //{
        //    string PlateNumber = "";
        //    RateCalculateModel rateclaculatemodel = new RateCalculateModel();

        //    Berthsec berthsec = _abpBerthsecRepository.Get(berthsecID);
        //    //berthsec.RateId = 43;
        //    Rate rate = _abpRateRepository.Get(berthsec.RateId);//早班费率
        //    RateModel ratemodel = Newtonsoft.Json.JsonConvert.DeserializeObject<RateModel>(rate.RateJson);
        //    #region 时间合理性和费率是否启用
        //    if (inCarTime > outCarTime)
        //    {
        //        rateclaculatemodel.exceptionMsg = "入场时间大于出场时间";
        //        return rateclaculatemodel;
        //    }
        //    if (ratemodel.IsActive == false)
        //    {
        //        rateclaculatemodel.exceptionMsg = "费率未启用";
        //        return rateclaculatemodel;
        //    }
        //    #endregion
        //    double totalParkTime = 0;
        //    TimeSpan ts = outCarTime.Date.Subtract(inCarTime.Date);
        //    int days = ts.Days;

        //    #region 对停车时间分天处理

        //    //设置list 对于多天收费分段保存收费时间
        //    //对头尾进行处理
        //    List<ParkTime> timelist = new List<ParkTime>();
        //    double qEnd = 0;//结束时间段 为前一个的开始
        //    for (int i = 0; i <= days; i++)
        //    {
        //        ParkTime parktime = new ParkTime();
        //        parktime.beginTime = Convert.ToDateTime(inCarTime.AddDays(i).Date.ToString("yyyy-MM-dd") + " " + ratemodel.TimeSettingList[0].beginTime);
        //        parktime.endTime = Convert.ToDateTime(inCarTime.AddDays(i).Date.ToString("yyyy-MM-dd") + " " + ratemodel.TimeSettingList[0].endTime);
        //        if (i == 0)
        //        {
        //            parktime.beginTime = DateTime.Compare(inCarTime, parktime.beginTime) > 0 ? inCarTime : parktime.beginTime;
        //            parktime.quantumBegin = 0;
        //        }
        //        if (i == days)
        //        {
        //            parktime.endTime = DateTime.Compare(parktime.endTime, outCarTime) > 0 ? outCarTime : parktime.endTime;
        //        }
        //        parktime.timeTotal = (parktime.endTime - parktime.beginTime).TotalMinutes>0 ? (parktime.endTime - parktime.beginTime).TotalMinutes:0;
        //        if (ratemodel.TimeSettingList[0].ManyDayMethod == "1")//0  按停车总和  1  分天计算
        //        {
        //            parktime.quantumBegin = 0;
        //            parktime.quantumEnd =  parktime.timeTotal>0?parktime.timeTotal:0 ;
        //        }
        //        else
        //        {
        //            parktime.quantumBegin = qEnd;
        //            parktime.quantumEnd = (qEnd + parktime.timeTotal ) >0?( qEnd + parktime.timeTotal):0 ;
        //            qEnd = parktime.quantumEnd;
        //        }

        //        totalParkTime += parktime.timeTotal;
        //        rateclaculatemodel.ParkTime += parktime.timeTotal;
        //        timelist.Add(parktime);
        //    }


        //    #endregion
        //    #region 获取车辆入场前当天已收金额
        //    List<BusinessDetail> carBusinessDetails = _abpBusinessDetailRepository.GetAll().Where(bd => bd.CarPayTime >= inCarTime.Date && bd.CarPayTime <= inCarTime && bd.PlateNumber == PlateNumber).ToList();
        //    timelist[0].alreadyCharge = carBusinessDetails.Sum(bd => bd.FactReceive);
        //    #endregion
        //    foreach (CarRateModel carfee in ratemodel.CarRateList)
        //    {
        //        if (cartype == int.Parse(carfee.CarType))//车辆类型
        //        {
        //            #region 免费时段
        //            double freeTime = Convert.ToDouble(carfee.FreeTime);
        //            if (totalParkTime <= freeTime)//如果收费时间总和小于免费时间 收费金额为0
        //            {
        //                rateclaculatemodel.CalculateMoney = 0;
        //                return rateclaculatemodel;
        //            }
        //            #endregion
        //            #region 按次收费
        //            if (ratemodel.TimeSettingList[0].RateMethod == "1") //收费方式（0 按时  1 按次）
        //            {
        //                //跨天按多次
        //                rateclaculatemodel.CalculateMoney = carfee.CarFeeScaleList[0].RateMoney * timelist.Count;
        //                return rateclaculatemodel;
        //            }
        //            #endregion
        //            #region 跨天计费方式 按停车总和
        //            if (ratemodel.TimeSettingList[0].ManyDayMethod == "0")//0  按停车总和  1  分天计算
        //            {
        //                #region 免费时间是否包含在收费时间段内 不包含需要减去1次 计算减去金额
        //                if (carfee.ContentFreeTimeFlag == false)//免费时间是否包含在收费时间段内 不包含需要减去1次 计算减去金额
        //                {
        //                    foreach (ParkTime pt in timelist)
        //                    {
        //                        if (freeTime > 0)
        //                        {
        //                            pt.timeTotal -= freeTime;
        //                            if (pt.timeTotal > 0)//免费时间减去了
        //                            {
        //                                freeTime = 0; break;
        //                            }
        //                            else
        //                            {
        //                                pt.timeTotal = 0;
        //                                freeTime -= pt.timeTotal;
        //                            }

        //                        }
        //                        else break;
        //                    }
        //                }
        //                #endregion
        //                #region 时间段计算  只计算一次
        //                //时间段只计算一次
        //                var CarTimeQuantumListOrder = carfee.CarTimeQuantumList.OrderBy(a => a.beginTime);
        //                foreach (CarTimeQuantumModel tq in CarTimeQuantumListOrder)
        //                {
        //                    double btime = tq.beginTime;//开始时间段
        //                    double etime = tq.endTime;//结束时间段
        //                    if (tq.beginTime >= tq.endTime)
        //                        continue;
        //                    //查找时间段对应的天数 比如多天数 第一天 第二天 一般用不到  
        //                    /*处理情况如下：
        //                     * 停车时间             第一天 0-0.5h                   第二天 全天9.5h                  第三天 4.5h 
        //                     * timelist时间段为     0-0.5 (0-30min)                 0.5-10  (30-600min)             10-14.5  (600-870min)
        //                     * 时间段设置为         0.1-0.2(6-12min)   5元           0.4-1(24-60min)    8元          10-15(600-900min)   5元
        //                     * 计算后 timelist[0]  timeTotal:0.5-0.1-0.1=0.3  parkMoney:5+8=13
        //                     * timelist[1]  timeTotal:9.5-0.5=9  parkMoney:0
        //                     * timelist[2]  timeTotal:4.5-3=2.5  parkMoney:5
        //                    */
        //                    foreach (ParkTime pt in timelist)
        //                    {


        //                        if (btime >= pt.quantumBegin && etime <= pt.quantumEnd)//1、如若费率时间段在同一天时间段内
        //                        {
        //                            pt.timeTotal -= (etime - btime);//把当天的停车时间减去时间段内的时长
        //                            pt.parkMoney += Convert.ToDecimal(tq.TimeQuantumMoney);
        //                            break;
        //                        }
        //                        else if (btime >= pt.quantumBegin && btime < pt.quantumEnd && etime > pt.quantumEnd)//2、若果费率开始时间段在当天时间段内 将费率时间段金额算入当天
        //                        {
        //                            pt.timeTotal -= (pt.quantumEnd - btime);
        //                            pt.parkMoney += Convert.ToDecimal(tq.TimeQuantumMoney);
        //                        }
        //                        else if (btime < pt.quantumBegin && etime >= pt.quantumBegin && etime < pt.quantumEnd)//3、如果费率结束时间段在当天时间段内 将费率时间段金额算入当天 因为2已经算过了
        //                        {
        //                            pt.timeTotal -= (etime - pt.quantumBegin);//
        //                            break;
        //                        }
        //                        else if (btime < pt.quantumBegin && etime >= pt.quantumEnd)//4、如果费率开始时间小于当天停车开始时间 费率结束时间大于当天停车时间 
        //                        {
        //                            pt.timeTotal = 0;
        //                        }
        //                    }

        //                }
        //                #endregion
        //                #region 剩余时间计算
        //                if (Convert.ToDouble(carfee.CarFeeScaleList[0].RateTime) > 0)
        //                {
        //                    double overplusTime = 0;//多计算的时间 比如第一天停车剩余30分钟 每小时2元 30-60=-30 多计算的30分钟要算在第二天里减去

        //                    //剩余时长计算
        //                    foreach (ParkTime pt in timelist)
        //                    {
        //                        pt.timeTotal += overplusTime;//overplusTime<=0  
        //                        //overplusTime += pt.timeTotal;
        //                        while (pt.timeTotal > 0)
        //                        {
        //                            pt.parkMoney += carfee.CarFeeScaleList[0].RateMoney;
        //                            pt.timeTotal = pt.timeTotal - Convert.ToDouble(carfee.CarFeeScaleList[0].RateTime);
        //                        }
        //                        overplusTime = pt.timeTotal;
        //                        //先计算次最大收费 然后在计算日最大收费
        //                        pt.parkMoney = pt.parkMoney <= carfee.OnceMaxMoney ? pt.parkMoney : carfee.OnceMaxMoney;
        //                        if (pt.alreadyCharge >= carfee.DayMaxMoney && carfee.DayMaxMoney!=0)
        //                        {
        //                            pt.parkMoney = 0;
        //                        }
        //                        else if (pt.alreadyCharge + pt.parkMoney >= carfee.DayMaxMoney)
        //                        {
        //                            pt.parkMoney = carfee.DayMaxMoney - pt.alreadyCharge;
        //                        }
        //                        rateclaculatemodel.CalculateMoney += pt.parkMoney;
        //                    }
        //                }
        //                #endregion

        //            }
        //            #endregion
        //            #region 跨天计费方式 分天计算
        //            else if (ratemodel.TimeSettingList[0].ManyDayMethod == "1")//0  按停车总和  1  分天计算
        //            {
        //                #region 免费时间不包含在收费时间段内 需要每天都减去
        //                if (carfee.ContentFreeTimeFlag == false)//免费时间是否包含在收费时间段内 不包含需要每天都减去 计算减去金额
        //                {
        //                    foreach (ParkTime pt in timelist)
        //                    {
        //                        pt.timeTotal -= freeTime;
        //                        pt.timeTotal = pt.timeTotal > 0 ? pt.timeTotal : 0;
        //                    }
        //                }

        //                #endregion
        //                #region 时间段计算 每天重复

        //                foreach (ParkTime pt in timelist)
        //                {
        //                    if (carfee.ContentFreeTimeFlag == true && pt.timeTotal <= freeTime)
        //                    {
        //                        pt.timeTotal = 0;
        //                        pt.parkMoney = 0;
        //                        continue;
        //                    }
        //                    var CarTimeQuantumListOrder = carfee.CarTimeQuantumList.OrderBy(a => a.beginTime);

        //                    foreach (CarTimeQuantumModel tq in CarTimeQuantumListOrder)
        //                    {
        //                        double btime = tq.beginTime;//开始时间段
        //                        double etime = tq.endTime;//结束时间段
        //                        if (tq.beginTime >= tq.endTime)
        //                            continue;
        //                        //查找时间段对应的天数 比如多天数 第一天 第二天 一般用不到  
        //                        /*处理情况如下：
        //                         * 停车时间             第一天 0-0.5h                   第二天 全天9.5h                  第三天 4.5h 
        //                         * timelist时间段为     0-0.5 (0-30min)                 0.5-10  (30-600min)             10-14.5  (600-870min)
        //                         * 时间段设置为         0.1-0.2(6-12min)   5元           0.4-1(24-60min)    8元          10-15(600-900min)   5元
        //                         * 计算后 timelist[0]  timeTotal:0.5-0.1-0.1=0.3  parkMoney:5+8=13
        //                         * timelist[1]  timeTotal:9.5-0.5=9  parkMoney:0
        //                         * timelist[2]  timeTotal:4.5-3=2.5  parkMoney:5
        //                        */

        //                        if (btime >= pt.quantumBegin && etime <= pt.quantumEnd)//1、如若费率时间段在同一天时间段内
        //                        {
        //                            pt.timeTotal -= (etime - btime);//把当天的停车时间减去时间段内的时长
        //                            pt.parkMoney += Convert.ToDecimal(tq.TimeQuantumMoney);

        //                        }
        //                        else if (btime >= pt.quantumBegin && btime < pt.quantumEnd && etime > pt.quantumEnd)//2、若果费率开始时间段在当天时间段内 将费率时间段金额算入当天
        //                        {
        //                            pt.timeTotal -= (pt.quantumEnd - btime);
        //                            pt.parkMoney += Convert.ToDecimal(tq.TimeQuantumMoney);
        //                        }   
        //                    }

        //                }
        //                #endregion
        //                #region 剩余时间计算
        //                if (Convert.ToDouble(carfee.CarFeeScaleList[0].RateTime) > 0)
        //                {                          
        //                    //剩余时长计算
        //                    foreach (ParkTime pt in timelist)
        //                    {                            
        //                        //overplusTime += pt.timeTotal;
        //                        while (pt.timeTotal > 0)
        //                        {
        //                            pt.parkMoney += carfee.CarFeeScaleList[0].RateMoney;
        //                            pt.timeTotal = pt.timeTotal - Convert.ToDouble(carfee.CarFeeScaleList[0].RateTime);
        //                        }                               
        //                        //先计算次最大收费 然后在计算日最大收费
        //                        pt.parkMoney = pt.parkMoney <= carfee.OnceMaxMoney ? pt.parkMoney : carfee.OnceMaxMoney;
        //                        if (carfee.DayMaxMoney != 0 && pt.alreadyCharge >= carfee.DayMaxMoney)
        //                        {
        //                            pt.parkMoney = 0;
        //                        }
        //                        else if (pt.alreadyCharge + pt.parkMoney >= carfee.DayMaxMoney)
        //                        {
        //                            pt.parkMoney = carfee.DayMaxMoney - pt.alreadyCharge;
        //                        }
        //                        rateclaculatemodel.CalculateMoney += pt.parkMoney;
        //                    }
        //                }
        //                #endregion
        //            }
        //            #endregion
        //        }
        //    }
        //    return rateclaculatemodel;
        //}
        #endregion


        /// <summary>
        /// 费率列表
        /// </summary>
        /// <returns></returns>
        public List<RateDto> GetAllRateName()
        {
            return _abpRateRepository.GetAllList(entry => entry.IsActive == true).MapTo<List<RateDto>>();
        }

        #region 费率处理

        /// <summary>
        /// 费率计算
        /// 增加包月车辆计算
        /// 白名单计算
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="inCarTime"></param>
        /// <param name="outCarTime"></param>
        /// <param name="cartype"></param>
        /// <param name="RateId"></param>
        /// <param name="parkId"></param>
        /// <param name="plateNumber"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public RateCalculateModel RateCalculate(int berthsecID, DateTime inCarTime, DateTime outCarTime, int cartype, int RateId, int parkId, string plateNumber, int companyId)
        {

            RateCalculateModel rateclaculatemodel = new RateCalculateModel();

            RateMode ratemode = new RateMode();

            string MonthyType = "";

            Berthsec berthsec = _abpBerthsecRepository.Get(berthsecID);
            if (!string.IsNullOrWhiteSpace(plateNumber))
            {
                string sql = "select count(1) as count from AbpWhiteList where IsDeleted = 0 and IsActive = 1 and RelateNumber = '" + plateNumber + "' and CompanyId = " + companyId;

                int count = int.Parse(Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, sql, null).ToString());
                if (count > 0)
                {
                    return rateclaculatemodel;
                }

                DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from AbpMonthlyCars with(nolock) where  PlateNumber = '"+ plateNumber +"' and IsDeleted = 0 and (case when CompanyId is null then AbpMonthlyCars.tenantid else " + berthsec.TenantId + "  end)  = " + berthsec.TenantId + " and (case when CompanyId is not null then AbpMonthlyCars.CompanyId else " + berthsec.CompanyId + " end) = " + berthsec.CompanyId + " and BeginTime <= getdate() and EndTime >= getdate() and (charindex(@parkid, ','+ ParkIds + ',') > 0 or ParkIds = '0')", new SqlParameter[] { new SqlParameter("@parkid", "," + berthsec.ParkId + ",") }).Tables[0];
                if (dt.Rows.Count > 0)
                    MonthyType = dt.Rows[0]["MonthyType"].ToString();
            }
           
            ratemode.rateModel = Newtonsoft.Json.JsonConvert.DeserializeObject<RateModel>(_abpRateRepository.Get(berthsec.RateId).RateJson);//早班费率

            if (inCarTime > outCarTime)
            {
                rateclaculatemodel.exceptionMsg = "入场时间大于出场时间";
                return rateclaculatemodel;
            }

            if (berthsec.RateId1.HasValue && berthsec.RateId1.Value != 0)
                ratemode.rateModel1 = Newtonsoft.Json.JsonConvert.DeserializeObject<RateModel>(_abpRateRepository.Get(berthsec.RateId1.Value).RateJson);//中班费率

            if (berthsec.RateId2.HasValue && berthsec.RateId2.Value != 0)
                ratemode.rateModel2 = Newtonsoft.Json.JsonConvert.DeserializeObject<RateModel>(_abpRateRepository.Get(berthsec.RateId2.Value).RateJson);//晚班费率

            if (ratemode.rateModel.IsActive == false)
            {
                rateclaculatemodel.exceptionMsg = "费率未启用";
                return rateclaculatemodel;
            }

            DateTime rateInCarTime = inCarTime.AddHours(-int.Parse(ratemode.rateModel.TimeSettingList[0].beginTime.Split(':')[0])).
                AddMinutes(-int.Parse(ratemode.rateModel.TimeSettingList[0].beginTime.Split(':')[1]));

            DateTime rateOutCarTime = outCarTime.AddHours(-int.Parse(ratemode.rateModel.TimeSettingList[0].beginTime.Split(':')[0])).
                AddMinutes(-int.Parse(ratemode.rateModel.TimeSettingList[0].beginTime.Split(':')[1]));

            TimeSpan ts = rateOutCarTime.Date.Subtract(rateInCarTime.Date);
            int days = ts.Days - 1;
            if (days > 0)
                rateclaculatemodel.CalculateMoney = ProcessDayMaxMoney(cartype, days, ratemode, MonthyType);
            rateclaculatemodel.ParkTime = (outCarTime - inCarTime).TotalMinutes;    //停车时长

            if (MonthyType == "MonthyAll")
            {
                rateclaculatemodel.CalculateMoney = 0;
                return rateclaculatemodel;
            }

            if (MonthyType != "MonthyMorning")
                rateclaculatemodel.CalculateMoney += ProcessCarTime(ratemode.rateModel, cartype, 0, rateInCarTime, rateOutCarTime, days, ratemode.rateModel.TimeSettingList[0].beginTime);   //早班

            if (MonthyType != "MonthyMiddle")
                rateclaculatemodel.CalculateMoney += ProcessCarTime(ratemode.rateModel1, cartype, 0, rateInCarTime, rateOutCarTime, days, ratemode.rateModel.TimeSettingList[0].beginTime);  //中班

            if (MonthyType != "MonthyNight")
                rateclaculatemodel.CalculateMoney += ProcessCarTime(ratemode.rateModel2, cartype, 0, rateInCarTime, rateOutCarTime, days, ratemode.rateModel.TimeSettingList[0].beginTime);  //晚班

            if (rateOutCarTime.Date > rateInCarTime.Date)
            {
                if (MonthyType != "MonthyMorning")
                    rateclaculatemodel.CalculateMoney += ProcessCarTime(ratemode.rateModel, cartype, 1, rateInCarTime, rateOutCarTime, days, ratemode.rateModel.TimeSettingList[0].beginTime);
                if (MonthyType != "MonthyMiddle")
                    rateclaculatemodel.CalculateMoney += ProcessCarTime(ratemode.rateModel1, cartype, 1, rateInCarTime, rateOutCarTime, days, ratemode.rateModel.TimeSettingList[0].beginTime);
                if (MonthyType != "MonthyNight")
                    rateclaculatemodel.CalculateMoney += ProcessCarTime(ratemode.rateModel2, cartype, 1, rateInCarTime, rateOutCarTime, days, ratemode.rateModel.TimeSettingList[0].beginTime);
            }
            return rateclaculatemodel;
        }


        /// <summary>
        /// 计算天最大金额
        /// </summary>
        /// <param name="CarType"></param>
        /// <param name="Days"></param>
        /// <param name="ratemode"></param>
        /// <param name="MonthyType"></param>
        /// <returns></returns>
        private decimal ProcessDayMaxMoney(int CarType, int Days, RateMode ratemode, string MonthyType)
        {
            decimal DayMaxMoney = 0;

            if (MonthyType == "MonthyAll")
                return DayMaxMoney;

            if (MonthyType != "MonthyMorning")
            {
                foreach (CarRateModel carfee in ratemode.rateModel.CarRateList)         //早班费率
                {
                    if (CarType == int.Parse(carfee.CarType))
                    {
                        if (ratemode.rateModel.TimeSettingList[0].RateMethod == "1")    // 按次
                        {
                            DayMaxMoney += (int)carfee.OnceMaxMoney;
                            break;
                        }
                        if (ratemode.rateModel.TimeSettingList[0].RateMethod == "0")    // 按时间
                        {
                            DayMaxMoney += (int)carfee.DayMaxMoney;
                            break;
                        }
                    }
                }
            }

            if (MonthyType != "MonthyMiddle")
            {
                if (ratemode.rateModel1 != null && ratemode.rateModel1.CarRateList != null && ratemode.rateModel1.IsActive)//中班费率
                {
                    foreach (CarRateModel carfee in ratemode.rateModel1.CarRateList)
                    {
                        if (CarType == int.Parse(carfee.CarType))
                        {
                            if (ratemode.rateModel1.TimeSettingList[0].RateMethod == "1")   //按次
                            {
                                DayMaxMoney += (int)carfee.OnceMaxMoney;
                                break;
                            }
                            if (ratemode.rateModel1.TimeSettingList[0].RateMethod == "0")   //按时间
                            {
                                DayMaxMoney += (int)carfee.DayMaxMoney;
                                break;
                            }
                        }
                    }
                }
            }

            if (MonthyType != "MonthyNight")
            {
                if (ratemode.rateModel2 != null && ratemode.rateModel2.CarRateList != null && ratemode.rateModel2.IsActive)    // 晚班费率
                {
                    foreach (CarRateModel carfee in ratemode.rateModel2.CarRateList)
                    {
                        if (CarType == int.Parse(carfee.CarType))
                        {
                            if (ratemode.rateModel2.TimeSettingList[0].RateMethod == "1")       // 按次
                            {
                                DayMaxMoney += (int)carfee.OnceMaxMoney;
                                break;
                            }
                            if (ratemode.rateModel2.TimeSettingList[0].RateMethod == "0")  // 按时间
                            {
                                DayMaxMoney += (int)carfee.DayMaxMoney;
                                break;
                            }
                        }
                    }
                }
            }
            return DayMaxMoney * Days;
        }

       /// <summary>
       /// 处理时间
       /// </summary>
       /// <param name="rateModel"></param>
       /// <param name="CarType"></param>
       /// <param name="Type"></param>
       /// <param name="rateInCarTime"></param>
       /// <param name="rateOutCarTime"></param>
       /// <param name="days"></param>
       /// <param name="temprate"></param>
       /// <returns></returns>
        private decimal ProcessCarTime(RateModel rateModel, int CarType, int Type, DateTime rateInCarTime, DateTime rateOutCarTime, int days, string temprate)
        {

            DateTime endTime = rateOutCarTime;
            DateTime beginTime = rateInCarTime;
            DateTime tempTime = rateInCarTime;

            if (rateModel == null)
                return 0;

            if (rateModel.CarRateList == null)
                return 0;
            if (Type == 1 && days < 0)
            {
                return 0;
            }

            ParkTime parktime = new ParkTime();


            if (days >= 0)
            {
                if (Type == 0)
                { // 进场时间
                    endTime = DateTime.Parse(beginTime.ToString("yyyy-MM-dd 23:59:59"));
                    tempTime = beginTime;
                }
                else
                { // 出场时间
                    beginTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd 00:00:00"));
                    tempTime = endTime;
                }
            }

            parktime.beginTime = DateTime.Parse(tempTime.ToString("yyyy-MM-dd " + rateModel.TimeSettingList[0].beginTime + ":00"));
            parktime.beginTime = parktime.beginTime.
                AddHours(-int.Parse(temprate.Split(':')[0])).
                AddMinutes(-int.Parse(temprate.Split(':')[1]));

            parktime.endTime = DateTime.Parse(tempTime.ToString("yyyy-MM-dd " + rateModel.TimeSettingList[0].endTime + ":00"));
            parktime.endTime = parktime.endTime.
                AddHours(-int.Parse(temprate.Split(':')[0])).
                AddMinutes(-int.Parse(temprate.Split(':')[1]));

            if (parktime.endTime < parktime.beginTime)
            {
                parktime.endTime = parktime.endTime.AddDays(1);
            }

            if (endTime <= parktime.beginTime)// 如果出场时间小于费率开始时间
                return 0;

            if (parktime.endTime <= beginTime && parktime.endTime > parktime.beginTime)
            {
                return 0;
            }

            if (beginTime > parktime.beginTime)// 如果停车时间大于费率开始时间
            {
                parktime.beginTime = beginTime;
            }

            if (endTime < parktime.endTime)
            {
                parktime.endTime = endTime;
            }

            parktime.timeTotal = (parktime.endTime - parktime.beginTime).TotalMinutes;

            if (rateModel.TimeSettingList[0].RateMethod == "1")// 按次收
            {
                foreach (CarRateModel carfee in rateModel.CarRateList)
                {
                    if (CarType == int.Parse(carfee.CarType))
                    {
                        if (int.Parse(carfee.FreeTime) >= parktime.timeTotal)
                            parktime.parkMoney = 0;
                        else
                            parktime.parkMoney = carfee.CarFeeScaleList[0].RateMoney;
                        break;
                    }
                }
                return parktime.parkMoney;
            }

            foreach (CarRateModel carfee in rateModel.CarRateList)
            {
                if (CarType == int.Parse(carfee.CarType))//车辆类型
                {
                    var CarTimeQuantumListOrder = carfee.CarTimeQuantumList.OrderBy(a => a.beginTime);
                    double freeTime = double.Parse(carfee.FreeTime);
                    double freeTimes = double.Parse("21");
                    if (parktime.timeTotal < freeTimes)// 如果收费时间总和小于免费时间 收费金额为0
                    {
                        parktime.parkMoney = 0;
                        return parktime.parkMoney;
                    }

                    if (carfee.ContentFreeTimeFlag == false && parktime.timeTotal > freeTime && freecount == 0)// 免费时间是否包含在收费时间段内
                    {
                        parktime.timeTotal -= freeTime;
                        freecount++;
                    }

                    for (int i = 0; i < carfee.CarTimeQuantumList.Count; i++)
                    {// 获取时间段的长度

                        if (carfee.CarTimeQuantumList[i].RateMethod == "0")
                        {// 判断时间段收费方式是分钟还是小时:
                         // 0分钟，1小时
                            if (parktime.timeTotal >= carfee.CarTimeQuantumList[i].beginTime
                                    && parktime.timeTotal <= carfee.CarTimeQuantumList[i].endTime && parktime.timeTotal != carfee.CarTimeQuantumList[i].beginTime)
                            {// 获取时间段的结束时间
                                for (int m = 0; m <= i; m++)
                                {
                                    parktime.parkMoney += decimal.Parse(carfee.CarTimeQuantumList[m].TimeQuantumMoney);
                                }
                            }
                            else if(parktime.timeTotal > carfee.CarTimeQuantumList[carfee.CarTimeQuantumList.Count - 1].endTime &&
                    i == carfee.CarTimeQuantumList.Count - 1)
                            {
                                // 超过时间段，xx元一分钟
                                decimal parktimeminutes = (decimal)(parktime.timeTotal- carfee.CarTimeQuantumList[carfee.CarTimeQuantumList.Count - 1].endTime);
                                for (int m = 0; m < carfee.CarTimeQuantumList.Count; m++)
                                {
                                    parktime.parkMoney += decimal.Parse(carfee.CarTimeQuantumList[m].TimeQuantumMoney);
                                }

                                if (carfee.CarFeeScaleList.Count > 0)
                                {
                                    parktime.parkMoney += Math.Ceiling(parktimeminutes / (int.Parse(carfee.CarFeeScaleList[0].RateTime)))
                                            * carfee.CarFeeScaleList[0].RateMoney;
                                }
                            }
                        }
                        else
                        {

                            if (parktime.timeTotal / 60 >= carfee.CarTimeQuantumList[i].beginTime / 60
                                    && parktime.timeTotal / 60 <= carfee.CarTimeQuantumList[i].endTime / 60)
                            {
                                for (int m = 0; m <= i; m++)
                                {
                                    parktime.parkMoney += decimal.Parse(carfee.CarTimeQuantumList[m].TimeQuantumMoney);
                                }
                            }
                            else
                            {
                                // 超过时间段，xx元一小时
                                double parktimeminutes = (parktime.timeTotal
                                        - carfee.CarTimeQuantumList[carfee.CarTimeQuantumList.Count - 1].endTime);
                                double hours = parktimeminutes % 60 == 0 ? parktimeminutes / 60
                                        : Math.Ceiling(parktimeminutes / 60);
                                for (int m = 0; m < carfee.CarTimeQuantumList.Count; m++)
                                {
                                    parktime.parkMoney += decimal.Parse(carfee.CarTimeQuantumList[m].TimeQuantumMoney);
                                }
                                if (carfee.CarTimeQuantumList[0].TimeQuantumMoney != null)
                                {
                                    parktime.parkMoney += (decimal)hours
                                            * decimal.Parse(carfee.CarTimeQuantumList[0].TimeQuantumMoney);
                                }
                            }
                        }
                    }
                }
            }
            decimal DayMaxMoney = 0;
            foreach (CarRateModel carfee in rateModel.CarRateList)
            {
                if (CarType == int.Parse(carfee.CarType))
                {
                    if (rateModel.TimeSettingList[0].RateMethod == "1")// 按次
                    {
                        DayMaxMoney += (int)carfee.OnceMaxMoney;
                        break;
                    }
                    if (rateModel.TimeSettingList[0].RateMethod == "0")// 按时间
                    {
                        DayMaxMoney += (int)carfee.DayMaxMoney;
                        break;
                    }
                }
            }

            if (parktime.parkMoney > DayMaxMoney)
                return DayMaxMoney;
            return parktime.parkMoney;
        }
        #endregion
    }
}