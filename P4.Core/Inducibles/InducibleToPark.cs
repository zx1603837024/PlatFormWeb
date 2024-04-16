using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inducibles
{
    [Table("AbpInducibleToParks")]
    public class InducibleToPark : Entity, IPassivable
    {
        public virtual int InducibleId { get; set; }

        public virtual int ParkId { get; set; }

        [ForeignKey("ParkId")]
        public virtual Parks.Park Park { get; set; }

        public virtual bool IsActive { get; set; }
        
    }
}
