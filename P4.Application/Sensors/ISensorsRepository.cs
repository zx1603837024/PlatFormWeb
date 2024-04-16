using Abp.Domain.Repositories;
using P4.Sensors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors
{
    /// <summary>
    /// 车检器接口
    /// </summary>
    public interface ISensorsRepository : IRepository<Sensor>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSensorsListOutput GetAllSensorList(SearchSensorsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetSensorInfoOutPut GetSensorInfoOnlineOrNot(SearchSensorsInput input);

        /// <summary>
        /// 修改车检器状态，心跳、时间
        /// </summary>
        /// <param name="sensor"></param>
        void SensorStatus(Sensor sensor);

        /// <summary>
        /// 车检器复位
        /// </summary>
        /// <param name="transmitterNumber"></param>
        /// <param name="sensorNumber"></param>
        void SensorReset(string transmitterNumber, string sensorNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetSensorParkRation GetSensorPRatio(SearchSensorsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SensorNumber"></param>
        /// <param name="BeatDatetime"></param>
        /// <returns></returns>
        int UpdateBerthStatus(string SensorNumber, DateTime? BeatDatetime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorBeatTime"></param>
        /// <param name="ParkStatus"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        int SensorResetUpdateBerthStatus(DateTime SensorsOutCarTime, DateTime SensorBeatTime, short ParkStatus, string SensorNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SensorsInCarTime"></param>
        /// <param name="SensorBeatTime"></param>
        /// <param name="SensorGuid"></param>
        /// <param name="ParkStatus"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        int UpdateBerthStatus(DateTime? SensorsInCarTime, DateTime SensorBeatTime, Guid SensorGuid, short ParkStatus, string SensorNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorBeatTime"></param>
        /// <param name="SensorGuid"></param>
        /// <param name="ParkStatus"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        int UpdateBerthCarOutStatus(DateTime? SensorsOutCarTime, DateTime SensorBeatTime, Guid? SensorGuid, short ParkStatus, string SensorNumber);

             
    }
}
