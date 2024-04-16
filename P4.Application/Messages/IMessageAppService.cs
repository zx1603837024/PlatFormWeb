using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.Messages.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Messages
{

    /// <summary>
    /// 
    /// </summary>
    public interface IMessageAppService : IApplicationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ListResultOutput<MessageDto> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetAllCount();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllMessageOutput GetAll(MessageInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        bool ReadMessage(List<int> Ids);


       /// <summary>
       /// 
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        GetAllSignalRMessageTypeOutput GetSignalRMessageTypeList(SignalRMessageTypeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        void SaveUserSubscribeMessageSetting(string val);
    }
}
