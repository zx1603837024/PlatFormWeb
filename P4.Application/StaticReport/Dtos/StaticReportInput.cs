using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
    public class StaticReportInput : IInputDto, IPagedResultRequest, IFilters
    {
        public int page { get; set; }

        public int rows { get; set; }

        public string filters { get; set; }

        public string operateDateBegin{  get; set;  }

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
                return  string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");

            }
            
        }
        /// <summary>
        /// 当天时间的最后时间
        /// </summary>
        public DateTime beginEnddt 
        {
            get {
                return string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 23:59:59");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string parkIdInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string parkNameInput { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        public string BerthsecIdInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string employeeIdInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyIdInput { get; set; }
    }
}
