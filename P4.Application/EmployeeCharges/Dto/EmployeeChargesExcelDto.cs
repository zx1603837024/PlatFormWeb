namespace P4.EmployeeCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeChargesExcelDto
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
        public decimal? SumMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumArrearage { get; set; }

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
        /// 微信收入
        /// </summary>
        public decimal? WxSumFactReceive { get; set; }

        /// <summary>
        /// 开户充值
        /// </summary>
        public decimal? CardMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CarInCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CarOutCount { get; set; }
        
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
        public decimal? WxSumRepayment { get; set; }

        /// <summary>
        /// 现金刷卡收入+现金刷卡补缴
        /// </summary>
        public decimal SumIncomePlusBack { get; set; }

        /// <summary>
        /// 补缴金额
        /// </summary>
        public decimal SumRepayment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yield { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AllYield { get; set; } 
    }
}
