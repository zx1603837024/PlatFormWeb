using Abp.Application.Services;
using P4.AppMessages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.AppMessages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppMessageAppService : IApplicationService
    {
        /// <summary>
        /// GetAllMessages?p=android&v=2410&page=1&mobile=13851483025
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        List<AppMessageDto> GetAllMessages(string p, int v, int page, string mobile);
    }
}
