using Abp.Domain.Repositories;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.CompanyCustomerExpressManagement;
using P4.EquipmentDetailManagement;
using P4.EquipmentMaintain;
using P4.ReworkFlowManagement;
using P4.ReworkFlowManagement.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class ReworkFlowController : P4ControllerBase
    {
        private readonly IReworkFlowAppService _reworkFlowAppService;
        private readonly ICompanyCustomerExpressAppService _companyCustomerExpressAppService;
        private readonly IEquipmentsMAppService _equipmentsMAppService;
        private readonly IEquipmentDetailAppService _equipmentDetailAppService;
        private readonly string PictureStore = ConfigurationManager.ConnectionStrings["PictureStore"].ToString();
        private readonly IRepository<ReworkFlow> _reworkRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="reworkFlowAppService"></param>
        public ReworkFlowController(IReworkFlowAppService reworkFlowAppService, ICompanyCustomerExpressAppService companyCustomerExpressAppService, IEquipmentsMAppService equipmentsMAppService, IEquipmentDetailAppService equipmentDetailAppService, IRepository<ReworkFlow> reworkRepository)
        {
            _reworkFlowAppService = reworkFlowAppService;
            _companyCustomerExpressAppService = companyCustomerExpressAppService;
            _equipmentsMAppService = equipmentsMAppService;
            _equipmentDetailAppService = equipmentDetailAppService;
            _reworkRepository = reworkRepository;
        }
        //
        // GET: /ReworkFlow/
        [AbpMvcAuthorize("ReworkFlowEdit")]
        public ActionResult ReworkFlowEdit()
        {
            return View();
        }

        /// <summary>
        /// 添加返修流程/返修快递
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expressinput"></param>
        /// <returns></returns>
        public JsonResult Insert(CreateOrUpdateReworkFlowInput input, string base64String)
        {
            input.MaintainState ="1";
            if (_reworkFlowAppService.Insert(input) == 0)
            {
                List<ReworkFlow> list= _reworkRepository.GetAllList(dic => dic.EqId == input.EqId && dic.PartsId == input.PartsId);
                if (list != null)
                {
                    if (base64String != null)
                    {
                        byte[] imageBytes = Convert.FromBase64String(base64String.Replace("data:image/png;base64,", ""));
                        _reworkFlowAppService.InsertPicture(new ReworkPictureDto() { ReworkPictureId = list[0].Id, Image = imageBytes });
                    }
                }
                return Json(new MvcAjaxResponse(true));
            }
            if (_reworkFlowAppService.Insert(input) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该返修批次已发货，请重新填写返修批次号！")));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该设备编号不存在！")));
            }
        }
        /// <summary>
        /// 查询所有批次号(下拉用)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllBatchNum()
        {
            List<CompanyCustomerExpress> list = _companyCustomerExpressAppService.GetAllCompanyCustomerExpressListToSelect();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过批次号查询设备编号(下拉用)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetEqIdByBatchNum(int id)
        {
            List<EquipmentsM> list= _equipmentsMAppService.GetEqIdByBatchNum(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 通过设备编号查询配件编号(下拉用)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetPartsIdByEqId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                List<EquipmentDetail> list = _equipmentDetailAppService.GetPartsIdByEqId(id);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new MvcAjaxResponse(false));
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="reworkPictureId"></param>
        /// <returns></returns>
        //[AbpAuthorize]
        //[HttpPost]
        //public JsonResult PhotoUpLoadToReworkPicture(int reworkPictureId)
        //{
        //    try
        //    {
        //        if (_reworkFlowAppService.GetReworkPicture(reworkPictureId))
        //        {
        //            return Json("true");//记录已经存在，避免重复上传图片
        //        }

        //        Stream str = Request.Files["pic"].InputStream;
        //        byte[] bytes = new byte[str.Length];
        //        str.Read(bytes, 0, bytes.Length);
        //        str.Seek(0, SeekOrigin.Begin);
        //        ReworkFlow entity = new ReworkFlow();
        //        entity.Id = reworkPictureId;
        //        ReworkFlow insertResult = _reworkRepository.Insert(entity);

        //        if (PictureStore == "SqlServer")
        //        {
        //            _reworkFlowAppService.InsertPicture(new ReworkPictureDto() { ReworkPictureId=insertResult.Id, Image = bytes });
        //        }
        //        else
        //        {
        //            _reworkFlowAppService.CreatePicture(new ReworkPictureDto() { ReworkPictureId = insertResult.Id, Image = bytes });
        //        }

        //        return Json(new { ReturnStr = insertResult == null ? "false" : "true" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { ReturnStr = ex.Message });
        //    }
        //}

        //public JsonResult SaveReworkFlowPicture(string base64String)
        //{
        //    byte[] imageBytes = Convert.FromBase64String(base64String.Replace("data:image/png;base64,", ""));
        //    _reworkFlowAppService.InsertPicture(new ReworkPictureDto() { ReworkPictureId = 1, Image = imageBytes });

        //    return this.Json("");
        //}


        [AbpMvcAuthorize("ReworkFlowDetail")]
        public ActionResult ReworkFlowDetail()
        {
            return View();
        }

        [AbpMvcAuthorize("ReworkFlowDelivery")]
        public ActionResult ReworkFlowDelivery()
        {
            return View();
        }

        

        /// <summary>
        /// 查询返修流程表详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetReworkFlowList(SearchReworkFlowInput input)
        {
            return this.Json(_reworkFlowAppService.GetReworkFlowList(input));
        }

        /// <summary>
        /// 通过id查询EqId，PartsId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetReworkFlowById(int id)
        {
            ReworkFlowDto reworkflowdto = _reworkFlowAppService.GetReworkFlowById(id);
            return this.Json(reworkflowdto);
        }

        /// <summary>
        /// 添加维修描述
        /// </summary>
        /// <param name="input"></param>
        public JsonResult UpdateMaintainDescription(CreateOrUpdateReworkFlowInput input)
        {
            if (_reworkFlowAppService.UpdateMaintainDescription(input) == 0)
            {
                return Json(new MvcAjaxResponse(true));
            }
            else if (_reworkFlowAppService.UpdateMaintainDescription(input) == 2)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("设备已发回至公司，无法添加维修描述！")));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该设备还未收货，无法添加维修描述！")));
            }
        }

        /// <summary>
        /// 查询返修图片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetReworkFlowPictureList(int Id)
        {
            return this.Json(_reworkFlowAppService.GetPictureList(Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        public FileResult ShowImage(int id)
        {
            var model = _reworkFlowAppService.GetPicture(id);
            if (model != null)
            {
                return File(model.Image, "image/jpg");
            }
            else
            {
                return null;
            }  
        }
	}
}