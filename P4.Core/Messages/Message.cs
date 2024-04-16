using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Messages
{
    [Table("AbpMessages")]
    public class Message : Entity<int>, IPassivable, IMustHaveTenant, ICreationAudited<Users.User>
    {
        [MaxLength(500)]
        public virtual string Msg { get; set; }
        //public string message

        public virtual Users.User CreatorUser
        {
            get;
            set;
        }

        public virtual long? CreatorUserId
        {
            get;
            set;
        }

        public virtual DateTime CreationTime
        {
            get;
            set;
        }

        public virtual bool IsActive
        {
            get;
            set;
        }

        public virtual int TenantId
        {
            get;
            set;
        }

        public virtual long ReviceUserId
        { get; set; }

        /// <summary>
        /// 读取时间
        /// </summary>
        public virtual DateTime? ReadTime { get; set; }

        /// <summary>
        /// 是否已读： true已读 ，false未读
        /// </summary>
        public virtual bool IsRead { get; set; }

    }
}
