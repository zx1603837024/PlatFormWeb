using Abp.Application.Services.Dto;
using P4.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    public class GetEmployeeReportTotalOutput : GetReportTotalOutput, IOutputDto
    {
       public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<EmployeeReport> employeeReportList { get; set; }
        ///// <summary>
        ///// 停车时长
        ///// </summary>
        //public virtual Int32? StopTime { get; set; }

        ///// <summary>
        ///// 预付费
        ///// </summary>
        //public virtual decimal? Prepaid { get; set; }

        ///// <summary>
        ///// 应收
        ///// </summary>
        //public virtual decimal? Receivable { get; set; }

        ///// <summary>
        ///// 实收
        ///// </summary>
        //public virtual decimal? FactReceive { get; set; }

        ///// <summary>
        ///// 欠费金额
        ///// </summary>
        //public virtual decimal? Arrearage { get; set; }

        ///// <summary>
        ///// 补缴金额
        ///// </summary>
        //public virtual decimal? Repayment { get; set; }

        ///// <summary>
        ///// 车检器停车时长
        ///// </summary>
        //public virtual Int32? SensorsStopTime { get; set; }

        ///// <summary>
        ///// 车检器应收
        ///// </summary>
        //public decimal? SensorsReceivable { get; set; }
    }
}
