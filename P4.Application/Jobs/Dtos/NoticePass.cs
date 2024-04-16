using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Jobs.Dtos
{
    class NoticePass : FullAuditedEntity<int, Users.User>, IMustHaveTenant, IMustHaveCompany, IMustVersion
    {
        public virtual string PlateNumber { get; set; }

        public virtual decimal Arrearage { get; set; }

        public virtual string IsDeleted { get; set; }

        public virtual string Status { get; set; }

    }
}
