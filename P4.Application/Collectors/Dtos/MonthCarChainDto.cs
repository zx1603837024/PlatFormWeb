using Abp.Application.Services.Dto;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class MonthCarChainDto : IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int? MonthCarNowCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MonthCarHistoryCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CountPercentUp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CountPercentDown { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MonthCarNowMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MonthCarHistoryMoney { get; set; }


        public int? MoneyPercentUp { get; set; }

        public int? MoneyPercentDown { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public decimal MonthCarTotalMoney { get; set; }
    }
}
