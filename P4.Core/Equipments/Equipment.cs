using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments
{
    /// <summary>
    /// 设备管理
    /// </summary>
    [Table("AbpEquipments")]
    public class Equipment : FullAuditedEntity<long, Users.User>, IMustHaveTenant, IMustHaveCompany, IPassivable
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string PDA { get; set; }


        /// <summary>
        /// 设备类型
        /// 1.收费机
        /// 2.巡查机
        /// </summary>
        public virtual Int16 Type { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public virtual int DeviceType { get; set; }

        /// <summary>
        /// sim卡
        /// </summary>
        [MaxLength(30)]
        public virtual string Sim { get; set; }

        /// <summary>
        /// 车牌识别卡,true为有，false为无
        /// </summary>
        public virtual bool SD { get; set; }

        /// <summary>
        /// Psam卡
        /// </summary>
        [MaxLength(30)]
        public virtual string Pasm { get; set; }

        /// <summary>
        /// 是否升级
        /// </summary>
        public virtual bool IsUpgrade { get; set; }

        /// <summary>
        /// Psam1卡
        /// </summary>
        [MaxLength(30)]
        public virtual string Pasm1 { get; set; }

        /// <summary>
        /// 是否有打印机
        /// true有，false无
        /// </summary>
        public virtual bool Printers { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [MaxLength(20)]
        public virtual string Version { get; set; }

        /// <summary>
        /// 升级时间
        /// </summary>
        public virtual DateTime? UpgradeTime { get; set; }

        /// <summary>
        /// 制式
        /// 1：WCDMA
        /// 2：EVDO
        /// 3：GPRS
        /// </summary>
        public virtual Int16 Standard { get; set; }

        /// <summary>
        /// GPS模块
        /// </summary>
        public virtual bool GPS { get; set; }

        /// <summary>
        /// 扫描头，一维码
        /// true有，false无
        /// </summary>
        public virtual bool Scan { get; set; }

        /// <summary>
        /// 使用状态
        /// 0.未使用
        /// 1.领用
        /// 2.返修
        /// </summary>
        public virtual Int16 UseStatus { get; set; }

        /// <summary>
        /// 领用时间
        /// </summary>
        public virtual DateTime? GetTime { get; set; }

        /// <summary>
        /// 领用用户
        /// </summary>
        public virtual long? EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 是否启用
        /// true，启用
        /// false，禁用
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
