using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 对账model
    /// </summary>
    public class GetReconciliation: IOutputDto
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        public bool IsNormal { get; set; }
        /// <summary>
        /// 是否对账
        /// </summary>
        public bool IsRepeal { get; set; }
        /// <summary>
        /// 追缴次数
        /// </summary>
        public int RecoverCount { get; set; }
        /// <summary>
        /// 进场总数
        /// </summary>
        public int CarInCount { get; set; }
        /// <summary>
        /// 出场总数
        /// </summary>
        public int CarOutCount { get; set; }
        /// <summary>
        /// 预付费总额
        /// </summary>
        public decimal PrepaySum { get; set; }
        /// <summary>
        /// 出场收费总额
        /// </summary>
        public decimal OutPaySum { get; set; }
        /// <summary>
        /// 部分追缴所收金额
        /// </summary>
        //public decimal PartPaySum { get; set; }

        /// <summary>
        /// 进出场应收总额
        /// </summary>
        public decimal ShouldPaySum { get; set; }
        /// <summary>
        /// 进出场实收总额
        /// </summary>
        public decimal RealPaySum { get; set; }
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
        /// 出场现金总额
        /// </summary>
        public decimal CashPaySum { get; set; }
        /// <summary>
        /// 在线支付
        /// </summary>
        public decimal OnlinePaySum { get; set; }
        /// <summary>
        /// 现金追缴
        /// </summary>
        public decimal CashRecPaySum { get; set; }
        /// <summary>
        /// 在线追缴总额
        /// </summary>
        public decimal OnlineRecPaySum { get; set; }
        /// <summary>
        /// 账号追缴总额
        /// </summary>
      //  public decimal CardRecPaySum { get; set; }
    }
}
