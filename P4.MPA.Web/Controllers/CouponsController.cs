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

    public class CouponsController : P4ControllerBase
    {
        [AbpMvcAuthorize("CouponManager")]
        public ActionResult CouponsPlan()
        {
            return View();
        }
        [AbpMvcAuthorize("CouponDetail")]
        public ActionResult CouponsDetails()
        {
            return View();
        }

        [AbpMvcAuthorize("CustomerOpinion")]
        public ActionResult CustomerOpinion()
        {
            return View();
        }



        /// <summary>
        /// 查询优惠劵计划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult GetCouponsPlanList(CouponsQueryList query)
        {
            CouponsService vs = new CouponsServiceImpl();
            var res = vs.getCouponsPlanList(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加优惠劵计划
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult InsertCouponsPlan(InsertPlanQuery query)
        {
            CouponsService vs = new CouponsServiceImpl();
            var res = vs.insertCouponsPlan(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 优惠券方案编辑删除
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult CouponsEditDelete(InsertPlanQuery query)
        {
            CouponsService vsc = new CouponsServiceImpl();
            Hashtable res = new Hashtable();
            switch (query.oper)
            {
                case "del": res=vsc.couponsDelete(query);break;
                case "edit": res=vsc.couponsEdit(query);break;
            }

            
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取优惠券发放列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult GetCouponsDetail(CouponsDetailQuery query)
        {
            CouponsService vs = new CouponsServiceImpl();
            var res = vs.getCouponsDetail(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 优惠券发放
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult GrantCoupons(GrantCouponsQuery query)
        {
            CouponsService vs = new CouponsServiceImpl();
            var res = vs.grantCoupons(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询意见反馈接口
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult GetOpinionList(OpinionListQuery query)
        {
            OpinionService vs = new OpinionServiceImpl();
            var res = vs.getOpinionList(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 意见处理
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult OpinionHandle(OpinionHandleQuery query)
        {
            OpinionService vs = new OpinionServiceImpl();
            var res = vs.opinionHandle(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
