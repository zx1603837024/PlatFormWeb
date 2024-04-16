using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Abp.Dependency;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace P4.WebApi.Api.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider, ITransientDependency
    {
        /// <summary>
        /// 在线用户列表
        /// </summary>
        public static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        /// <summary>
        /// 更新token值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString("N");
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddYears(1)
            };
            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);
            _refreshTokens.TryAdd(guid, refreshTokenTicket);
            context.SetToken(guid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            AuthenticationTicket ticket;
            if (_refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}