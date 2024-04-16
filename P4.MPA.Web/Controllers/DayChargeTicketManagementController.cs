using Abp.AutoMapper;
using Abp.Web.Mvc.Authorization;
using P4.TicketManagement;
using P4.TicketManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class DayChargeTicketManagementController : Controller
    {
        // GET: DayChargeTicketManagement
        private readonly ITickeAppService _abpTicketStyleRepository;

        public DayChargeTicketManagementController(ITickeAppService abpTicketStyleRepository)
        {
            _abpTicketStyleRepository = abpTicketStyleRepository;
        }

        [AbpMvcAuthorize("DayChargeTicketManagement")]
        public ActionResult DayChargeTicketManagement()
        {
            var model = _abpTicketStyleRepository.GetDayChargeTicketStyle();
            return View(model);
        }

        public JsonResult AddOrUpdateTicket(TicketStyleDto ticketStyle)
        {
            TicketStyleDto model = new TicketStyleDto();
            if (ticketStyle.Id == 0)
            {
                model = _abpTicketStyleRepository.InsertTicketStyle(ticketStyle).MapTo<TicketStyleDto>();
            }
            else
            {
                model = _abpTicketStyleRepository.UpdateTicketStyle(ticketStyle).MapTo<TicketStyleDto>();
            }
            return Json(model);
        }
    }
}