using Abp.Application.Services;
using P4.WebChat.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WebChat
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebchatAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetAdsOutput GetAdsList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        GetStopCarOutput GetStopCarList(int page, int size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        StopCarInfoDto GetStopCarInfo(Int64 Id);

    }
}
