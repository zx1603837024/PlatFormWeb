using Abp.Domain.Entities.Auditing;
using P4.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Weixinplate")]
    public class Weixinplates : FullAuditedEntity<int, User>
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(6)]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int orders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ValueCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }

    }
}
