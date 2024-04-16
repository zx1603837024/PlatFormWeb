/********************************************************************************
** auth： 黎通
** date： 2015/12/16 20:24:02
** desc： 尚未编写描述
** Ver.:  V1.0.0
*********************************************************************************/
using System.Collections.Generic;
using Abp.Application.Services;
using P4.CarownerApp.Dtos;
using System.Web.Http;
using P4.OtherAccounts.Dtos;

namespace P4.CarownerApp
{

    /// <summary>
    /// 车主端管理类
    /// </summary>
    public interface ICarownerAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        [HttpGet]
        string GetPhoneCode(string userPhone);


        /// <summary>
        /// 获取停车场详细信息
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        [HttpGet]
        ParkDto GetParkInfo(int parkId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="phonecode"></param>
        /// <returns></returns>
        [HttpGet]
        string AppLogin(string username, string phonecode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="msg"></param>
        void SendAlarmMsg(string phone, string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="oldplateNumber"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool BindPlateNumber(string plateNumber, string oldplateNumber, string mobile);

        /// <summary>
        /// p=android&v=2410&action=detail&mobile=13851483025
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        [HttpGet]
        AppAccountOutput GetAccountInfo(string p, string v, string action,string mobile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        List<Dtos.DeductionRecordDto> GetDeductionRecordList(string p, string v, string action, string mobile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        List<PlateDto> GetPlateList(string p, string v, string action, string mobile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        OrderDto GetCurrentOrder(string p, string v, string action, string mobile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        OrderDto GetOrderDetail(string guid);

        /// <summary>
        /// 获取用户设置
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        SettingInfoDto GetProf(string p, string v, string action, string mobile);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="Platenumber1"></param>
        /// <param name="Platenumber2"></param>
        /// <param name="Platenumber3"></param>
        /// <returns></returns>
        string UploadPlatenumber(string Phone, string Platenumber1, string Platenumber2, string Platenumber3);

        /// <summary>
        /// 获取停车场数据（附近）
        /// </summary>
        /// <param name="payable"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [HttpGet]
        ParkOutput GetParkList(string payable, string lat, string lng, string p, string v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        List<HistoryParkingDto> GetHistoryOrderList(string p, string v, int size, int page, string mobile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        List<MonthyDto> GetUseMonthyList(string action, string mobile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <param name="carnumber"></param>
        /// <param name="oldplateNumber"></param>
        /// <returns></returns>
        [HttpPost]
        int AddPlate(string p, string v, string action, string mobile, string carnumber, string oldplateNumber);
    }
}
