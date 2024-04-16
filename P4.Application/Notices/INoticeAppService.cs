using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.Notices.Dto;
using System.Collections.Generic;

namespace P4.Notices
{

    /// <summary>
    /// 
    /// </summary>
    public interface INoticeAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ListResultOutput<NoticeDto> GetAll();


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
        GetAllNoticeOutput GetAll(NoticeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        bool ReadNotice(List<int> Ids);
    }
}
