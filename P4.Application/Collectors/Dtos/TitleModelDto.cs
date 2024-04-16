using Abp.Application.Services.Dto;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class TitleModelDto : IOutputDto
    {
        /// <summary>
        /// 当日金额
        /// </summary>
        public decimal TodayMoney { get; set; }

        /// <summary>
        /// 当月金额
        /// </summary>
        public decimal MonthMoney { get; set; }

        /// <summary>
        /// 当日订单量
        /// </summary>
        public int TodayOrderSize { get; set; }

        /// <summary>
        /// 当月订单量
        /// </summary>
        public int MonthOrderSize { get; set; }
    }
}
