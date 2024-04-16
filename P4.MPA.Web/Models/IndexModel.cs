using P4.Collectors.Dtos;
using P4.DecisionAnalysis.Dtos;
using P4.Inspectors;
using P4.Inspectors.Dtos;
using P4.Weixin.Dtos;
using System.Collections.Generic;

namespace P4.Web.Models
{
    public class IndexModel
    {
        /// <summary>
        /// 
        /// </summary>
        public MonthCarChainDto MonthCarChain { get; set; }

        /// <summary>
        /// 当日金额
        /// </summary>
        public decimal TodayMoney { get; set; }

        /// <summary>
        /// 当月金额
        /// </summary>
        public decimal MonthMoney { get; set; }

        /// <summary>
        /// 当日订单量
        /// </summary>
        public int TodayOrderSize { get; set; }

        /// <summary>
        /// 当月订单量
        /// </summary>
        public int MonthOrderSize { get; set; }

        /// <summary>
        /// 总预付费
        /// </summary>
        public decimal PrepaidTotal { get; set; }

        /// <summary>
        /// 总应收
        /// </summary>
        public decimal ReceivableTotal { get; set; }

        /// <summary>
        /// 总欠费
        /// </summary>
        public decimal ArrearageTotal { get; set; }

        /// <summary>
        /// 总实收
        /// </summary>
        public decimal FactReceiveTotal { get; set; }

        /// <summary>
        /// 总补缴
        /// </summary>
        public decimal RepaymentTotal { get; set; }

        /// <summary>
        /// 员工签退签到表
        /// </summary>
        public GetCollectorCheckTotalOutput CheckTotal { get; set; }

        /// <summary>
        /// 获取指定条数巡查员
        /// </summary>
        public GetInspectorByTopOutput Inspectors { get; set; }

        /// <summary>
        /// 个性化设置
        /// </summary>
        public Layout.ProfileModel _ProfileModel { get; set; }

        /// <summary>
        /// 用户操作日志
        /// </summary>
        public DataLogs.Dtos.GetAllDataLogsOutput DataLogs { get; set; }

        /// <summary>
        /// 操作日志
        /// </summary>
        public OperationLog.Dtos.GetAllOperationLogOutput OperationLogs { get; set; }
        /// <summary>
        /// 在停车辆表
        /// </summary>
        public GetBerthCheckTotalOutput ParkCount { get; set; }

        /// <summary>
        /// 车检器在线表
        /// </summary>
        public GetSensorCheckTotalOutput SensorCount { get; set; }

        /// <summary>
        /// 道闸在线率
        /// </summary>
        public GetsignoOnlineOutput SignoCount { get; set; }
        /// <summary>
        /// 微信相关信息
        /// </summary>
        public GetWeixinOutput WeixinCount { get; set; }

        /// <summary>
        /// 泊位段收费统计
        /// </summary>
        public GetBerthsecReportTotalOutput BerthsecCount { get; set; }
        /// <summary>
        /// 收费员收费统计
        /// </summary>
        public List<GetEmployeeReportTotalOutput> EmployeeCountList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PeakPeriodDto> PeakPeriodList { get; set; }

        /// <summary>
        /// 停车场收费统计
        /// </summary>
        public List<GetParkTotalOutput> ParkTotalList { get; set; }
        /// <summary>
        /// 停车场收费统计Json字符串
        /// </summary>
        public string ParkTotalListJson { get; set; }
        /// <summary>
        /// 当月收费统计
        /// </summary>
        public List<GetMonthTotalOutput> MonthTotalList { get; set; }

        /// <summary>
        /// 当月收费统计Json字符串
        /// </summary>
        public string MonthTotalListJson { get; set; }
        /// <summary>
        /// 今年收费统计
        /// </summary>
        public GetYearTotalOutput YearTotal { get; set; }
        /// <summary>
        /// 巡查员任务列表
        /// </summary>
        public List<InspectorTask> inspectorTaskList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<WeixinIdeaDto> WeixinIdeas { get; set; }

        /// <summary>
        /// 达标金额
        /// </summary>
        public decimal TargetAmount { get; set; }

    }
}