using Abp.Authorization.AppAccounts;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.OtherPlateNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts
{
    /// <summary>
    /// 第三方账号
    /// </summary>
    public class OtherAccount : AbpOtherAccount, IMayHaveCompany
    {
        /// <summary>
        /// 是否激活
        /// true：激活
        /// false：未激活
        /// </summary>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// 激活时间
        /// </summary>
        public virtual DateTime? EnabledTime { get; set; }

        /// <summary>
        /// 激活用户id
        /// </summary>
        public virtual long? EnabledUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("EnabledUserId")]
        public virtual Employees.Employee Employee { get; set; }   //为什么会对应到收费员表的trueName
        //[ForeignKey("AssignedOtherAccountId")]
        //public virtual OtherPlateNumber OtherplateNumber { get; set; }

        /// <summary>
        /// 分公司id，可空
        /// </summary>
        public virtual int? CompanyId { get; set; }

        /// <summary>
        /// 上次发送短信的时间
        /// </summary>
        public DateTime? SendSmsDatetime { get; set; }

        /// <summary>
        /// 已发送短信的次数
        /// </summary>
        public int SendSmsNumber { get; set; }

        /// <summary>
        /// 自动扣款
        /// </summary>
        public bool AutoDeduction { get; set; }
    }
}
