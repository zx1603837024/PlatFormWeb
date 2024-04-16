using Abp.Web.Mvc.Authorization;
using F2.OptionSystem.Service.impl;
using F2.OptionSystem.Service;
using F2.SystemWork.Models;
using F2.SystemWork.Services;
using P4.Businesses.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    public class VideoConfigController : P4ControllerBase
    {
        // 高位摄像信息
        [AbpMvcAuthorize("VideoSetting")]
        public ActionResult VideoSetting()
        {
            return View();
        }
        // 视频桩信息
        [AbpMvcAuthorize("VideoSPSetting")]
        public ActionResult VideoSPSetting()
        {
            return View();
        }
        //路牙机信息
        [AbpMvcAuthorize("VideoLYSetting")]
        public ActionResult VideoLYSetting()
        {
            return View();
        }
        //高位摄像在线率
        [AbpMvcAuthorize("VideoGraph")]
        public ActionResult VideoGraph()
        {
            return View();
        }
        //视频桩在线率
        [AbpMvcAuthorize("VideoSPGraph")]
        public ActionResult VideoSPGraph()
        {
            return View();
        }
        //高位摄像绑定
        [AbpMvcAuthorize("VideoSettingEdit")]
        public ActionResult VideoSettingEdit()
        {
            return View();
        }
        //视频桩绑定
        [AbpMvcAuthorize("VideoSPSettingEdit")]
        public ActionResult VideoSPSettingEdit()
        {
            return View();
        }
        //路牙机绑定
        [AbpMvcAuthorize("VideoLYSettingEdit")]
        public ActionResult VideoLYSettingEdit()
        {
            return View();
        }
        //高位摄像待审核订单
        [AbpMvcAuthorize("VideoEqProcessData")]
        public ActionResult VideoEqProcessData()
        {
            return View();
        }
        //高位摄像无效订单
        [AbpMvcAuthorize("VideoEqInvalidData")]
        public ActionResult VideoEqInvalidData()
        {
            return View();
        }
        //高位摄像定时抓拍
        [AbpMvcAuthorize("GwTimingPic")]
        public ActionResult GwTimingPic()
        {
            return View();
        }
        //视频超时订单
        [AbpMvcAuthorize("OverTimeOrder")]
        public ActionResult OverTimeOrder()
        {
            return View();
        }
        //查询VideoEquips唯一设备列表
        public ActionResult SelectVideoEquipsDistinct(VideoGraphParamModel input)
        {
            VideoService vs = new VideoService();
            var res = vs.SelectVideoEquipsDistinct(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //查询VideoEquips列表
        public ActionResult SelectVideoEquips(VideoGraphParamModel input)
        {
           
            VideoService vs = new VideoService();
            var res = vs.SelectVideoEquips(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /**
         * 臻识使用查询设备接口
         * 传设备编号/查所有
         * 
         */
        public ActionResult GetVideoEquips(VideoQuery input)
        {
            VideoService vs = new VideoService();
            var res = vs.getVideoEquips(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getTUser(TUserQuery query)
        {
            Hashtable result = new Hashtable();

            query.start = (query.page - 1) * query.rows;
            TUserService vs = new TUserServiceImpl();
            var res = vs.getTUser(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /**
         * 臻识使用查询泊位接口
         * 传设备编号查询这个设备下的泊位
         * 传泊位编号查单个泊位
         *  
         */
        public ActionResult GetVideoBerth(VideoQuery input)
        {
            VideoService vs = new VideoService();
            var res = vs.getVideoBerth(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /** 
         * 查询设备抓拍接口
         */
        public ActionResult Capture(CaptureQuery query)
        {
            Hashtable result = new Hashtable();
            if (query.StartTime == null ||query.EndTime==null)
            {
                result.Add("msg", "开始时间及结束时间不可为空");
                result.Add("code", 1001);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            query.start = (query.Page - 1) * query.Size;
            VideoService vs = new VideoService();
            var res = vs.capture(query);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /**
         * 查询泊位段信息
         */
        public ActionResult GetVideoBerthsec()
        {
            VideoService vs = new VideoService();
            var res = vs.getVideoBerthsec();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //查询停车场信息
        public ActionResult SelectParkInfoList()
        {
            VideoService vs = new VideoService();
            var res = vs.SelectParkInfoList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //查询泊位段信息
        public ActionResult SelectBerthsecsInfoList(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.SelectBerthsecsInfoList(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //查询未绑定泊位信息
        public ActionResult SelectNoBerthsecsNumber(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.SelectNoBerthsecsNumber(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //视频设备解绑
        public ActionResult UpdateVideoEquipsNull(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.UpdateVideoEquipsNull(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //视频设备绑定
        public ActionResult UpdateVideoEquipsBand(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.UpdateVideoEquipsBand(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //视频设备编辑、删除
        public ActionResult EditVideoEquips(VideoSettingModel input)
        {
            VideoService vs = new VideoService();
            Hashtable res = new Hashtable() ;
            switch (input.oper)
            {
                case "del":
                    res = vs.DeleteVideoEquips(input);
                    break;
                case "add":
                    break;
                case "edit":
                    res = vs.UpdateVideoEquips(input);
                    break;
                default:
                    break;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //批量插入视频设备
        public ActionResult InsertVideoEquipsBatch(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.InsertVideoEquipsBatch(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //查询心跳列表
        public ActionResult SelectVideoHeartBeat(VideoGraphParamModel input)
        {
            VideoService vs = new VideoService();
            var res = vs.SelectVideoHeartBeat(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //首页统计图
        public ActionResult SelectVideoEqOnlineCount()
        {
            VideoService vs = new VideoService();
            var res = vs.SelectVideoEqOnlineCount();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //查询欠费列表
        public ActionResult SelectArrearageDetail(ArrearageParamModel input) {
            VideoService vs = new VideoService();
            var res = vs.SelectArrearageDetail(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult SelectEmployeesInfo(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.SelectEmployeesInfo(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateBusinessDetailArrearage(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.UpdateBusinessDetailArrearage(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //高位摄像视频绑定
        public ActionResult BandVideoEquipsGW(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.BandVideoEquipsGW(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //高位摄像设备编辑、删除
        public ActionResult EditGWVideoEquips(VideoGWModel input)
        {
            VideoService vs = new VideoService();
            Hashtable res = new Hashtable();
            switch (input.oper)
            {
                case "del":
                    res = vs.DeleteVideoEquipsByEquips(input);
                    break;
                case "add":
                    break;
                case "edit":
                    res = vs.UpdateVideoEquipsGW(input);
                    break;
                default:
                    break;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //审核状态修改
        public ActionResult ProcessAuditStatus(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.UpdateBusinessDetailAuditStatus(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //订单重新发送
        public ActionResult RePostProcess(String input)
        {
            VideoService vs = new VideoService();
            var res = vs.RePostProcess(input);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //华峰数据维护
        public ActionResult HFDataRedeem()
        {
            VideoService vs = new VideoService();
            var res = vs.HFDataRedeem();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}