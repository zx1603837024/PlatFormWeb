using Abp.Application.Services;
using P4.TicketCss;
using P4.TicketManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.TicketManagement
{
    /// <summary>
    /// 小票接口
    /// </summary>
    public interface ITickeAppService : IApplicationService
    {
        /// <summary>
        /// 获取入场小票样式
        /// </summary>
        /// <returns></returns>
        TicketStyleDto GetCarInTicketStyle();

        /// <summary>
        /// 获取出场小票样式
        /// </summary>
        /// <returns></returns>
        TicketStyleDto GetCarOutTicketStyle();

        /// <summary>
        /// 获取欠费小票样式
        /// </summary>
        /// <returns></returns>
        TicketStyleDto GetOweTicketStyle();

        /// <summary>
        /// 获取追缴小票样式
        /// </summary>
        /// <returns></returns>
        TicketStyleDto GetRepayTicketStyle();

        /// <summary>
        /// 获取收费日报小票样式
        /// </summary>
        /// <returns></returns>
        TicketStyleDto GetDayChargeTicketStyle();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ticketStyle"></param>
        /// <returns></returns>
        TicketStyle InsertTicketStyle(TicketStyleDto ticketStyle);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ticketStyle"></param>
        /// <returns></returns>
        TicketStyle UpdateTicketStyle(TicketStyleDto ticketStyle);
    }
}
