using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WorkGroups.Dto
{
    /// <summary>
    /// Echars数据数据实体DTO
    /// </summary>
    [AutoMapFrom(typeof(WorkGroup))]
    public class WorkGroupEcharsDto : EntityDto<int>
    {
        public int WorkGroupId { get; set; }
        public string WorkGroupName { get; set; }
        public decimal? WorkGroupPrepaid { get; set; }
        public decimal? WorkGroupReceivable { get; set; }
        public decimal? WorkGroupFactReceive { get; set; }
        public decimal? WorkGroupArrearage { get; set; }
        public int BerthsecId { get; set; }
        public decimal? WorkGroupCash { get; set; }
        public decimal? WorkGroupByCard { get; set; }



        public decimal SumPrepaid { get; set; }
        public decimal SumReceivable { get; set; }
        public decimal SumFactReceive { get; set; }
        public decimal SumArrearage { get; set; }
    }
}
