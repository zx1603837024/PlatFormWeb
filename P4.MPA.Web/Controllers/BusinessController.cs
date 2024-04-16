using Abp.Domain.Repositories;
using Abp.Helper;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Berths;
using P4.Berthsecs;
using P4.BlackLists;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Companys;
using P4.Dictionarys;
using P4.Employees;
using P4.Parks;
using P4.Rates;
using P4.Regions;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class BusinessController : P4ControllerBase
    {
        #region Var
        private readonly IBusinessAppService _businessAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly IParkAppService _parkAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IBlackListsAppService _blackListsAppService;
        private readonly ICompanyAppService _companyAppService;
        private readonly IRatesAppService _ratesAppService;


        private readonly IPictureStoreAppService _pictureStoreAppService;
        private readonly string PictureStore = ConfigurationManager.ConnectionStrings["PictureStore"].ToString();
        private readonly IDictionarysAppService _dictionarysApplicationService;
        private readonly IRepository<Berth, long> _abpBerthRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessAppService"></param>
        /// <param name="berthsecAppService"></param>
        /// <param name="regionAppService"></param>
        /// <param name="parkAppService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="blackListsAppService"></param>
        /// <param name="companyAppService"></param>
        /// <param name="pictureStoreAppService"></param>
        /// <param name="dictionarysApplicationService"></param>
        /// <param name="abpBerthRepository"></param>
        public BusinessController(IBusinessAppService businessAppService, IBerthsecAppService berthsecAppService
            , IRegionAppService regionAppService, IParkAppService parkAppService, IEmployeeAppService employeeAppService, IBlackListsAppService blackListsAppService, ICompanyAppService companyAppService, IPictureStoreAppService pictureStoreAppService, IDictionarysAppService dictionarysApplicationService, IRepository<Berth, long> abpBerthRepository, IRatesAppService ratesAppService)
        {
            _businessAppService = businessAppService;
            _berthsecAppService = berthsecAppService;
            _regionAppService = regionAppService;
            _parkAppService = parkAppService;
            _employeeAppService = employeeAppService;
            _blackListsAppService = blackListsAppService;
            _companyAppService = companyAppService;
            _pictureStoreAppService = pictureStoreAppService;
            _dictionarysApplicationService = dictionarysApplicationService;
            _abpBerthRepository = abpBerthRepository;
            _ratesAppService = ratesAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EscapeDetails")]
        public ActionResult EscapeDetails()
        {
            BusinessModel model = new BusinessModel
            {
                regionList = _regionAppService.GetAllRegion(),
                berthsecList = _berthsecAppService.GetAllBerthsec(),
                parkList = _parkAppService.GetParkAll(),
                employeeList = _employeeAppService.GetEmployeeAll(),
                StopType = _dictionarysApplicationService.GetAllValueData("A10023"),
                PayType = _dictionarysApplicationService.GetAllValueData("A10024"),
                AllOption = alloption
            };
            return View(model);
        }

        /// <summary>
        /// 逃逸明细 报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EscapeDetails")]
        public JsonResult GetEscapeDetailsList(Businesses.Dtos.GetEscapeDetailsInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            return Json(_businessAppService.GetEscapeDetailsList(input), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 设置黑名单
        /// </summary>
        /// <param name="PlateNumber"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EscapeRank.Field2")]
        public JsonResult ProcessEscapeBlackList(string PlateNumber)
        {
            string[] strs = PlateNumber.Split(',');

            if (!AbpSession.CompanyId.HasValue)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("分公司账号才能迁移黑名单")));
            }

            var entity = _companyAppService.FirstOrDefault(AbpSession.CompanyId.Value);

            foreach (string str in strs)
            {
                if (!_blackListsAppService.CheckBlackExisis(str))//存在
                {
                    _blackListsAppService.BlackListsInsert(new BlackLists.Dtos.CreateOrUpdateBlackListsInput() { CarOwner = "", CompanyName = AbpSession.CompanyId.Value.ToString(), IdNumber = entity.TelePhone, RelateNumber = str, VehicleType = "0", IsActive = true, Remarks = "手动迁移黑名单" });
                }
            }
            return Json(new MvcAjaxResponse());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetEscapeDetails(string id)
        {
            GetEscapeDetailsInput input = new GetEscapeDetailsInput
            {
                PlateNumber = id,
                page = 1,
                rows = 1000,
                RepaymentYorN = 2
            };
            return GetEscapeDetailsList(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlateNumber"></param>
        /// <param name="id"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("BusinessDetails.Update")]
        public JsonResult ProcessBusiness(string PlateNumber, string id, string oper)
        {
            _businessAppService.UpdateBusiness(PlateNumber, id, oper);
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public JsonResult UpdateMoney(string id, string money)
        {
            _businessAppService.UpdateMoney(id, money);
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 处理逃逸明细明细表（增删改）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessEscpeDetails(CreateOrUpdateEscpeDetails input)
        {
            ErrorInfo info = new ErrorInfo();
            int count = 0;
            switch (input.oper)
            {
                case "del":  //删除
                    count = DeteleEscpeDatails(input);
                    break;
                case "edit"://修改
                    //info.Message = ModifyEscpeDatails(input);
                    break;
                case "add"://添加e

                    //info.Message = InsertEscpeDatails(input);
                    break;
            }
            if (count == 0)
            {
                return this.Json(info.Message == null ? new MvcAjaxResponse(new ErrorInfo("操作失败，请选择一条未追缴的记录")) : new MvcAjaxResponse());
            }
            else
            {
                return this.Json(new MvcAjaxResponse());
            }
        }

        /// <summary>
        /// 删除逃逸明细
        /// </summary>
        /// <param name="input"></param>
        private int DeteleEscpeDatails(CreateOrUpdateEscpeDetails input)
        {
            return _businessAppService.Delete(input.id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("SubstantialOrder.Delete")]
        public JsonResult DeleteSubstantialOrder(CreateOrUpdateEscpeDetails input)
        {
            _businessAppService.Delete(input.id);
            ErrorInfo info = new ErrorInfo();
            return Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("BusinessDetails")]
        public ActionResult BusinessDetails()
        {
            BusinessModel model = new BusinessModel
            {
                regionList = _regionAppService.GetAllRegion(),
                berthsecList = _berthsecAppService.GetAllBerthsec(),
                parkList = _parkAppService.GetParkAll(),
                employeeList = _employeeAppService.GetEmployeeAll(),
                StopType = _dictionarysApplicationService.GetAllValueData("A10023"),
                PayType = _dictionarysApplicationService.GetAllValueData("A10024"),
                AllOption = alloption
            };
            return View(model);
            //return View();
        }

        /// <summary>
        /// 收费明细 linq
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetBusinessDetailsLList(GetBusinessDetailsInput input)
        {
            return Json(_businessAppService.GetBusinessDetailsList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetBusinessDetail(Guid Id)
        {
            var model = _businessAppService.GetBusinessDetailById(Id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 恢复欠费（可恢复多少金额）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public JsonResult ResumeArrearage(Guid id, decimal money)
        {
            _businessAppService.ResumeArrearage(id, money);
            return Json("");
        }

        /// <summary>
        /// 查询 收费明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetBusinessDatailsLListSql(Businesses.Dtos.GetBusinessDetailsInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            //收费明细表数据会重复问题修改20220804
            try
            {
                GetBusinessDetailsOutput DetailSoutput = _businessAppService.GetBusinessDetailsListSql(input);
                List<BusinessDetailsDto> BDetailsDto = new List<BusinessDetailsDto>();
                List<long> IdListA = new List<long>();
                List<long> IdListB = new List<long>();
                foreach (BusinessDetailsDto element in DetailSoutput.rows)
                {
                    if (!IdListA.Contains(element.Id))
                    {

                        IdListA.Add(element.Id);
                        BDetailsDto.Add(element);
                    }
                    else
                    {
                        IdListB.Add(element.Id);
                    }
                }
                foreach (BusinessDetailsDto element in BDetailsDto)
                {
                    if (IdListB.Contains(element.Id))
                    {
                        var SensorsStopTime = 0;
                        decimal Receivable = 0;
                        var time = Convert.ToDateTime(element.SensorsOutCarTime) - Convert.ToDateTime(element.SensorsInCarTime);
                        if ((int)(time.TotalSeconds / 60) > 0)
                        {
                            SensorsStopTime = (int)(time.TotalSeconds / 60);
                        }
                        element.SensorsStopTime = SensorsStopTime;
                        if (SensorsStopTime>0) {
                            //地磁重新计算费用
                            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select CarInTime, BerthsecId, PlateNumber, ParkId, CompanyId from AbpSensorBusinessDetail with(nolock) where guid = '" + element.guid + "'").Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                var model = _ratesAppService.RateCalculate(int.Parse(dt.Rows[0]["BerthsecId"].ToString()), Convert.ToDateTime(element.SensorsInCarTime), Convert.ToDateTime(element.SensorsOutCarTime), 2, 0, int.Parse(dt.Rows[0]["ParkId"].ToString()), dt.Rows[0]["PlateNumber"].ToString(), int.Parse(dt.Rows[0]["CompanyId"].ToString()));
                                Receivable = model.CalculateMoney;
                            }
                        }
                        element.SensorsReceivable = Receivable;
                    }

                }
                DetailSoutput.rows = BDetailsDto;
                return this.Json(DetailSoutput);
            }
            catch (Exception e)
            {
                return this.Json(_businessAppService.GetBusinessDetailsListSql(input));
            }
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operateDateBegin"></param>
        /// <param name="operateDateEnd"></param>
        /// <param name="PlateNumber"></param>
        /// <returns></returns>
        public JsonResult GetPlatenumberLocusList(string operateDateBegin, string operateDateEnd, string PlateNumber)
        {
            return Json(_businessAppService.GetPlatenumberLocusList(operateDateBegin, operateDateEnd, PlateNumber));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BusinessDetailId"></param>
        /// <returns></returns>
        //public JsonResult GetBusinessDetailsPictureList(long id)
        //{
        //    return this.Json(_businessAppService.GetPictureList(id), JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public JsonResult GetBusinessDetailsPictureList(Guid Id)
        {
            //if(PictureStore == "PictureStore")
            return this.Json(_businessAppService.GetPictureList(Id), JsonRequestBehavior.AllowGet);
            //else
            //    return Json(_pictureStoreAppService.GetPicture())
        }

        public JsonResult GetBusinessDetailsPictureList1(int Id)
        {
            var entity=_abpBerthRepository.Load(Id);
            return this.Json(_businessAppService.GetPictureList(entity.guid), JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 大额订单
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SubstantialOrder")]
        public ActionResult SubstantialOrder()
        {
            BusinessModel model = new BusinessModel
            {
                regionList = _regionAppService.GetAllRegion(),
                berthsecList = _berthsecAppService.GetAllBerthsec(),
                parkList = _parkAppService.GetParkAll(),
                employeeList = _employeeAppService.GetEmployeeAll(),
                StopType = _dictionarysApplicationService.GetAllValueData("A10023"),
                PayType = _dictionarysApplicationService.GetAllValueData("A10024"),
                AllOption = alloption
            };
            return View(model);
        }

        /// <summary>
        /// 欠费排名
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EscapeRank")]
        public ActionResult EscapeRank()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EscapeRank")]
        public JsonResult GetEscapeRankList(GetEscapeRankInput input)
        {
            return this.Json(_businessAppService.GetEscapeRankList(input), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 取消追缴费用
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <returns></returns>
        public JsonResult CancelEscapePay(List<string> escapeId)
        {
            int count = _businessAppService.CancelEscapePay(escapeId);
            if (count > 0)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }

        /// <summary>
        /// 获取大额订单列表
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SubstantialOrder")]
        public JsonResult GetSubstantialOrderList(GetBusinessDetailsInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            return this.Json(_businessAppService.GetSubstantialOrderListSql(input));
        }

        /// <summary>
        /// 根据图片ID 显示图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ShowImage(int id)
        {
            PicMongoDto model = null;
            if (PictureStore == "SqlServer")
            {
                //获取 原图片
                model = _pictureStoreAppService.GetPicture(id);
            }
            else
            {
                model = _businessAppService.GetPicture(new PicMongoDto() { BusinessDetailId = id });
            }
            if (string.IsNullOrWhiteSpace(model.FileSavePath))
            {
                return File(model.Image, "image/jpg");
            }
            else
            {
                //存放车牌号图片的host，图片存储位置 即部署的域名
                var host = ConfigurationManager.AppSettings["PlatePictureSaveHost"];
                //图片 绝对根目录 替换前
                var replaceOld = ConfigurationManager.AppSettings["ReplaceOrdStr"];
                //图片虚拟根目录 替换为
                var replaceNew = ConfigurationManager.AppSettings["ReplaceNewStr"];
                //显示图片
                //替换 把数据库跟路径替换成IIS 虚拟目录 用于显示图片
                model.FileSavePath = model.FileSavePath.Replace(replaceOld, replaceNew);
                return Redirect($"{host}/{model.FileSavePath}");
            }
        }
    }
}