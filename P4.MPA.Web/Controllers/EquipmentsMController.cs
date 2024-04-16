using Abp.Configuration;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Dictionarys;
using P4.Dictionarys.Dtos;
using P4.EquipmentDetailManagement;
using P4.EquipmentDetailManagement.Dtos;
using P4.EquipmentMaintain;
using P4.EquipmentMaintain.Dtos;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class EquipmentsMController : P4ControllerBase
    {
        private readonly IEquipmentsMAppService _equipmentsMAppService;
        private readonly IEquipmentDetailAppService _equipmentDetailAppService;
        private readonly IDictionarysAppService _dictionarysApplicationService;
        //
        // GET: /EquipmentsM/

        public EquipmentsMController(IEquipmentsMAppService equipmentsMAppService, IEquipmentDetailAppService iEquipmentDetailAppService, IDictionarysAppService dictionarysApplicationService)
        {
            _equipmentsMAppService = equipmentsMAppService;
            _equipmentDetailAppService = iEquipmentDetailAppService;
            _dictionarysApplicationService = dictionarysApplicationService;
        }

        [AbpMvcAuthorize("EquipmentsM")]
        public ActionResult EquipmentsM()
        {
            return View();
        }

        /// <summary>
        /// 下拉框初始化
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EquipmentsEdit")]
        public ActionResult EquipmentsEdit()
        {
            EquipmentsEditModel model = new EquipmentsEditModel();
            model.EqVersion = _dictionarysApplicationService.GetAllValueData("A10017");
            List<GetAllValueDataDto> dislist = _dictionarysApplicationService.GetAllValueData(P4Consts.PartsType);
            model.PartsType = "{";
            if (dislist != null && dislist.Count > 0)
            {
                foreach (var item in dislist)
                {
                    if (item.TypeCode == P4Consts.PartsType)
                        model.PartsType += item.ValueData + ":" + item.ValueCode.ToString() + ",";
                }
                model.PartsType = model.PartsType.Length == 1 ? "{" : model.PartsType.Substring(0, model.PartsType.Length - 1);
            }
            model.PartsType += "}";
            return View(model);

            //model.PartsId = _dictionarysApplicationService.GetAllValueData("A10018");
            //return View(model);
        }

        [AbpMvcAuthorize("EquipmentsM")]
        public JsonResult GetEquipmentsMList(SearchEquipmentsMInput input)
        {
            return this.Json(_equipmentsMAppService.GetAllEquipmentsM(input));
        }

        //public GetAllEquipmentsMOutput GetEquipmentsMList1(SearchEquipmentsMInput input)
        //{
        //    return _equipmentsMAppService.GetAllEquipmentsM(input);
        //}
        

        public JsonResult ProcessEquipmentsM(CreateOrUpdateEquipmentsMInput input)
        {
            switch (input.oper)
            {
                //case "add":
                //    Insert(input);
                //    break;
                case "del":
                    Delete(input);
                    break;
                case "edit":
                    Update(input);
                    break;
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEquipmentDetailList(SearchEquipmentDetailInput input)
        {
            return this.Json(_equipmentDetailAppService.GetAllEquipmentDetail(input));
        }

        public JsonResult ProcessEquipmentDetail(CreateOrUpdateEquipmentDetailInput input)
        {
            switch (input.oper)
            {
                case "add":
                    Insert(input);
                    break;
                case "del":
                    Delete(input);
                    break;
                case "edit":
                    Update(input);
                    break;
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize("EquipmentsM.Insert")]
        public void Insert(CreateOrUpdateEquipmentsMInput input)
        {
            _equipmentsMAppService.Insert(input);
        }
        /// <summary>
        /// 添加设备详情
        /// </summary>
        /// <param name="input"></param>
        public void Insert(CreateOrUpdateEquipmentDetailInput input)
        {
                _equipmentDetailAppService.Insert(input);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("EquipmentsM.Delete")]
        public void Delete(CreateOrUpdateEquipmentsMInput input)
        {
            _equipmentsMAppService.Delete(input);
        }
        /// <summary>
        /// 删除设备详情
        /// </summary>
        /// <param name="input"></param>
        public void Delete(CreateOrUpdateEquipmentDetailInput input)
        {
            _equipmentDetailAppService.Delete(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("EquipmentsM.Update")]
        public void Update(CreateOrUpdateEquipmentsMInput input)
        {
            _equipmentsMAppService.Update(input);
        }
        /// <summary>
        /// 修改设备详情
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("EquipmentsM.Update")]
        public void Update(CreateOrUpdateEquipmentDetailInput input)
        {
            if (_equipmentDetailAppService.Update(input) == 0)
            {
                _equipmentDetailAppService.Update(input);
            }
            
        }


        /// <summary>
        /// 设备详情关联记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetEquipmentDetailCorrelation(int Id)
        {
            EquipmentsMDto equipmentsMDto = _equipmentsMAppService.GetEquipmentDetailInfo(Id);
            return Json(_equipmentDetailAppService.GetAllEquipmentDetail(new SearchEquipmentDetailInput() { page = 1, rows = 1000, EqId = equipmentsMDto.EqId }), JsonRequestBehavior.AllowGet);
        }

        public string GetEqld(string eqld)
        {
            return _equipmentsMAppService.GetEqld(eqld);
        }


        /// <summary>
        /// 保存设备配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EquipmentsEdit")]
        public async Task<JsonResult> SaveEquipmentsEdit(CreateOrUpdateEquipmentsMInput entity,  List<CreateOrUpdateEquipmentDetailInput> PartsList)
        {
            //entity = new CreateOrUpdateEquipmentsMInput();
            //detailentity = new CreateOrUpdateEquipmentDetailInput();
            string a = Request.Form["EqId"];
            string b = Request.Form["EqName"];
            string c = Request.Form["EqVersion"];
            string d = Request.Form["BatchNum"];
            string e = Request.Form["EqFactory"];
            //string f = Request.Form["PartsType"];
            //string g = Request.Form["PartsId"];
            
            
            //查询泛型
            //var temp = _equipmentsMAppService.GetAllEquipmentsM(new SearchEquipmentsMInput() { page = 1, rows = 1000 }).rows;

            //detailentity.EqId = a;
            //detailentity.PartsType = f;
            //detailentity.PartsId = g;

            if (GetEqld(a) == "true")
            {
                _equipmentsMAppService.Insert(entity);
                for (int i = 0; i < PartsList.Count; i++)
                {
                    if (_equipmentDetailAppService.Insert(PartsList[i]) == 0)
                    {
                        _equipmentDetailAppService.Insert(PartsList[i]);
                    }
                }
                return Json(new MvcAjaxResponse(true));
            }
            //_equipmentDetailAppService.Insert(detailentity);
            return this.Json(new MvcAjaxResponse(new ErrorInfo("该设备已存在！")));
        }


        #region 下拉框
        /// <summary>
        /// 获取设备型号(jqgrid中用)
        /// </summary>
        /// <returns></returns>
        public string GetEqVersionType()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10017"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
        /// <summary>
        /// 获取配件类型(jqgrid中用)
        /// </summary>
        /// <returns></returns>
        public string GetPartsType()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10021"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
        #endregion 
    }
}