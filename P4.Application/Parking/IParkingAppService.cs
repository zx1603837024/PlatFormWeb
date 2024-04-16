using Abp.Application.Services;
using P4.BlackLists.Dtos;
using P4.Parking.Dtos;
using P4.WhiteLists.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace P4.Parking
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParkingAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <returns></returns>
        [HttpGet]
        List<WeixinPlateDto> GetWeixinPlateList(DateTime updatetime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <param name="tenantId"></param>
        /// <param name="parkId"></param>
        /// <returns></returns>
        [HttpGet]
        List<MonthlyCars.Dtos.MonthlyCarDto> GetMonthlyCarList(DateTime updatetime, int tenantId, int parkId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        List<BlackListsDto> GetBlackCarList(DateTime updatetime, int tenantId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        List<WhiteListsDto> GetWhiteCarList(DateTime updatetime, int tenantId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        int InsertCarinDetail(string BerthNumber, string PlateNumber, string InDeviceCode, string CarType, string Prepaid, int PrepaidPayStatus, string CarInTime, Guid guid, string SensorsInCarTime, string StopType, string CardNo, int TenantId, int CompanyId, int RegionId, int ParkId, int berthsecId, int InOperaId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        int InsertCaroutDetail(Guid guid, int Status, decimal Money, decimal FactReceive, string CarOutTime, string CarPayTime, int OutOperaId, string OutDeviceCode, int PayStatus, bool IsPay, int StopTime, string OutBatchNo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="str"></param>
        /// <param name="businessid"></param>
        /// <param name="pictype"></param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        [HttpPost]
        int InsertPicStore(Guid guid, Stream str, int businessid, int pictype, string createTime);
    }
}
