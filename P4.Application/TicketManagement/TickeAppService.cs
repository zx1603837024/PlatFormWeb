using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
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
    /// 小票
    /// </summary>
    public class TickeAppService : ApplicationService, ITickeAppService
    {
        private readonly IRepository<TicketStyle> _abpTicketStyleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpTicketStyleRepository"></param>
        public TickeAppService(IRepository<TicketStyle> abpTicketStyleRepository)
        {
            _abpTicketStyleRepository = abpTicketStyleRepository;
        }

        /// <summary>
        /// 获取入场小票样式
        /// </summary>
        /// <returns></returns>
        public TicketStyleDto GetCarInTicketStyle()
        {
          return _abpTicketStyleRepository.GetAll().Where(e => e.Status == "CarIn").FirstOrDefault().MapTo<TicketStyleDto>();
        }

        /// <summary>
        /// 获取出场小票样式
        /// </summary>
        /// <returns></returns>
        public TicketStyleDto GetCarOutTicketStyle()
        {
            return _abpTicketStyleRepository.GetAll().Where(e => e.Status == "CarOut").FirstOrDefault().MapTo<TicketStyleDto>();
        }

        /// <summary>
        /// 获取欠费小票样式
        /// </summary>
        /// <returns></returns>
        public TicketStyleDto GetOweTicketStyle()
        {
            return _abpTicketStyleRepository.GetAll().Where(e => e.Status == "OweDetail").FirstOrDefault().MapTo<TicketStyleDto>();
        }

        /// <summary>
        /// 获取追缴小票样式
        /// </summary>
        /// <returns></returns>
        public TicketStyleDto GetRepayTicketStyle()
        {
            return _abpTicketStyleRepository.GetAll().Where(e => e.Status == "Repay").FirstOrDefault().MapTo<TicketStyleDto>();
        }

        /// <summary>
        /// 获取收费日报
        /// </summary>
        /// <returns></returns>
        public TicketStyleDto GetDayChargeTicketStyle()
        {
            return _abpTicketStyleRepository.GetAll().Where(e => e.Status == "DayCharge").FirstOrDefault().MapTo<TicketStyleDto>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ticketStyle"></param>
        /// <returns></returns>
        public TicketStyle InsertTicketStyle(TicketStyleDto ticketStyle)
        {
            TicketStyle model = new TicketStyle
            {
                Status = ticketStyle.Status,
                Text = ticketStyle.Text,
                TwoBarCode = ticketStyle.TwoBarCode
            };
            return _abpTicketStyleRepository.Insert(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ticketStyle"></param>
        /// <returns></returns>
        public TicketStyle UpdateTicketStyle(TicketStyleDto ticketStyle)
        {
            TicketStyle model = _abpTicketStyleRepository.Load(ticketStyle.Id);
            model.Text = ticketStyle.Text;
            model.TwoBarCode = ticketStyle.TwoBarCode;
            return _abpTicketStyleRepository.Update(model);
        }
    }
}
