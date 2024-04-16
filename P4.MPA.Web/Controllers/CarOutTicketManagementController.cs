using Abp.AutoMapper;
using Abp.Web.Mvc.Authorization;
using P4.TicketManagement;
using P4.TicketManagement.Dto;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class CarOutTicketManagementController : P4ControllerBase
    {
        private readonly ITickeAppService _abpTicketStyleRepository;

        public CarOutTicketManagementController(ITickeAppService abpTicketStyleRepository)
        {
            _abpTicketStyleRepository = abpTicketStyleRepository;
        }

        [AbpMvcAuthorize("CarOutTicketManagement")]
        public ActionResult CarOutTicketManagement()
        {
            var model = _abpTicketStyleRepository.GetCarOutTicketStyle();
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
