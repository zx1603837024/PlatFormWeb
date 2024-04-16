using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 动态报表Input
    /// </summary>
    public class DynamicReportInput : IInputDto, IPagedResultRequest, IFilters
    {

        public int page { get; set; }

        public int rows { get; set; }

        public string filters { get; set; }

        public string operateDateBegin { get; set; }

        public string operateDateEnd { get; set; }

        public DateTime begindt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            }

        }
        public DateTime enddt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");

            }

        }
        //public string parkIdInput { get; set; }
        //public string parkNameInput { get; set; }
    }
}
