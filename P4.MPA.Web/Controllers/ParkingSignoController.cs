using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.AliPay;
using P4.Berths;
using P4.Berthsecs;
using P4.BlackLists;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.MonthlyCars;
using P4.Weixin;
using P4.WhiteLists;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 停车场道闸接口
    /// </summary>
    [AbpMvcAuthorize]
    public class ParkingSignoController : P4ControllerBase
    {
        #region Var
        private readonly IWhiteListsAppService _whiteListsAppService;
        private readonly IBlackListsAppService _blackListsAppService;
        private readonly IMonthlyCarAppService _monthlyCarAppService;
        //private readonly IRepository<Rate, int> _abpRateRepository;
        private readonly IRepository<IllegalOpenSignoPicture> _illegalOpenSignoPictureRepository;
        private readonly IRepository<BusinessDetailPicture> _businessDetailPictureRepository;
        private readonly IPictureStoreAppService _pictureStoreAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly string PictureStore = ConfigurationManager.ConnectionStrings["PictureStore"].ToString();
        private readonly IRepository<AliPayOrder> _abpAliPayOrderRepository;
        private readonly IRepository<WeixinOrder> _abpWeixinOrderRepository;
        //private readonly IBerthsecAppService _berthsecAppService;
        private readonly IRepository<Berth, long> _berthRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whiteListsAppService"></param>
        /// <param name="blackListsAppService"></param>
        /// <param name="monthlyCarAppService"></param>
        /// <param name="rateApplicationService"></param>
        /// <param name="illegalOpenSignoPictureRepository"></param>
        /// <param name="businessDetailPictureRepository"></param>
        /// <param name="pictureStoreAppService"></param>
        /// <param name="businessAppService"></param>
        /// <param name="abpAliPayOrderRepository"></param>
        /// <param name="abpWeixinOrderRepository"></param>
        public ParkingSignoController(IWhiteListsAppService whiteListsAppService,IBlackListsAppService blackListsAppService,IMonthlyCarAppService monthlyCarAppService, /*IRepository<Rate, int> abpRateRepository, */IRepository<IllegalOpenSignoPicture> illegalOpenSignoPictureRepository, IRepository<BusinessDetailPicture> businessDetailPictureRepository, IPictureStoreAppService pictureStoreAppService, IBusinessAppService businessAppService, IRepository<AliPayOrder> abpAliPayOrderRepository, IRepository<WeixinOrder> abpWeixinOrderRepository/*, IBerthsecAppService berthsecAppService*/, IRepository<Berth, long> berthRepository)
        {
            _whiteListsAppService = whiteListsAppService;
            _blackListsAppService = blackListsAppService;
            _monthlyCarAppService = monthlyCarAppService; 
            //_abpRateRepository = abpRateRepository;
            _illegalOpenSignoPictureRepository = illegalOpenSignoPictureRepository;
            _businessDetailPictureRepository = businessDetailPictureRepository;
            _pictureStoreAppService = pictureStoreAppService;
            _businessAppService = businessAppService;
            _abpAliPayOrderRepository = abpAliPayOrderRepository;
            _abpWeixinOrderRepository = abpWeixinOrderRepository;
            _berthRepository = berthRepository;
            //_berthsecAppService = berthsecAppService;
        }

        /// <summary>
        /// 获取白名单
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public JsonResult GetWhiteCarList()
        {
            var model = _whiteListsAppService.GetAllWhiteCar();
            return Json(new MvcAjaxResponse(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 黑名单获取
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public JsonResult GetBlackCarList()
        {
            var model = _blackListsAppService.GetAllBlackList();
            return Json(new MvcAjaxResponse(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取包月车辆列表
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public JsonResult GetMonthlyCarList()
        {
            var model = _monthlyCarAppService.GetAllMonthlyCar();
            return Json(new MvcAjaxResponse(model), JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        /////  费率列表
        ///// </summary>
        ///// <returns></returns>
        //[AbpAuthorize]
        //public JsonResult GeRateDtos()
        //{
        //    var berthescID = AbpSession.BerthsecIds;
         //   List<BerthsecDto> Berthseclist = _berthsecAppService.LoadToSql(berthescID);
        //    List<Rate> rateList = new List<Rate>();
        //    foreach (var item in Berthseclist)
        //    {
        //        Rate rate = _abpRateRepository.FirstOrDefault(dic => dic.Id == item.RateId);
        //        rateList.Add(rate);
        //        if(item.RateId1 != null)
        //        {
        //            Rate rate1 = _abpRateRepository.FirstOrDefault(dic => dic.Id == item.RateId1);
        //            rateList.Add(rate1);
        //        }
        //        if(item.RateId2 != null)
        //        {
        //            Rate rate2 = _abpRateRepository.FirstOrDefault(dic => dic.Id == item.RateId2);
        //            rateList.Add(rate2);
        //        }
        //    }
        //    return Json(new MvcAjaxResponse(rateList), JsonRequestBehavior.AllowGet);           
        //}


        /// <summary>
        /// 车辆进场记录上传接口
        /// </summary>
        /// <param name="PlateNumber"></param>
        /// <param name="CarType"></param>
        /// <param name="Prepaid"></param>
        /// <param name="CarInTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsInCarTime"></param>
        /// <param name="StopType"></param>
        /// <param name="CardNo"></param>
        /// <param name="RegionID"></param>
        /// <param name="ParkID"></param>
        /// <param name="BerthsecID"></param>
        /// <param name="BerthNumber"></param>
        /// <param name="InBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool CarInUpLoadData(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionID, int ParkID, int BerthsecID,string InBatchNo)
        {
            var berthList = _berthRepository.GetAllList(entity => entity.IsActive == true && entity.RegionId == RegionID && entity.ParkId == ParkID && entity.BerthsecId == BerthsecID && entity.BerthStatus == "2" && entity.ParkStatus == 0);
            int random = new Random().Next(berthList.Count);
            BerthNumber = berthList[random].BerthNumber;
            //如果传来的BerthNumber是正确格式则用下面的
            //if (string.IsNullOrEmpty(BerthNumber))
            //{
            //    BerthNumber = berthList[random].BerthNumber;
            //}
            return _businessAppService.InsertCarIn(BerthNumber, PlateNumber, CarType, Prepaid, CarInTime, guid, SensorsInCarTime, StopType, CardNo, RegionID, ParkID, BerthsecID, InBatchNo);
        }
        /// <summary>
        /// 车辆出场纪录上传接口
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <param name="CardNo"></param>
        /// <param name="BerthsecID"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool CarOutUpLoadData(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, int BerthsecID, string OutBatchNo)
        {
            return _businessAppService.InsertBatchCarOut(Receivable, FactReceive, CarOutTime, CarPayTime, guid, SensorsOutCarTime, SensorsStopTime, SensorsReceivable, PayStatus, IsPay, FeeType, StopTime, Money, CardNo, BerthsecID, OutBatchNo);
        }

        /// <summary>
        /// 非法开闸记录上传接口
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="signoDeviceNo"></param>
        /// <param name="photographTime"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [HttpPost]
        public JsonResult IllegalOpenSigno(Guid guid,string signoDeviceNo, DateTime photographTime)
        {
            try
            {
                if (_businessAppService.GetBusinessDetailPicture(guid, 1))
                {
                    return Json("true");//记录已经存在，避免重复上传图片
                }
                Stream str = Request.Files["pic"].InputStream;
                byte[] bytes = new byte[str.Length];
                str.Read(bytes, 0, bytes.Length);
                str.Seek(0, SeekOrigin.Begin);
                IllegalOpenSignoPicture entity = new IllegalOpenSignoPicture();
                entity.BusinessDetailGuid = guid;
                //entity.BusinessDetailPictureUrl = bytes;
                entity.BusinessDetailId = 1;
                entity.CreatorUserId = AbpSession.UserId;
                entity.CreationTime = photographTime;
                IllegalOpenSignoPicture insertResult = _illegalOpenSignoPictureRepository.Insert(entity);
                if (PictureStore == "SqlServer")
                {
                    _pictureStoreAppService.InsertPicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes });
                }
                else
                {
                    _businessAppService.CreatePicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes, CreationTime = DateTime.Now });
                }
                return Json(new { ReturnStr = insertResult == null ? "false" : "true" });
            }
            catch (Exception ex)
            {
                return Json(new { ReturnStr = ex.Message });
            }
        }

        /// <summary>
        /// 进出场图片上传接口
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="pictype"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [HttpPost]
        public JsonResult PhotoUpLoadToSigno(Guid guid, int pictype)
        {
            try
            {
                if (_businessAppService.GetBusinessDetailPicture(guid, 1))
                {
                    return Json("true");//记录已经存在，避免重复上传图片
                }

                Stream str = Request.Files["pic"].InputStream;
                byte[] bytes = new byte[str.Length];
                str.Read(bytes, 0, bytes.Length);
                str.Seek(0, SeekOrigin.Begin);
                BusinessDetailPicture entity = new BusinessDetailPicture();
                entity.BusinessDetailGuid = guid;
                entity.BusinessDetailId = 1;
                entity.CreatorUserId = AbpSession.UserId;
                entity.PicType = pictype;
                entity.CreationTime = DateTime.Now;
                BusinessDetailPicture insertResult = _businessDetailPictureRepository.Insert(entity);
                if (PictureStore == "SqlServer")
                {
                    _pictureStoreAppService.InsertPicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes });
                }
                else
                {
                    _businessAppService.CreatePicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes, CreationTime = DateTime.Now });
                }
                return Json(new { ReturnStr = insertResult == null ? "false" : "true" });
            }
            catch (Exception ex)
            {
                return Json(new { ReturnStr = ex.Message });
            }
        }

        /// <summary>
        /// 验证是否微信支付
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public int IsWeiXinPayOrders(Guid guid)
        {
            var busGuid = guid.ToString();
            var model = _abpWeixinOrderRepository.FirstOrDefault(entry => entry.attach.Contains(busGuid));
            if (model == null)
            {
                return 3;
            }
            else
            {
                var time = model.time_end;
                DateTime DateTime2 = DateTime.Now;//现在时间  
                DateTime DateTime1 = DateTime.ParseExact(time, "yyyyMMddHHmmss", null); //设置要求的减的时间  
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if (ts.Minutes < 15 && ts.Days == 0)
                {
                    return 1;
                }
                else 
                {
                    return 2;
                }
            }
        }

        /// <summary>
        /// 验证是否支付宝支付
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public int IsAliPayOrders(Guid guid)
        {
            var busGuid = guid.ToString();         
            var model = _abpAliPayOrderRepository.FirstOrDefault(entry => entry.guid == busGuid);
            if (model == null)
            {
                return 3;
            }
            else
            {
                var time = model.gmt_payment;
                DateTime DateTime2 = DateTime.Now;//现在时间  
                DateTime DateTime1 = Convert.ToDateTime(time); //设置要求的减的时间  
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if (ts.Minutes < 15 && ts.Days == 0)
                {
                    return 1;
                }
                else 
                {
                    return 2;
                }
            }    
        }

        /// <summary>
        /// 道闸心跳验证接口
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public string GetSignoBerthSyn()
        {
            return DateTime.Now.ToString("YYYY-MM-dd HH:mm:ss");
        }
    }
}