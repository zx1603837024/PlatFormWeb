using Abp.AutoMapper;
using Abp.Web.Mvc.Authorization;
using P4.TicketManagement;
using P4.TicketManagement.Dto;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class RepayPrintTicketManagementController : Controller
    {
        private readonly ITickeAppService _abpTicketStyleRepository;

        public RepayPrintTicketManagementController(ITickeAppService abpTicketStyleRepository)
        {
            _abpTicketStyleRepository = abpTicketStyleRepository;
        }

        [AbpMvcAuthorize("RepayPrintTicketManagement")]
        public ActionResult RepayPrintTicketManagement()
        {
            var model = _abpTicketStyleRepository.GetOweTicketStyle();
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