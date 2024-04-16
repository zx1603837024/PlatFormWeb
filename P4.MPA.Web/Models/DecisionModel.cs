using P4.DecisionAnalysis.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DecisionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PeakPeriodDto> PeakPeriodList { get; set; }

        /// <summary>
        /// 包月车辆总计
        /// </summary>
        public int AllMonthyCount { get; set; }

        /// <summary>
        /// 包月车辆总金额
        /// </summary>
        public string AllMonthyMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NewMonthyCount { get; set; }

        /// <summary>
        /// 新增车辆总金额
        /// </summary>
        public string NewMonthyMoney { get; set; }

        /// <summary>
        /// 微信注册用户
        /// </summary>
        public int WeixinRegisterCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int WeixinNewRegisterCount { get; set; }

        /// <summary>
        /// 微信投诉记录
        /// </summary>
        public int WeixinIdeaCount { get; set; }

        /// <summary>
        /// 微信回复记录
        /// </summary>
        public int WeixinRelyIdeaCount { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public string AllMoney { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public string AllFactReceive { get; set; }

        /// <summary>
        /// 逃逸金额
        /// </summary>
        public string AllArrearage { get; set; }

        /// <summary>
        /// 追缴金额
        /// </summary>
        public string AllRepayment { get; set; }

        /// <summary>
        /// 总停车时长
        /// </summary>
        public string AllStopTime { get; set; }

        /// <summary>
        /// 总停车次数
        /// </summary>
        public string AllStopTimes { get; set; }
    }
}