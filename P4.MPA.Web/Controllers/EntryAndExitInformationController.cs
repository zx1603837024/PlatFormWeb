using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using P4.Businesses;
using P4.Signo;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class EntryAndExitInformationController : P4ControllerBase
    {
        #region Var
        private readonly IBusinessAppService _businessAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly IRepository<SignoParkInformation, long> _abpSignoParkInformationRepository;
        #endregion

        public EntryAndExitInformationController(IBusinessAppService businessAppService,IRepository<SignoParkInformation, long> abpSignoParkInformationRepository, IBerthsecAppService berthsecAppService)
        {
            _businessAppService = businessAppService;
            _abpSignoParkInformationRepository = abpSignoParkInformationRepository;
            _berthsecAppService = berthsecAppService;
        }

        /// <summary>
        /// 出入口信息
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EntryAndExitInformation")]
        public ActionResult EntryAndExitInformation()
        {
            return View();
        }

        /// <summary>
        /// 出入口信息List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetParkDetail(SearchBerthsecInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("BerthsecName", "BerthsecName.temp");
            return Json(_berthsecAppService.GetSignoParkList(input));
        }

        /// <summary>
        /// 获取照片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetBusinessDetailsPictureList1(int Id)
        {
            var list = _abpSignoParkInformationRepository.GetAllList(entity =>entity.BerthsecId == Id);
            return Json(_businessAppService.GetSignoPictureList(list), JsonRequestBehavior.AllowGet);
        }


    }
}