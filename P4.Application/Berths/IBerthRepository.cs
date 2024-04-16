using Abp.Domain.Repositories;
using P4.Berths.Dtos;
using System;
using System.Collections.Generic;

namespace P4.Berths
{

    /// <summary>
    /// 
    /// </summary>
    public interface IBerthRepository : IRepository<Berth, long>
    {
        /// <summary>
        /// 获取泊位数据
        /// </summary>
        /// <param name="input"></param>
        /// /// <param name="tenantId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        GetAllBerthOutput GetBerthAll(BerthInput input, int? tenantId, int? companyId);

        /// <summary>
        /// 获取泊位列表（PDA专用）
        /// </summary>
        /// <returns></returns>
        List<BerthDto> GetAllBerths(List<int> BerthsecId);

        /// <summary>
        /// 获取地磁数据（PDA专用）
        /// </summary>
        /// <param name="SyncTime"></param>
        /// <param name="BerthsecID"></param>
        /// <returns></returns>
        List<BerthSensorDto> GetAllBerthSensor(string SyncTime, List<int> BerthsecID);

        /// <summary>
        /// 车辆进场  泊位在停
        /// </summary>
        /// <param name="BerthNumber"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="CarInTime"></param>
        /// <param name="guid"></param>
        /// <param name="CarType"></param>
        /// <param name="CardNo"></param>
        /// <param name="Prepaid"></param>
      void  CarInUpdateBerhs(string BerthNumber, int BerthsecId, string PlateNumber, DateTime? CarInTime, Guid guid, short CarType, string CardNo, decimal Prepaid);
    }
}
