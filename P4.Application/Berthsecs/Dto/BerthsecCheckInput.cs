using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berthsecs.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class BerthsecCheckInput : IInputDto
    {

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 收费员Id
        /// </summary>
        public long EmployeeId { get; set; }
    }
}
