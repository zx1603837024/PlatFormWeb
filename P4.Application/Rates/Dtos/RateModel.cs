using Abp.Application.Services.Dto;
using P4.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Rates.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class RateModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 费率名称
        /// </summary>
        public string RateName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 车辆费率列表
        /// </summary>
        public List<CarRateModel> CarRateList { get; set; }
        /// <summary>
        /// 收费时间段列表
        /// </summary>
        public List<TimeSettingModel> TimeSettingList { get; set; }


        /// <summary>
        /// 计算停车金额 小型车
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public decimal AutoCalcConsume(DateTime beginTime, DateTime endTime)
        {
            var RateMethod = TimeSettingList[0].RateMethod;
            var parkTime = (double)(endTime - beginTime).Minutes;
            //按次
            if (RateMethod == "1")
            {
                var rate = CarRateList[1];
                if (string.IsNullOrWhiteSpace(rate.FreeTime))
                {
                    rate.FreeTime = "0";
                }
                parkTime -= double.Parse(rate.FreeTime);//减去免费时长
                return parkTime > 0 ? (CarRateList[1].CarFeeScaleList[0].RateMoney) : 0m;
            }
            else
            {
                var rate = CarRateList[1];
                if (string.IsNullOrWhiteSpace(rate.FreeTime))
                {
                    rate.FreeTime = "0";
                }
                var scalar = 0m;//总消费
                foreach (var timeQuantum in rate.CarTimeQuantumList)
                {
                    //按时
                    if (timeQuantum.RateMethod == "1")
                    {
                        timeQuantum.beginTime = (timeQuantum.beginTime == 0 || timeQuantum.beginTime == 1) ? 0 : timeQuantum.beginTime * 60;
                        timeQuantum.endTime = timeQuantum.endTime * 60;
                    }
                }
                //包含在计费时间段内
                if (!rate.ContentFreeTimeFlag)
                {
                    parkTime -= double.Parse(rate.FreeTime);//减去免费时长
                }
                
                foreach (var timeQuantum in rate.CarTimeQuantumList)
                {
                    if (timeQuantum.beginTime > parkTime)
                        break;
                    scalar += decimal.Parse(timeQuantum.TimeQuantumMoney);//增加时间段消费
                }
                parkTime -= rate.CarTimeQuantumList.Last().endTime;//减去最后一个时间段
                if (parkTime > 0)//如果还有剩余时间
                {
                    if (rate.CarFeeScaleList != null && rate.CarFeeScaleList.Count > 0)
                    {
                        if (rate.CarFeeScaleList[0].RateMethod == "1")//按时
                        {
                            //向上取整
                            scalar += (decimal)Math.Ceiling(Math.Ceiling(parkTime / 60) / double.Parse(rate.CarFeeScaleList[0].RateTime)) * (rate.CarFeeScaleList[0].RateMoney);
                        }
                        else
                        {
                            //向上取整
                            scalar += (decimal)Math.Ceiling(parkTime / double.Parse(rate.CarFeeScaleList[0].RateTime)) * (rate.CarFeeScaleList[0].RateMoney);
                        }
                    }
                }
                //return rate.DayMaxMoney == 0 ? scalar : (scalar > rate.DayMaxMoney ? rate.DayMaxMoney : scalar);
                return scalar;
            }
        }

        /// <summary>
        /// 获取最小消费金额
        /// </summary>
        /// <returns></returns>
        public decimal GetMinConsume()
        {
            var RateMethod = TimeSettingList[0].RateMethod;
            var rate = CarRateList[1];
            if (string.IsNullOrWhiteSpace(rate.FreeTime))
            {
                rate.FreeTime = "0";
            }
            if (double.Parse(rate.FreeTime) > 0) return 0;
            //按次
            if (RateMethod == "1")
            {
                return 0;
                //return  (CarRateList[1].CarFeeScaleList[0].RateMoney);
            }
            else
            {
                var arg1 = (rate.CarFeeScaleList[0].RateMoney);
                if (rate.CarTimeQuantumList.Count > 0)
                {
                    var arg2 = decimal.Parse(rate.CarTimeQuantumList[0].TimeQuantumMoney);
                    return arg2;
                }
                return arg1;
            }

        }

        /// <summary>
        /// 获取一个比较合理的停车时间
        /// </summary>
        /// <param name="beginTime">入场时间</param>
        /// <param name="scalar">消费金额</param>
        /// <returns></returns>
        public Tuple<DateTime, DateTime> GetRationalParkTime(DateTime beginTime, decimal scalar)
        {
            var RateMethod = TimeSettingList[0].RateMethod;
            var rate = CarRateList[1];
            if (string.IsNullOrWhiteSpace(rate.FreeTime))
            {
                rate.FreeTime = "0";
            }
            
            //按次
            if (RateMethod == "1")
            {
                var rateMoney = (CarRateList[1].CarFeeScaleList[0].RateMoney);
                //刚好消费金额=一次消费金额
                if (scalar>0  && Math.Ceiling(rateMoney / scalar) == 1)
                {
                    return new Tuple<DateTime, DateTime>(beginTime, beginTime.AddHours(3));
                }
                return new Tuple<DateTime, DateTime>(beginTime, beginTime.AddHours(2));
            }
            if (scalar == 0)
            {
                return new Tuple<DateTime, DateTime>(beginTime, beginTime.AddMinutes(double.Parse(rate.FreeTime)));
            }
            else
            {
                foreach (var timeQuantum in rate.CarTimeQuantumList)
                {
                    //按时
                    if (timeQuantum.RateMethod == "1")
                    {
                        timeQuantum.beginTime = (timeQuantum.beginTime == 0 || timeQuantum.beginTime == 1) ? 0 : timeQuantum.beginTime * 60;
                        timeQuantum.endTime = timeQuantum.endTime * 60;
                    }
                }
                //不包含在计费时间段内需要加上免费时长
                if (!rate.ContentFreeTimeFlag)
                {
                    beginTime = beginTime.AddMinutes(double.Parse(rate.FreeTime));
                }
                foreach (var timeQuantum in rate.CarTimeQuantumList)
                {
                    if (scalar <= 0)
                    {
                        return new Tuple<DateTime, DateTime>(beginTime.AddMinutes(timeQuantum.beginTime), beginTime.AddMinutes(timeQuantum.endTime));
                    }
                    scalar -= decimal.Parse(timeQuantum.TimeQuantumMoney);//增加时间段消费
                }
                if (scalar <= 0)
                {
                    return new Tuple<DateTime, DateTime>(beginTime.AddMinutes(rate.CarTimeQuantumList.Last().beginTime), beginTime.AddMinutes(rate.CarTimeQuantumList.Last().endTime));
                }
                beginTime = beginTime.AddMinutes(rate.CarTimeQuantumList.Last().endTime);//超过最后一个时间段

                if (rate.CarFeeScaleList != null && rate.CarFeeScaleList.Count > 0)
                {
                    if (rate.CarFeeScaleList[0].RateMethod == "1")//按时
                    {
                        var parkHours = Math.Ceiling((double)scalar / double.Parse(rate.CarFeeScaleList[0].RateTime));
                        return new Tuple<DateTime, DateTime>(beginTime, beginTime.AddHours(parkHours));
                    }
                    else
                    {
                        var parkMinutes = Math.Ceiling((double)scalar / double.Parse(rate.CarFeeScaleList[0].RateTime));
                        return new Tuple<DateTime, DateTime>(beginTime, beginTime.AddMinutes(parkMinutes));
                    }
                }
                return new Tuple<DateTime, DateTime>(beginTime, beginTime);
            }
        }
    }
    /// <summary>
    /// 车辆费率
    /// </summary>
    public class CarRateModel
    {
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 免费时间
        /// </summary>
        public string FreeTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ContentFreeTimeFlag { get; set; }

        /// <summary>
        /// 日最大收费金额 0表示无最大收费金额
        /// </summary>
        public decimal DayMaxMoney { get; set; }

        /// <summary>
        /// 次最大收费金额 0表示无最大收费金额
        /// </summary>
        public decimal OnceMaxMoney { get; set; }
        /// <summary>
        /// 时间段列表
        /// </summary>
        public List<CarTimeQuantumModel> CarTimeQuantumList { get; set; }
        /// <summary>
        /// 标准收费列表
        /// </summary>
        public List<CarFeeScaleModel> CarFeeScaleList { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public List<CarFeeModel> CarFeeList { get; set; }


    }
    /// <summary>
    /// 时间段设置  0-30 分钟 2 元
    /// </summary>
    public class CarTimeQuantumModel
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public double beginTime { get; set; }
        /// <summary>
        /// 
        /// 结束时间
        /// </summary>
        public double endTime { get; set; }

        /// <summary>
        /// 收费方式
        /// </summary>
        public string RateMethod { get; set; }

        /// <summary>
        /// 收费金额
        /// </summary>
        public string TimeQuantumMoney { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CarFeeScaleModel
    {
        /// <summary>
        /// 收费时间
        /// </summary>
        public string RateTime { get; set; }

        /// <summary>
        /// 收费金额
        /// </summary>
        public decimal RateMoney { get; set; }

        /// <summary>
        /// 收费方式 	0  分钟  1 小时  2	车次
        /// </summary>
        public string RateMethod { get; set; }

    }
    /// <summary>
    /// 收费时间段
    /// </summary>
    public class TimeSettingModel
    {
        /// <summary>
        /// 车辆类型  	0 所有   1	 大型车   2 小型车  3 	摩托车
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        ///收费方式（0 按时  1 按次）
        /// </summary>
        public string RateMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string beginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 停车多天计费方式 0  按停车总和  1  分天计算
        /// </summary>
        public string ManyDayMethod { get; set; }

    }

    /// <summary>
    /// 具体费率Model
    /// </summary>
    public class CarFeeModel
    {
        /// <summary>
        /// 收费时间
        /// </summary>
        public string RateTime { get; set; }
        /// <summary>
        /// 收费金额
        /// </summary>
        public string RateMoney { get; set; }

        /// <summary>
        /// 收费方式 	0  分钟  1 小时  2	车次
        /// </summary>
        public string RateMethod { get; set; }

        /// <summary>
        /// 0 前 1 每  ps：前10分钟0元 每30分钟2元
        /// </summary>
        public string TimeType { get; set; }

    }
    /// <summary>
    /// 停车时间类
    /// </summary>
    public class ParkTime
    {
        /// <summary>
        /// 当天停车开始时间
        /// </summary>
        public DateTime beginTime { get; set; }

        /// <summary>
        /// 当天停车结束时间
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// 当天开始时间段
        /// </summary>
        public double quantumBegin { get; set; }

        /// <summary>
        /// 当天结束时间段
        /// </summary>
        public double quantumEnd { get; set; }

        /// <summary>
        ///当天停车总时长
        /// </summary>
        public double timeTotal { get; set; }

        /// <summary>
        /// 停车费用
        /// </summary>
        public decimal parkMoney { get; set; }

        /// <summary>
        /// 当天已收金额
        /// </summary>
        public decimal alreadyCharge { get; set; }
    }

}
