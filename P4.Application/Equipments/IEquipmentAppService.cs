using Abp.Application.Services;
using P4.Equipments.Dtos;
using P4.InspectorCharges.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.Equipments
{

    /// <summary>
    /// 
    /// </summary>
    public interface IEquipmentAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<long> InsertEquipBeat();
        //Gps接口

        /// <summary>
        /// 上传GPS地址
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [HttpGet]
        GetGpsOutput GetGps(string x, string y);

        /// <summary>
        /// 获取pda设备列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEquipmentOutput GetAllEquipmentOutputList(EquipmentInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Insert(CreateOrUpdateEquipmentInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Modify(CreateOrUpdateEquipmentInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 获取pda维护列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEquipmentMaintenanceOutput GetAllEquipmentMaintenanceOutputList(EquipmentMaintenanceInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void DeleteEquipmentMaintenance(int Id);

        /// <summary>
        /// 更新设备版本号
        /// </summary>
        /// <param name="PDA"></param>
        /// <param name="Version"></param>
        /// <param name="tenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        Task UpdateVersion(string PDA, string Version, int tenantId, int CompanyId);

        /// <summary>
        /// 新增设备记录
        /// </summary>
        /// <param name="DeviceCode"></param>
        /// <param name="VersionNum"></param>
        /// <param name="tenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        Task InsertEquipment(string DeviceCode, string VersionNum, int tenantId, int CompanyId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<EquipmentDto> GetEquipmentNumGroupByUseStatus();

        /// <summary>
        /// 
        /// </summary>
        void CargoInfoExport();

        /// <summary>
        /// 收费员/巡查员Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GpsDto> GetEquipmentGpsList(InspectorChargeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void InsertAndroidLog(AndroidLogDto input);
    }
}
