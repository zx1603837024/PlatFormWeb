using Abp.Application.Services.Dto;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMonthTotalOutput : IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int monthday { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal Receivable { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal FactReceive { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public virtual decimal Arrearage { get; set; }
    }
}
