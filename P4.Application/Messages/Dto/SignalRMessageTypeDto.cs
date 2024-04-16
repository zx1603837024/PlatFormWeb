using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.Messages.Dto
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(SignalRMessageType))]
    public class SignalRMessageTypeDto : EntityDto<int>
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
