using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SubscribeManager
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubscribeAppService : IApplicationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="message"></param>
        /// <param name="msgType"></param>
        void SendMessage(string group, string message, string msgType);
    }
}
