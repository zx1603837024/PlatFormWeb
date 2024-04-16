using Abp.Application.Services;
using P4.Berths.Dtos;
using P4.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace P4.Berths
{

    /// <summary>
    /// 
    /// </summary>
    public interface IBerthAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBerthOutput GetAllBethlist(BerthInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string BerthUpdate(CreatOrUpdateBerthInput input);

        /// <summary>
        /// 收费员获取管理泊位号
        /// </summary>
        /// <returns></returns>
        List<BerthDto> GetAllBerths();

        /// <summary>
        /// 地感数据同步接口
        /// </summary>
        /// <param name="SyncTime"></param>
        /// <returns></returns>
        [HttpGet]
        List<BerthSensorDto> GetBerthsSyn(string SyncTime);

        /// <summary>
        /// 地感数据同步接口New
        /// </summary>
        /// <param name="SyncTime"></param>
        /// <param name="Berthesclist"></param>
        /// <returns></returns>
        [HttpGet]
        List<BerthSensorDto> GetBerthsSynNew(string SyncTime, string Berthesclist);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="berth"></param>
        /// <returns></returns>
        int UpdateBerthSensorGuid(Berth berth);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        List<Berth> GetBerthListByBerthsecid(int berthsecid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        List<BerthDto> GetBerthsByBerthsecid(int berthsecid);

        /// <summary>
        /// 获取详细泊位段detail
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <param name="CheckInEmployeeId"></param>
        /// <param name="CheckOutEmployeeId"></param>
        /// <returns></returns>
        EmployeeGps GetBerthsecGpsDetail(int BerthsecId, long CheckInEmployeeId, long CheckOutEmployeeId);

        /// <summary>
        /// 通过泊位id，获取guid
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        BerthDto GetBerthGuidByBerthId(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthids"></param>
        /// <returns></returns>
        int BerthToSensorNolock(List<int> berthids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <returns></returns>
        List<BerthDto> GetBerthList(int berthsecId);


        /// <summary>
        /// 查询车辆再停数据
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        [HttpGet]
        List<BerthDto> GetBerthInfoByPlateNumber(string plateNumber);
    }
}
