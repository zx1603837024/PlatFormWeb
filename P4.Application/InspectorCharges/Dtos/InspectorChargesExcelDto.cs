namespace P4.InspectorCharges.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectorChargesExcelDto
    {
        /// <summary>
        /// 收费员工号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChargeOperaName { get; set; }

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
        /// 
        /// </summary>
        public decimal? XJSumRepayment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SKSumRepayment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SumMoney { get; set; }

        /// <summary>
        /// 现金刷卡收入+现金刷卡补缴
        /// </summary>
        public decimal SumIncomePlusBack { get; set; }

        /// <summary>
        /// 账号充值
        /// </summary>
        public decimal CardMoney { get; set; }
    }
}
