using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MongoCore
{
    /// <summary>
    /// 
    /// </summary>
    public class MBusinessDetailPicture : CreationAuditedEntity
    {
        /// <summary>
        /// 收费记录id
        /// </summary>
        public virtual long BusinessDetailId { get; set; }

        /// <summary>
        /// 关联收费记录guid
        /// </summary>
        public virtual Guid BusinessDetailGuid { get; set; }

        /// <summary>
        /// 图片顺序
        /// </summary>
        public virtual UInt16 Order { get; set; }
    }
}
