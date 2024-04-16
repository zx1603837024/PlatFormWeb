using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen.Dtos
{
    /// <summary>
    /// 大屏展示金额明细
    /// </summary>
    public class BigScreenMoneyDetail: IOutputDto
    {
        /// <summary>
        /// 预付费总额
        /// </summary>
        public decimal PrepaySum { get; set; }
        /// <summary>
        /// 出场收费总额
        /// </summary>
        public decimal OutPaySum { get; set; }
        /// <summary>
        /// 进出场欠费总额
        /// </summary>
        public decimal OwePaySum { get; set; }
        /// <summary>
        /// 追缴总额
        /// </summary>
        public decimal RecPaySum { get; set; }
        /// <summary>
        /// 预付现金总额
        /// </summary>
        public decimal CashPrePay { get; set; }
        /// <summary>
        /// 进出场应收总额
        /// </summary>
        public decimal ShouldPaySum { get; set; }

    }
}
