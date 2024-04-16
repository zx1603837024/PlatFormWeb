using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments
{
    /// <summary>
    /// 设备配置
    /// </summary>
    public class EquipmentConfig :  FullAuditedEntity<long, Users.User>, IMustHaveTenant, IPassivable
    {

        /// <summary>
        /// 商户id
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
