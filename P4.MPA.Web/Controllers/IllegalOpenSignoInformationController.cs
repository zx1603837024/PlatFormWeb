using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using P4.Berthsecs.Dto;
using P4.Businesses;
using P4.Signo;
using P4.Signo.Dtos;
using System;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class IllegalOpenSignoInformationController : P4ControllerBase
    {
        #region Var
        private readonly IBusinessAppService _businessAppService;
        private readonly IRepository<IllegalOpenSignoPicture> _illegalOpenSignoPictureRepository;
        private readonly ISignoParkInformationAppService _signoParkInformationAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessAppService"></param>
        /// <param name="signoParkInformationAppService"></param>
        /// <param name="illegalOpenSignoPictureRepository"></param>
        public IllegalOpenSignoInformationController(IBusinessAppService businessAppService, ISignoParkInformationAppService signoParkInformationAppService, IRepository<IllegalOpenSignoPicture> illegalOpenSignoPictureRepository)
        {
            _signoParkInformationAppService = signoParkInformationAppService;
            _businessAppService = businessAppService;
            _illegalOpenSignoPictureRepository = illegalOpenSignoPictureRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EntryAndExitInformation")]
        public ActionResult IllegalOpenSignoInformation()
        {
            return View();
        }

        /// <summary>
        /// 获取list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetIllegalOpenSignoList(SearchBerthsecInput input)
        {
            GetIllegalOpenOutput resultList = _signoParkInformationAppService.GetIllegalOpenOutputList(input);
            return Json(resultList);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetPicture(int id)
        {
            var guid = _illegalOpenSignoPictureRepository.FirstOrDefault(id).BusinessDetailGuid;
            return Json(_businessAppService.GetPictureList(guid), JsonRequestBehavior.AllowGet);
        }
    }
}