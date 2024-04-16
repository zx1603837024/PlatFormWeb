using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars.Dtos
{
    public class CreateOrUpdateMonthlyCarAbnormalInput : EntityRequestInput<long>, IOperDto, IInputDto
    {
        public string oper { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }
    }
}
