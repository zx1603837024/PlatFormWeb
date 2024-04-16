using Abp.AutoMapper;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(BusinessDetail))]
    public class BusinessDetailUserData
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// 预付
        /// </summary>
        public decimal Prepaid { get; set; }
        /// <summary>
        /// 应收
        /// </summary>
        public decimal Receivable { get; set; }
        /// <summary>
        /// 实收
        /// </summary>
        public decimal FactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Arrearage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Money { get; set; }
    }
}
