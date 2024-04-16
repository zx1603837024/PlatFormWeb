/********************************************************************************
** auth： 黎通
** date： 2017/07/26 10:00:50
** desc： 尚未编写描述
** Ver.:  V1.0.0
*********************************************************************************/

using Abp.Application.Services;
using P4.Card.Dtos;
using System;
using System.Web.Http;

namespace P4.Card
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIPassCardAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="Package"></param>
        /// <param name="Money"></param>
        /// <param name="PayDate"></param>
        /// <returns></returns>
        [HttpGet]
        bool InsertIPassCard(Guid guid, string Package, int Money, DateTime PayDate);



        /// <summary>
        /// 结算返回结果
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="Status"></param>
        /// <param name="ReturnResult"></param>
        /// <returns></returns>
        bool Settlement(Guid guid, int Status, string ReturnResult);

        /// <summary>
        /// 重新结算
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool FreshSettlement(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetAllIPassCardOutput GetList(GetAllIPassCardInput input);
    }
}
