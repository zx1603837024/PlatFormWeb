using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.VideoEquipBusinessDetails;
using P4.VideoEquipBusinessDetails.Dtos;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class VideoRoadRollerController : P4ControllerBase
    {
        #region Var
        private readonly IVideoEquipBusinessDetailAppService _videoEquipBusinessDetailApplicationService;
        #endregion

        public VideoRoadRollerController(IVideoEquipBusinessDetailAppService videoEquipBusinessDetailApplicationService)
        {
            _videoEquipBusinessDetailApplicationService = videoEquipBusinessDetailApplicationService;
        }

        // GET: VideoRoadRoller
        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize("VideoRRManagementStopData")]
        public ActionResult VideoEquipBusinessDetails()
        {
            return View();
        }

        public JsonResult GetVideoEquipBusinessDetail(VideoEquipBusinessDetailInput input)
        {
            return this.Json(_videoEquipBusinessDetailApplicationService.GetAllVideoRoadRollBusinessDetaillist(input));
        }

        public JsonResult ProcessVideoEquipBusinessDetail(CreateOrUpdateVideoEqBusDetail input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridUpdate:
                    List<string> Ids = new List<string>(input.id.Split(','));
                    if (Ids.Count > 1)
                    {
                        info.Message = "请选择一条数据";
                        return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
                    }
                    bool isFlag = false;
                    string carnumRegex = @"^([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[a-zA-Z](([DF]((?![IO])[a-zA-Z0-9](?![IO]))[0-9]{4})|([0-9]{5}[DF]))|[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1})$";
                    isFlag = Regex.IsMatch(input.PlateNumber, carnumRegex);
                    if (isFlag)
                    {
                        _videoEquipBusinessDetailApplicationService.Update(input);
                    }
                    else
                    {
                        info.Message = "输入车牌号信息错误";
                        return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
                    }
                    break;
                case P4Consts.JqGridDelete:
                    VqBusinessDetailDelete(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("VideoRRManagementStopData.Delete")]
        public void VqBusinessDetailDelete(CreateOrUpdateVideoEqBusDetail input)
        {

            List<string> Ids = new List<string>(input.id.Split(','));
            foreach (var item in Ids)
            {
                int temp = Convert.ToInt32(item);
                _videoEquipBusinessDetailApplicationService.Delete2(temp);
            }
        }
    }
}