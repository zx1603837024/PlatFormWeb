using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using F2.SystemWork.Models;
using F2.SystemWork.Query;
using F2.SystemWork.Services;
using F2.SystemWork.Services.impl;
using P4.Berths;
using P4.Berths.Dtos;
using P4.Berthsecs;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Rates;
using P4.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    public class PatrolCarController : P4ControllerBase
    {
        [AbpMvcAuthorize("PatrolCarIndex")]
        public ActionResult Index()
        {
            return View();
        }
        [AbpMvcAuthorize("BindCar")]
        public ActionResult BindCar()
        {
            return View();
        }
        [AbpMvcAuthorize("AlarmData")]
        public ActionResult AlarmData()
        {
            return View();
        }
        [AbpMvcAuthorize("ValidOrder")]
        public ActionResult ValidOrder()
        {
            return View();
        }
        [AbpMvcAuthorize("InvalidOrder")]
        public ActionResult InvalidOrder()
        {
            return View();
        }
        [AbpMvcAuthorize("ExamineOrder")]
        public ActionResult ExamineOrder()
        {
            return View();
        }

        /// <summary>
        /// 批量插入巡检车信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult InsertPatrolCar(string input)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.InsertPatrolCar(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 巡查车设备查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult getPatrolList(PartolListQuery query)
        {
            query.start = (query.page - 1) * query.rows;
            PatrolCarService vs = new PatrolCarServiceImpl();
            CommonService cs = new CommonService();
            if(query.PatrolCarNumber!=null&&query.PatrolCarNumber.Length > 0)
            {
                query.PatrolCarNumber="'"+query.PatrolCarNumber+"'";
            }
            Hashtable param = cs.InputAnalysis2<PartolListQuery>(query);
            var res = vs.getPatrolCarList(param);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 巡检车设备绑定
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult BindingCar(BindingCarQuery query)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.bindingCar(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 编辑修改巡检车
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult PatrolEditDelete(PatrolCarModel input)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            Hashtable res = new Hashtable();
            switch (input.oper)
            {
                case "del":
                    res = vs.PatrolDelete(input);
                    break;
                case "edit":
                    res = vs.PatrolEdit(input);
                    break;
                default:
                    break;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询关联巡检车
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult GetPatrolBerthsList(PatrolBerthsQuery query)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.getPatrolBerthsList(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 解绑巡检车
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult DeletePatrolBerths(DeletePatrolBerhsQuery query)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.deletePatrolBerths(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 巡检车订单数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult GetPatrolCarBusinessDetail(PatrolCarModel query)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.GetPatrolCarBusinessDetail(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 巡检车订单审核
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult UPatrolCarBusinessDetailAuditStatus(string input)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.UPatrolCarBusinessDetailAuditStatus(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        

        /// <summary>
        /// 巡检车订单编辑
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult UPatrolCarBusinessDetail(PatrolCarModel input)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.UPatrolCarBusinessDetail(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
		/// <summary>
        /// 报警数据查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult GetAlarmDataList(AlarmDataQuery query)
        {
            PatrolCarService vs = new PatrolCarServiceImpl();
            var res = vs.getAlarmDataList(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
