namespace P4.InspectorCharges.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectorUserData
    {
        /// <summary>
        /// 收费操作员
        /// </summary>
        public string ChargeOperaName
        {
            get;
            set;
        }
        /// <summary>
        /// 实收总和
        /// </summary>
        public decimal SumFactReceive { get; set; }

        /// <summary>
        /// 现金总和
        /// </summary>
        public decimal? XJSumFactReceive { get; set; }

        /// <summary>
        /// 刷卡总和
        /// </summary>
        public decimal? SKSumFactReceive { get; set; }

        /// <summary>
        /// 欠费总和
        /// </summary>
        public decimal SumArrearage { get; set; }
        /// <summary>
        /// 应收总和
        /// </summary>
        public decimal? SumMoney { get; set; }
        /// <summary>
        /// 总现金补缴
        /// </summary>
        public decimal? XJSumRepayment { get; set; }
        /// <summary>
        /// 总刷卡补缴
        /// </summary>
        public decimal? SKSumRepayment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SumIncomePlusBack
        {
            get
            {
                decimal? SumIPB = SumFactReceive + XJSumRepayment + SKSumRepayment;
                return SumIPB;
            }
        }

    }
}
