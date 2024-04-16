using Abp.Application.Services;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusinessV2AppService : IApplicationService
    {
        /// <summary>
        /// 车辆入场(跳过校验在线用户信息)
        /// </summary>
        /// <param name="BerthNumber"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="CarType"></param>
        /// <param name="Prepaid"></param>
        /// <param name="CarInTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsInCarTime"></param>
        /// <param name="StopType"></param>
        /// <param name="CardNo"></param>
        /// <param name="RegionId"></param>
        /// <param name="ParkId"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="InBatchNo"></param>
        /// <param name="UserId"></param>
        /// <param name="DeviceCode"></param>
        /// <returns></returns>
        bool InsertCarIn(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string InBatchNo, int UserId, string DeviceCode);
    }
}
