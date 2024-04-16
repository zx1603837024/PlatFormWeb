using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{
    [AutoMapTo(typeof(Equipment))]
    public class CreateOrUpdateEquipmentInput : EntityDto, IInputDto, IOperDto
    {
        public string oper { get; set; }

        public string CompanyName { get; set; }

        public string PDA { get; set; }

        public Int16 Type { get; set; }

        public string Sim { get; set; }

        public bool SD { get; set; }

        public string Pasm { get; set; }

        public bool Printers { get; set; }

        public bool IsActive { get; set; }

        public Int16 Standard { get; set; }

        public bool GPS { get; set; }

        public bool Scan { get; set; }

        public Int16 UseStatus { get; set; }

        public string EmployeeName { get; set; }

        public virtual DateTime? GetTime { get; set; }

        public string Remark { get; set; }

        public bool IsUpgrade { get; set; }
    }
}
