using Abp.Web.Mvc.Authorization;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ParkingInformationController : P4ControllerBase
    {
        #region Var
        private readonly IBerthsecAppService _berthsecAppService;
        #endregion

        public ParkingInformationController(IBerthsecAppService berthsecAppService)
        {
            _berthsecAppService = berthsecAppService;
        }

        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("ParkingInformation")]
        public ActionResult ParkingInformation()
        {
            return View();
        }

        /// <summary>
        /// 获取List
        /// </summary>
        /// <param name="input"></param>
        /// <param name="searchField"></param>
        /// <param name="searchOper"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public JsonResult GetBerthsecList(SearchBerthsecInput input, string searchField, string searchOper,
           string searchString)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("BerthsecName", "BerthsecName.temp");
            return this.Json(_berthsecAppService.GetBerthsecList(input));
        }
    }
}