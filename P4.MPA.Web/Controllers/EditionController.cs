using Abp.Application.Navigation;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Editions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using P4.Permissions.Dto;
using P4.Editions.Dtos;
using Abp.Web.Models;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 版本管理
    /// </summary>
    [AbpMvcAuthorize]
    public class EditionController : P4ControllerBase
    {

        #region Var
        private readonly IEditionAppService _editionAppService;
        private readonly IUserNavigationManager _userNavigationManager;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editionAppService"></param>
        /// <param name="userNavigationManager"></param>
        public EditionController(IEditionAppService editionAppService, IUserNavigationManager userNavigationManager)
        {
            _editionAppService = editionAppService;
            _userNavigationManager = userNavigationManager;
        }

        /// <summary>
        /// 版本管理页面
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EditionManager")]
        public ActionResult EditionManager()
        {
            return View();
        }

        /// <summary>
        /// 获取系统权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SystemPermission(int id)
        {
            return _editionAppService.SystemPermission(id);
        }


        /// <summary>
        /// 获取系统权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetMenuTree(int id)
        {
            var tree= _editionAppService.GetMenuTreeForEle(id);
            return Json(tree);
        }

        //public string ProcessMenu()

        /// <summary>
        /// 版本列表
        /// </summary>
        /// <returns></returns>

        public JsonResult GetEditionList(SearchEditionInput input)
        {
            return this.Json(_editionAppService.GetAll(input));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="savetype"></param>
        /// <param name="chooseeditionid"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EditionManager.Field1")]
        public JsonResult SaveEditionFeature(string savetype, int chooseeditionid, string nodes)
        {
          
            bool flag = _editionAppService.SaveEditionFeature(savetype, chooseeditionid, nodes);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="savetype"></param>
        /// <param name="chooseeditionid"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EditionManager.Field1")]
        public JsonResult SaveMenu(SaveMenuInput input)
        {
            try
            {
                _editionAppService.SaveMenu(input);
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                Success = true,
                Message = ""
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessEdition(CreateOrUpdateEditionInput input)
        {
            switch (input.oper)
            {
                case "add":
                    return InsertEdition(input);
                case "del":
                    return DeleteEdition(input);
                case "edit":
                    return UpdateEdition(input);
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse(new ErrorInfo("提交异常，请重试！")));
        }

        [AbpMvcAuthorize("EditionManager.Insert")]
        public JsonResult InsertEdition(CreateOrUpdateEditionInput input)
        {
            _editionAppService.Add(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize("EditionManager.Update")]
        public JsonResult UpdateEdition(CreateOrUpdateEditionInput input)
        {
            _editionAppService.Update(input);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize("EditionManager.Delete")]
        public JsonResult DeleteEdition(CreateOrUpdateEditionInput input)
        {
            _editionAppService.Delete(input.Id);
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        public string GetAllEdition()
        {
            StringBuilder strtemp = new StringBuilder("<select>");

            strtemp.AppendLine(emptyoption);
            foreach (var model in _editionAppService.GetAll())
            {
                strtemp.AppendFormat(option, model.Id, model.DisplayName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
    }
}