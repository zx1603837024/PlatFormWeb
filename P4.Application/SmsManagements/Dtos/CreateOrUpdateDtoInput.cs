using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Smss;
using System;

namespace P4.SmsManagements.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(SmsModel))]
    public class CreateOrUpdateDtoInput : EntityDto, IInputDto, IOperDto
    {
        /// <summary>
        /// 短信类型
        /// </summary>
        public virtual string ModelType { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public virtual string SmsContext { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 发送短信路径
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }
    }
}
