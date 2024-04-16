using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.Sensors;

namespace P4.Inducibles.Dtos
{
     [AutoMapFrom(typeof(InducibleToPark))]
    public class InducibleToParkDto : EntityDto
    {
         public virtual int Id { get; set; }
        public virtual int InducibleId { get; set; }

         public virtual string EquipmentId { get; set; }

         public virtual int ParkId { get; set; }

       
        public virtual string ParkName { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual int emplyBerthsec { get; set; }

        public virtual int BerthsecCount { get; set; }

    }
}
