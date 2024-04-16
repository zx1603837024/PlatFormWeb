using Abp.Application.Services.Dto;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMoneyOutput : IOutputDto
    {
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
        /// 
        /// </summary>
        public decimal MoneyTotal { get; set; }
    }
}
