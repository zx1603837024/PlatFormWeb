using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SpecialLists
{

     [Table("AbpSpecialList")]
    public class SpecialList : FullAuditedEntity<int>, IMustHaveTenant, IMustHaveCompany, IPassivable
    {
        [MaxLength(50)]
        public virtual string VehicleType { get; set; }
        [MaxLength(50)]
        public virtual string PlateNumber { get; set; }

        
        [MaxLength(50)]
        public virtual string IdNumber { get; set; }
        [MaxLength(50)]
        public virtual string CarOwner { get; set; }
        [MaxLength(500)]
        public virtual string Remarks { get; set; }

        [MaxLength(10)]
        public virtual string Discount { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int TenantId { get; set; }

        public virtual int CompanyId { get; set; }
        //特殊类型  1 优惠时间段折扣车辆 2 错峰时期
        public virtual int? SpecilType { get; set; }
        //优惠开始时间
        public virtual string BeginDiscountTime { get; set; }
        //优惠结束时间
        public virtual string EndDiscountTime { get; set; }

        
        public virtual int ParkId { get; set; }
    }
}
