using Abp.Application.Services;
using P4.Berths.Dtos;
using P4.Berthsecs.Dto;
using P4.BigScreen.Dtos;
using P4.Businesses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBigScreenAppService: IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        StatisticsDto GetStatisticsInfo();

        /// <summary>
        /// 大屏金额展示
        /// </summary>
        /// <returns></returns>
        StatisticsMoneyDto GetStatisticsMoneyInfo(int Status);

        /// <summary>
        /// 泊位段今日金额
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        BerthsecStatisticsDto GetBerthsecStatisticsInfo(int BerthsecId);

        /// <summary>
        /// 今日实收
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        EmployeeFactReceiveDto GetTodayFactReceiveList(int EmployeeId);
        /// <summary>
        /// 获取地磁信息
        /// </summary>
        /// <returns></returns>
        SensorsDetail GetSensorsDetail();

        /// <summary>
        /// 获取泊位使用情况
        /// </summary>
        /// <returns></returns>
        List<BerthsecsUtilization> GetBerthsecsUtilization();

        /// <summary>
        /// 获取环比金额
        /// </summary>
        /// <returns></returns>
        List<MoneyChain> GetMoneyChain();

        /// <summary>
        /// 获取泊位段坐标
        /// </summary>
        /// <returns></returns>
        List<Berthsecs.Berthsec> GetBerthsecsPoint();

        /// <summary>
        /// 获取人员坐标
        /// </summary>
        /// <returns></returns>
        List<EmpolyeePoint> GetEmpolyeePoint();
        /// <summary>
        /// 获取单人员坐标
        /// </summary>
        /// <returns></returns>
        EmpolyeePoint GetEmpolyeePointModel(int Employeeid);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<StopTimesRankDto> StopTimesRank();

        /// <summary>
        /// 微信注册用户概况
        /// </summary>
        /// <returns></returns>
        List<int> WeixinTuser();

        /// <summary>
        /// 包月车辆概况
        /// </summary>
        /// <returns></returns>
        List<int> MonthlyCar();
    }
}
