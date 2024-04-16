using Abp.Application.Services;
using P4.Berthsecs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berthsecs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBerthsecAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBerthsecsListOutput GetBerthsecList(SearchBerthsecInput input);

        /// <summary>
        /// 道闸
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBerthsecsListOutput GetSignoParkList(SearchBerthsecInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetAllBerthsecsListOutput GetBerthsecList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string InsertBerthsec(CreateOrUpdateBerthsecInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteBerthsec(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ModifyBerthsec(CreateOrUpdateBerthsecInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        BerthsecDto Load(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<BerthsecDto> GetUseDataPermissions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        List<BerthsecDto> LoadToSql(List<int> Ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LParkIds"></param>
        /// <returns></returns>
        List<BerthsecDto> LoadListBerhtsec(List<int> LParkIds);

        /// <summary>
        /// 异步加载数据
        /// 禁用过滤器
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<BerthsecDto> LoadAsync(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<BerthsecDto> GetBerthsecsListByList(List<int> list);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        GetAllBerthsecCheckListOutput GetAllBerthsecCheckList(long EmployeeId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<BerthsecDto> GetAllBerthsec();
        /// <summary>
        /// 收费员签到修改泊位段数据
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employessID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task UpdateBerthsecIn(int berthsecID, string DeviceCode, int employessID, int tenantId);

        /// <summary>
        /// 收费员签到业务（存储过程）
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="TenantID"></param>
        /// <param name="checkInOrOutTime"></param>
        /// <param name="CompanyId"></param>
        /// <param name="ParkID"></param>
        /// <param name="VersionNum"></param>
        /// <param name="BatchNo"></param>
        void EmployeeCheckInPro(List<int> berthsecId, string DeviceCode, int employeeID, int TenantID, DateTime checkInOrOutTime, int CompanyId, int ParkID, string VersionNum,string BatchNo);

        /// <summary>
        /// 收费员签退业务（存储过程）
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="checkInOrOutTime"></param>
        void EmployeeCheckOutPro(int berthsecId, string DeviceCode, int employeeID, DateTime checkInOrOutTime);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="checkInOrOutTime"></param>
        void EmployeeCheckOutOulinePro(string berthsecId, string DeviceCode, int employeeID, DateTime checkInOrOutTime);

        /// <summary>
        /// 收费员签退修改泊位段数据
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employessID"></param>
        void UpdateBerthsecOut(int berthsecID, string DeviceCode, int employessID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        int BerthsecBatchCheckOutBGO(List<int> BerthsecId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParkId"></param>
        /// <returns></returns>
        List<BerthsecDto> GetBerthsecByParkID(int ParkId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        string GetBerthList(int BerthsecId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savetype"></param>
        /// <param name="chooseberthsecid"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        bool SaveBerthsToBerthsec(string savetype, int chooseberthsecid, string nodes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int SaveBerthsecAddress(BerthsecMapDto model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        BerthsecDto GetBerthsecById(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<BerthsecDto> GetBerthsecMapList(int Id);


    }
}
