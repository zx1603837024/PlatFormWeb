﻿namespace P4.EmployeeCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeUserData
    {

        /// <summary>
        /// 
        /// </summary>
        public int CarInCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CarOutCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChargeOperaName{get;set; }
        /// <summary>
        /// 实收总和
        /// </summary>
        public decimal SumFactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal WXSumRepayment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal WxSumFactReceive { get; set; }

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
                return SumFactReceive + XJSumRepayment;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SumRepayment
        {
            get
            {
                return XJSumRepayment + SKSumRepayment + WXSumRepayment;
            }
        }

        /// <summary>
        /// 开户充值总额
        /// </summary>
        public decimal? CardMoney { get; set; }

    }
}
