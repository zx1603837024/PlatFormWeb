using Abp.Authorization.Inspectors;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors
{
    /// <summary>
    /// 巡查员
    /// </summary>

    public class Inspector : AbpInspector
    {
        /// <summary>
        /// 后台签退用户
        /// </summary>
        [ForeignKey("CheckOutUserId")]
        public virtual Users.User User { get; set; }
    }
}
