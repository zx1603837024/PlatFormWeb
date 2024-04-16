using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Messages
{
    /// <summary>
    /// 消息类型
    /// </summary>
    [Table("AbpSignalRMessageType")]
    public class SignalRMessageType : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public virtual string TypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string TypeGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(500)]
        public virtual string Remark { get; set; }
    }
}
