using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.WeixinSendMsgModel.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(P4.Weixin.WeixinSendMsgModel))]
    public  class WeixinSendMsgModelDto : EntityDto
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

    }
}
