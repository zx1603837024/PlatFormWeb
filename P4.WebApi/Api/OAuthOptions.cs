
using System;
using Abp.Dependency;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using P4.WebApi.Api.Providers;

/// <summary>
/// The WebApi namespace.
/// </summary>
namespace P4.WebApi.Api.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class OAuthOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        private static OAuthAuthorizationServerOptions _serverOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static OAuthAuthorizationServerOptions CreateServerOptions()
        {
            if (_serverOptions == null)
            {
                var provider = IocManager.Instance.Resolve<SimpleAuthorizationServerProvider>();
                var refreshTokenProvider = IocManager.Instance.Resolve<SimpleRefreshTokenProvider>();                
                _serverOptions = new OAuthAuthorizationServerOptions
                {
                    TokenEndpointPath = new PathString("/oauth/token"),
                    Provider = provider,                    
                    RefreshTokenProvider = refreshTokenProvider,
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(7),
                    AllowInsecureHttp = true                    
                };
            }
            return _serverOptions;
        }
    }
}
