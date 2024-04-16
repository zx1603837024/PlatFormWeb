using Abp.Application.Services;
using P4.WeixinSendMsgModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WeixinSendMsgModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWeixinSendMsgModelAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWeixinSendMsgModelOutput GetAll(SearchWeixinSendMsgModelInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Add(CreateOrUpdateWeixinSendMsgModelInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Update(CreateOrUpdateWeixinSendMsgModelInput input);
    }
}
