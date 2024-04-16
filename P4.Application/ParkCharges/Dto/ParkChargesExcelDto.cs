﻿namespace P4.ParkCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkChargesExcelDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumFactReceive { get; set; }

        /// <summary>
        /// 现金收入
        /// </summary>
        public decimal? XJSumFactReceive { get; set; }

        /// <summary>
        /// 刷卡收入
        /// </summary>
        public decimal? SKSumFactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumArrearage { get; set; }

        /// <summary>
        /// 欠费补缴金额
        /// </summary>
        public decimal SumRepayment { get; set; }

        /// <summary>
        /// 车检器应收
        /// </summary>
        public decimal? SensorSumReceivable { get; set; }


        /// <summary>
        /// 在线支付
        /// </summary>
        public decimal? OnlineSumFactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yield { get; set; }

        
    }
}
