using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.WorkGroupInspectors.Dto
{
    /// <summary>
    /// Echars数据数据实体DTO
    /// </summary>
    [AutoMapFrom(typeof(WorkGroupsInspectors))]
    public class WorkGroupInspectorsEcharsDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int WorkGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WorkGroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? WorkGroupPrepaid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? WorkGroupReceivable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? WorkGroupFactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? WorkGroupArrearage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? WorkGroupCash { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? WorkGroupByCard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumPrepaid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumReceivable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumFactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SumArrearage { get; set; }
    }
}
