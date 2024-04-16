using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using P4.Inducibles;
using P4.Inducibles.Dtos;
using P4.Parks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 诱导系统
    /// </summary>
    [AbpMvcAuthorize]
    public class InducibleSystemController : P4ControllerBase
    {
        #region Var
        private readonly IRepository<Inducible> _abpInducibleRepository;
        private readonly IInducibleAppService _inducibleAppService;
        private readonly IParkAppService _parkAppServoce;
        #endregion

        public InducibleSystemController(IRepository<Inducible> abpInducibleRepository, IInducibleAppService inducibleAppService, IParkAppService parkAppServoce)
        {
            _abpInducibleRepository = abpInducibleRepository;
            _inducibleAppService = inducibleAppService;
            _parkAppServoce = parkAppServoce;
        }

        /// <summary>
        /// 诱导地图展示
        /// </summary>
        /// <returns></returns>
        public ActionResult InducibleMap()
        {
            Models.InducibleModel inducibleModel = new Models.InducibleModel();
  
            inducibleModel.inducibleList = _inducibleAppService.GetAllInducible();
            inducibleModel.parkList = _parkAppServoce.GetParkAll();
            return View(inducibleModel);
        }

        /// <summary>
        /// 诱导参数设置
        /// </summary>
        /// <returns></returns>
        public ActionResult InducibleParam()
        {
       
           Models.InducibleModel inducibleModel = new Models.InducibleModel();
           inducibleModel.inducibleList =_inducibleAppService.GetAllInducible();
           inducibleModel.parkList = _parkAppServoce.GetParkAll();
           //inducibleModel.sensorList = _inducibleAppService.GetEmplyBerthsecs();
           inducibleModel.sensorParkList = _inducibleAppService.GetParkToSensor();
           return View(inducibleModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inducibleDto"></param>
        public void SaveInducible(InducibleDto inducibleDto)
        {
            _inducibleAppService.SaveInducible(inducibleDto);
        }


        /// <summary>
        /// 获取诱导数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllInducible()
        {
            Models.InducibleModel inducibleModel = new Models.InducibleModel();
            inducibleModel.inducibleList = _inducibleAppService.GetAllInducible();
            inducibleModel.parkList = _parkAppServoce.GetParkAll();
            //inducibleModel.sensorList = _inducibleAppService.GetEmplyBerthsecs();
            inducibleModel.sensorParkList = _inducibleAppService.GetParkToSensor();
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(inducibleModel),
                ContentType = "application/json"
            };

        }
        public JsonResult GetEmplyBerthsecs()
        {
            return this.Json(_inducibleAppService.GetEmplyBerthsecs());
        }
        
    }
}