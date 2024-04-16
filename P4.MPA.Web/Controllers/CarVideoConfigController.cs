using Abp.Web.Mvc.Authorization;
using F2.SystemWork.Daos;
using F2.SystemWork.Models;
using F2.SystemWork.Services;
using Newtonsoft.Json;
using P4.Businesses.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    public class CarVideoConfigController : P4ControllerBase
    {
        // GET: CarVideoConfig
        [AbpMvcAuthorize("CarVideoSetting")]
        public ActionResult Index()
        {
            return View();
        }
        [AbpMvcAuthorize("CarVideoEdit")]
        public ActionResult CarVideoEdit()
        {
            return View();
        }
        [AbpMvcAuthorize("CarVideoGraph")]
        public ActionResult CarVideoGraph()
        {
            return View();
        }
        public ActionResult SelectVideoCarDistinct(CarVideoParamModel input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.SelectVideoCarDistinct(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectVideoCars(CarVideoParamModel input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.SelectVideoCars(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InsertVideoCarBatch(String input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.InsertVideoCarBatch(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //巡检车绑定
        public ActionResult BandVideoCar(String input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.BandVideoCar(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //巡检车解绑
        public ActionResult UnbandVideoCars(String input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.DeleteVideoCars(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //巡检车编辑、删除
        public ActionResult EditVideoCars(CarVideoParamModel input)
        {
            CarVideoService vs = new CarVideoService();
            Hashtable res = new Hashtable();
            switch (input.oper)
            {
                case "del":
                    res = vs.DeleteVideoCarsByNumber(input);
                    break;
                case "add":
                    break;
                case "edit":
                    res = vs.UpdateVideoCars(input);
                    break;
                default:
                    break;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectVideoCarHeartBeatList(CarVideoParamModel input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.SelectVideoCarHeartBeatList(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //配置参数查询
        public ActionResult SelectAbpParamConfig()
        {
            Hashtable param = new Hashtable();
            CarVideoService vs = new CarVideoService();
            var res = vs.SelectAbpParamConfig(param);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //配置参数插入
        public ActionResult InsertAbpParamConfig(String input)
        {
            CarVideoService vs = new CarVideoService();
            var param = JsonConvert.DeserializeObject<Hashtable>(input);
            var res = vs.InsertAbpParamConfig(param);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //配置参数更新
        public ActionResult UpdateAbpParamConfig(String input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.UpdateAbpParamConfig(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //控制台图表信息展示上
        public ActionResult GetEqDetalControlInfo(String input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.GetEqDetalControlInfo(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //控制台图表信息展示下
        public ActionResult GetEqDetalChart()
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.GetEqDetalChart();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //用户初始化
        public ActionResult UserInitialization(String input)
        {
            CarVideoService vs = new CarVideoService();
            var res = vs.UserInitialization(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}