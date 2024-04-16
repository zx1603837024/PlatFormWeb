using Abp.Application.Services;
using Abp.Authorization;
using P4.Berths;
using System;
using System.Collections.Generic;
using System.Data;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessV2AppService : ApplicationService, IBusinessV2AppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthNumber">泊位号</param>
        /// <param name="PlateNumber">车牌号</param>
        /// <param name="CarType">车辆类型</param>
        /// <param name="Prepaid">预缴</param>
        /// <param name="CarInTime">进场时间</param>
        /// <param name="guid">唯一标示</param>
        /// <param name="SensorsInCarTime">车检器进场时间</param>
        /// <param name="StopType">停车类型</param>
        /// <param name="CardNo">卡号</param>
        /// <param name="RegionId">区域id</param>
        /// <param name="ParkId">停车场id</param>
        /// <param name="BerthsecId">泊位段id</param>
        /// <param name="InBatchNo">进场批次号</param>
        /// <param name="UserId">用户id</param>
        /// <param name="DeviceCode">设备编号</param>
        /// <returns></returns>
        public bool InsertCarIn(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string InBatchNo, int UserId, string DeviceCode)
        {
            //判断车位号是否存在
            if (StopType == "F4")//白名单
                StopType = "7";
            Guid g = new Guid(guid);
            Berth berthmodel = null;
            List<Berth> list = Abp.DataProcessHelper.GetEntityFromTable<Berth>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from AbpBerths where BerthsecId =  " + BerthsecId + " and BerthNumber = '" + BerthNumber + "'").Tables[0]);

            if (list != null && list.Count > 0)
                berthmodel = list[0];
            else
                throw new AbpAuthorizationException("入场失败：泊位号不存在！", "23");

            int count = int.Parse(Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select count(1) from AbpBusinessDetail with(nolock) where guid = '" + g + "'").ToString());
            if (count > 0)
            {
                throw new AbpAuthorizationException("入场失败：guid已经存在！", "20");
            }

            return false;
        }
    }
}
