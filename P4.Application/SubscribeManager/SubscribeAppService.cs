using Abp.Application.Services;
using Abp.Authorization.SignalRMessage;
using Abp.Domain.Repositories;
using Abp.SignalR.SignalrService;
using System;
using System.Collections.Generic;

namespace P4.SubscribeManager
{
    /// <summary>
    /// 
    /// </summary>
    public class SubscribeAppService : P4AppServiceBase, ISubscribeAppService
    {
        #region Var
        private readonly IRepository<UserSubscribeMessageSetting, long> _abpUserSubscribeMessageSetting;
        private readonly IRepository<RoleSubscribeMessageSetting, long> _abpRoleSubscribeMessageSetting;
        private readonly IP4ChatService _p4ChatService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpUserSubscribeMessageSetting"></param>
        /// <param name="abpRoleSubscribeMessageSetting"></param>
        /// <param name="p4ChatService"></param>
        public SubscribeAppService(IRepository<UserSubscribeMessageSetting, long> abpUserSubscribeMessageSetting, IRepository<RoleSubscribeMessageSetting, long> abpRoleSubscribeMessageSetting, IP4ChatService p4ChatService)
        {
            _abpUserSubscribeMessageSetting = abpUserSubscribeMessageSetting;
            _abpRoleSubscribeMessageSetting = abpRoleSubscribeMessageSetting;
            _p4ChatService = p4ChatService;
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="group"></param>
        /// <param name="message"></param>
        /// <param name="msgType"></param>
        public void SendMessage(string group, string message, string msgType)
        {

            var list = _abpUserSubscribeMessageSetting.GetAllList(entity => entity.TypeGroup == group && entity.IsGranted);
            List<string> userlist = new List<string>();
            foreach (var entry in list)
            {
                if (entry.UserId == AbpSession.UserId.Value)
                    continue;
                Abp.SignalR.Chat.ChatUser user = _p4ChatService.GetUser(entry.UserId.ToString());
                if (user != null)
                    userlist.Add(user.ConnectionId);
            }
            if (userlist.Count > 0)
                _p4ChatService.CreateChatService().Clients.Clients(userlist).sendPrivateMessage(AbpSession.Name + "在:" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分") + message, 5);
        } 
    }
}
