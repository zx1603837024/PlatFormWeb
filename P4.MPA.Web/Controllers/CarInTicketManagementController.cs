using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using P4.TicketCss;
using P4.TicketManagement;
using P4.TicketManagement.Dto;
using System;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class CarInTicketManagementController : P4ControllerBase
    {
        private readonly ITickeAppService _abpTicketStyleRepository;

        public CarInTicketManagementController(ITickeAppService abpTicketStyleRepository)
        {
            _abpTicketStyleRepository = abpTicketStyleRepository;
        }

        [AbpMvcAuthorize("CarInTicketManagement")]
        public ActionResult CarInTicketManagement()
        {
            var model = _abpTicketStyleRepository.GetCarInTicketStyle();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult CarInTicketManagement(TicketStyleDto ticketStyle)
        //{
        //    TicketStyleDto model = new TicketStyleDto();
        //    if (_abpTicketStyleRepository.GetCarInTicketStyle() == null)
        //    {
        //        model = _abpTicketStyleRepository.InsertTicketStyle(ticketStyle).MapTo<TicketStyleDto>();
        //    }
        //    else
        //    {
        //        model = _abpTicketStyleRepository.UpdateTicketStyle(ticketStyle).MapTo<TicketStyleDto>();
        //    }
        //    model.Message = "提交成功";
        //    return View(model);
        //}

        public JsonResult AddOrUpdateTicket(TicketStyleDto ticketStyle)
        {
            TicketStyleDto model = new TicketStyleDto();
            if (ticketStyle.Id == 0)
            {
                model= _abpTicketStyleRepository.InsertTicketStyle(ticketStyle).MapTo<TicketStyleDto>();
            }
            else
            {
                model = _abpTicketStyleRepository.UpdateTicketStyle(ticketStyle).MapTo<TicketStyleDto>();
            }
            return Json(model);       
        }
    }
}