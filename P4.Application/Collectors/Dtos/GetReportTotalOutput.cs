using System;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetReportTotalOutput
    {
        /// <summary>
        /// 停车时长
        /// </summary>
      public virtual Int32 StopTime { get; set; }

        /// <summary>
        /// 预付费
        /// </summary>
      public virtual decimal Prepaid { get; set; }
        /// <summary>
        /// 刷卡实收总计
        /// </summary>
        public virtual decimal ByCard { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal Receivable { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal FactReceive { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public virtual decimal Arrearage { get; set; }
        

        /// <summary>
        /// 补缴金额
        /// </summary>
        public virtual decimal Repayment { get; set; }

        /// <summary>
        /// 车检器停车时长
        /// </summary>
        public virtual Int32 SensorsStopTime { get; set; }

        /// <summary>
        /// 车检器应收
        /// </summary>
        public virtual decimal SensorsReceivable { get; set; }
        /// <summary>
        /// 预缴总额
        /// </summary>
        public virtual decimal PrepaidMoney { get; set; }

        /// <summary>
        ///  停车次数总计
        /// </summary>
        public virtual Int32 StopTimes { get; set; }
       
        /// <summary>
        ///  逃逸停车次数总计
        /// </summary>
        public virtual Int32 EscapeStopTimes { get; set; }
       
        /// <summary>
        ///  现金支付总计
        /// </summary>
        public virtual decimal Cash { get; set; }
        /// <summary>
        /// 时间段
        /// </summary>
        public virtual string Time { get; set; }
       
      
       
    }
}
