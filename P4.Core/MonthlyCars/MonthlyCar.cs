using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars
{
    /// <summary>
    /// 包月卡
    /// </summary>
    [Table("AbpMonthlyCars")]
    public class MonthlyCar : FullAuditedEntity<int, Users.User>, IMustHaveTenant, IMustHaveCompany, IMustVersion
    {
        /// <summary>
        /// 车主
        /// </summary>
        [MaxLength(40)]
        public virtual string VehicleOwner { get; set; }

        /// <summary>
        /// 车主电话
        /// </summary>
        [MaxLength(20)]
        public virtual string Telphone { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public virtual string PlateNumber { get; set; }

        /// <summary>
        /// 续费总金额（包括开卡金额）
        /// </summary>
        public virtual decimal Money { get; set; }

        /// <summary>
        /// 包月卡生效的停车场
        /// </summary>
        [MaxLength(200)]
        public virtual string ParkIds { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public virtual DateTime BeginTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public virtual DateTime EndTime { get; set; }


        /// <summary>
        /// 商户Id
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 分公司Id
        /// </summary>
        public virtual int CompanyId { get; set; }
        /// <summary>
        /// 车辆类型
        /// 1.大型车2.中型车3.小型车4.摩托车
        /// </summary>
        public virtual int CarType { get; set; }

        /// <summary>
        /// 包月时间类型
        /// </summary>
        [MaxLength(100)]
        public virtual string MonthyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Version { get; set; }
        /// <summary>
        /// 是否发送自动短信提醒
        /// </summary>
        public virtual int IsSms { get; set; }
    }
}
