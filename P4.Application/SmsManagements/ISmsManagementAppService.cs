using Abp.Application.Services;
using Abp.Dependency;
using P4.SmsManagements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SmsManagements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISmsManagementAppService : ISingletonDependency
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        void SendSms(SmsAccountDto entity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void SendSmsNoTenant(SmsAccountDto entity);

        /// <summary>
        /// 获取账号信息，未使用的代码 ByWRS
        /// </summary>
        /// <param name="input"></param>
        void GetSmsAccount(GetSmsAccountInput input);

        /// <summary>
        /// 获取所有发短信记录
        /// </summary>
        /// <returns></returns>
        GetAllSmsOutput GetSmsList(SearchSmssInput input);

        /// <summary>
        /// 发送短信；未使用的代码，ByWRS
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="msg"></param>
        void SendMsg(string phone, string msg);

        /// <summary>
        /// 自动发短信，三十分钟
        /// </summary>
        /// <param name="input"></param>
        /// <param name="TenantId"></param>
        void SendSmsNoTenantByTenant(SmsAccountDto input, int TenantId);
    }
}
