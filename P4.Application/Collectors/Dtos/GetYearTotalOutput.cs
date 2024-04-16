using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    public class GetYearTotalOutput : IOutputDto
    {
        public int YearNum { get; set; }
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
        /// <summary>
        /// 补缴金额
        /// </summary>
        public virtual decimal Repayment { get; set; }
    }
}
