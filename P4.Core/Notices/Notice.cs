using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Notices
{
    [Table("AbpNotices")]
    public class Notice : Entity<int>, IPassivable, IMustHaveTenant, ICreationAudited<Users.User>
    {
        /// <summary>
        /// 通知信息
        /// </summary>
        [MaxLength(2000)]
        public virtual string NoticeInfo { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        [MaxLength(100)]
        public virtual string NoticeUrl { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 发送用户实体
        /// </summary>
        public virtual Users.User CreatorUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// 接收用户Id
        /// </summary>
        public virtual long ReviceUserId { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public virtual bool IsRead { get; set; }

        /// <summary>
        /// 读取时间
        /// </summary>
        public virtual DateTime? ReadTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
