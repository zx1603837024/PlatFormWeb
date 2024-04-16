using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Card
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpIPassBlackCard")]
    public class IPassBlackCard : Entity, IPassivable, ISoftDelete, IHasCreationTime
    {

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [MaxLength(20)]
        public string CardNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
