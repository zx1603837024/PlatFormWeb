using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.WeixinSendMsgModel.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(P4.Weixin.WeixinSendMsgModel))]
    public class CreateOrUpdateWeixinSendMsgModelInput : EntityRequestInput, IOperDto
    {

        /// <summary>
        /// 标签，使用;隔开
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }
    }
}
