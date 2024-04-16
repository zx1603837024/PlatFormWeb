using Abp.Application.Services;
using P4.WeixinPushMsg.Dtos;

namespace P4.WeixinPushMsg
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWeixinHistoryMsgAppService : IApplicationService
    {
        /// <summary>
        /// 微信历史消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinPushMsgOutput GetAllWeixinPushMsgList(SearchWeixinPushMsgInput input);
    }
}
