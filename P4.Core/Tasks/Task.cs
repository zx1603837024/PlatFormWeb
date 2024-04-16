using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tasks
{
    [Table("AbpTasks")]
    public class Task : Entity<int>, IPassivable, IMustHaveTenant, ICreationAudited<Users.User>
    {
        [MaxLength(500)]
        public virtual string TaskInfo { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public virtual Users.User CreatorUser
        { get; set; }


        public virtual long ReadUserId { get; set; }

        public virtual DateTime? ReadTime { get; set; }

        public virtual bool IsRead { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>
        public virtual long? CreatorUserId
        { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime
        { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public virtual int TenantId
        { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public virtual bool IsActive
        { get; set; }
    }
}
