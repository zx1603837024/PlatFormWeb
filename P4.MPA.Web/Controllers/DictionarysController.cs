using Abp.Web.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P4.Dictionarys.Dtos;
using P4.Dictionarys;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Models;
using Abp.SignalR.SignalrService;
using Abp.SignalR;
using Abp.Web.Models;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 字典控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class DictionarysController : P4ControllerBase
    {
        #region Var
        private readonly IDictionarysAppService _dictionarysAppService;
        private readonly IP4ChatService _p4ChatService;
        private readonly IChat _chat;
        #endregion

        public DictionarysController(IDictionarysAppService dictionarysAppService, IP4ChatService p4ChatService, IChat chat)
        {
            _dictionarysAppService = dictionarysAppService;
            _p4ChatService = p4ChatService;
            _chat = chat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取字典数据
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetDictionaryListOnCombox(string Id)
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysAppService.GetAllValueData(Id))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
        

        //public static object GetValue<T>(T model, string field)
        //{
        //    if (field.Split('.').Length == 1)
        //        return model.GetType().GetProperty(field).GetValue(model, null);

        //    int index = field.IndexOf('.');
        //    string f1 = field.Substring(0, index);
        //    string f2 = field.Substring(index + 1);
        //    object obj = model.GetType().GetProperty(f1).GetValue(model, null);
        //    return GetValue(obj, f2);
        //}

        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("DictionaryTypeManagement")]
        public ActionResult DictionaryType()
        {
           
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("DictionaryValueManagement")]
        public ActionResult DictionaryValue()
        {
            return View();
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult GetAllDictionaryTypeList(DictionaryTypeInput entity)
        {
            return this.Json(_dictionarysAppService.GetAllDictionaryTypeList(entity));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        public JsonResult GetDictionartValueList(string TypeCode)
        {
            List<GetAllValueDataDto> list = new List<GetAllValueDataDto>();
            string[] sArray = TypeCode.Split(',');
            for (int i = 0; i < sArray.Length; i++)
            {
                list.AddRange(_dictionarysAppService.GetAllValueData(sArray[i]));
            }
            return this.Json(list);
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllDictionaryValueList(DictionaryValueInput input)
        {
            var temp = _dictionarysAppService.GetAllDictionaryValueList(input);
            return this.Json(temp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult GetAllDictionaryType(DictionaryTypeInput entity)
        {
            return this.Json(_dictionarysAppService.GetAllDictionaryType(entity));
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetAllTypeValue(DictionaryTypeInput entity)
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysAppService.GetAllDictionaryType
(entity))
            {
                strtemp.AppendFormat(option, model.TypeCode, model.TypeValue);
           
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
       
        /// <summary>
        /// 字典类型管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessDictionaryType(CreateOrUpdateDictionaryTypeInput input)
        {
            
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = TypeInsert(input);
                    break;
                case P4Consts.JqGridDelete:
                  info.Message= TypeDelete(input);
                    break;
                case P4Consts.JqGridUpdate:
                   info.Message= TypeEdit(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("DictionaryType.Insert")]
        public string TypeInsert(CreateOrUpdateDictionaryTypeInput input)
        {
           return _dictionarysAppService.DictionaryTypeInsert(input);
          
        }

        [AbpMvcAuthorize("DictionaryType.Delete")]
        public string TypeDelete(CreateOrUpdateDictionaryTypeInput input)
        {
        
           return _dictionarysAppService.DictionaryTypeDelete(input);
         
        }
        [AbpMvcAuthorize("DictionaryType.Update")]
        public string TypeEdit(CreateOrUpdateDictionaryTypeInput input)
        {
           return  _dictionarysAppService.DictionaryTypeUpdate(input);
      
      
        }
        public JsonResult ProcessDictionaryValue(CreateOrUpdateDictionaryValueInput input)
        {
            JsonResult returnJson = new JsonResult();
            switch (input.oper)
            {
                case "add":
                    returnJson = ValueInsert(input);
                    break;
                case "del":
                    returnJson = ValueDelete(input);
                    break;
                case "edit":
                    returnJson = ValueEdit(input);
                    break;
                default:
                    break;

            }
            return this.Json(new MvcAjaxResponse());
        }
        [AbpMvcAuthorize("DictionaryValue.Insert")]
        public JsonResult ValueInsert(CreateOrUpdateDictionaryValueInput input)
        {
            _dictionarysAppService.DictionaryValueInsert(input);
            return this.Json("12121");
        }

        [AbpMvcAuthorize("DictionaryValue.Delete")]
        public JsonResult ValueDelete(CreateOrUpdateDictionaryValueInput input)
        {
            _dictionarysAppService.DictionaryValueDelete(input);
            return this.Json("12121");
        }

        [AbpMvcAuthorize("DictionaryValue.Update")]
        public JsonResult ValueEdit(CreateOrUpdateDictionaryValueInput input)
        {
            _dictionarysAppService.DictionaryValueUpdate(input);
            return this.Json("12121");
        }

        /// <summary>
        /// 获取下拉值
        /// 是：true
        /// 否：false
        /// </summary>
        /// <returns></returns>
        public string GetYesOrNoSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "true", "是");
            strtemp.AppendFormat(option, "false", "否");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }


        public string GetPushTypeSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "CarInMsg", "进场消息");
            strtemp.AppendFormat(option, "CarOutMsg", "出场消息");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取上班种类：早班(1)，中班(2)，晚班(3)
        /// </summary>
        /// <returns></returns>
        public string GetWorkStatus()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, 1, "早班");
            strtemp.AppendFormat(option, 2, "中班");
            strtemp.AppendFormat(option, 3, "晚班");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetDictionaryValue(string TypeCode, string ValueCode)
        {
            return _dictionarysAppService.GetDictionaryValue(TypeCode, ValueCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        //public JsonResult GetDictionartValueList(string TypeCode)
        //{
        //    return this.Json(_dictionarysAppService.GetAllValueData(TypeCode));
        //}

        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public string GetCheckin01()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "true", "签到");
            strtemp.AppendFormat(option, "false", "未签到");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 签退
        /// </summary>
        /// <returns></returns>
        public string GetCheckout01()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "true", "签退");
            strtemp.AppendFormat(option, "false", "未签退");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 记录状态
        /// </summary>
        /// <returns></returns>
        public string GetStatusSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "3", "未收费");
            strtemp.AppendFormat(option, "4", "已收费");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public string GetCheckOrCheckOutSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "true", "签到");
            strtemp.AppendFormat(option, "false", "签退");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }


        /// <summary>
        /// A10007
        /// </summary>
        /// <returns></returns>
        public string GetOperationTypeList()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysAppService.GetAllValueData(P4Consts.OperationType))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 字段代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetDictionaryValueList(string code)
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysAppService.GetAllValueData(code))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetEquipmentVersionList()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysAppService.GetAllValueData(P4Consts.EquipmentVersion))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSmsModelTypeList()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysAppService.GetAllValueData(P4Consts.SmsModel))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetOperTypeNameList()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysAppService.GetAllValueData(P4Consts.OperTypeName))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public string GetDirectionSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, 1, "进");
            strtemp.AppendFormat(option, 2, "出");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
    }
}