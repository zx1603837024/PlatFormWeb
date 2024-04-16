using Abp.Application.Services;
using P4.BigScreen.Dtos;
using P4.Sensors.Dtos;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace P4.Sensors
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISensorAppService : IApplicationService
    {
        /// <summary>
        /// 设备心跳/电压、电容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        Sensor InsertSensor(Sensor entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Magnetism"></param>
        /// <param name="Battery"></param>
        /// <param name="TransmitterNumber"></param>
        /// <param name="SensorNumber"></param>
        /// <param name="ParkStatus"></param>
        /// <returns></returns>
        [HttpPost]
        Sensor InsertSensor(decimal? Magnetism, decimal? Battery, string TransmitterNumber, string SensorNumber, short ParkStatus);


       /// <summary>
       /// 
       /// </summary>
       /// <param name="TransmitterNumber"></param>
       /// <param name="SensorNumber"></param>
       /// <returns></returns>
        [HttpPost]
        bool SensorReset(string TransmitterNumber, string SensorNumber);

        /// <summary>
        /// 读取车检器信息
        /// </summary>
        /// <param name="SensorNumber">车检器编号</param>
        /// <returns></returns>
        [HttpPost]
        SensorDto LoadSensor(string SensorNumber);

        /// <summary>
        /// 复位车检器,返回车检器对象
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        [HttpPost]
        SensorDto SensorResetFirstDefault(string TransmitterNumber, string SensorNumber);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetAllSensorsBeatOutput GetSensorBeatList(SearchSensorBeatInput input);

        /// <summary>
        /// 基站心跳/电压、电容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        Transmitter InsertTransmitter(Transmitter entity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <param name="VoltageCaution"></param>
        /// <returns></returns>
        [HttpPost]
        Transmitter InsertTransmitter(string TransmitterNumber, decimal? VoltageCaution);

        /// <summary>
        /// 车辆入场
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        SensorBusinessDetail InsertCarAdmission(SensorBusinessDetail entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CarInTime"></param>
        /// <param name="Indicate"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        [HttpPost]
        SensorBusinessDetail InsertCarAdmission(DateTime CarInTime, string Indicate, string SensorNumber);

        /// <summary>
        /// 车辆出场
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        SensorBusinessDetail InsertCarEntrance(SensorBusinessDetail entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CarOutTime"></param>
        /// <param name="Indicate"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        [HttpPost]
        SensorBusinessDetail InsertCarEntrance(DateTime CarOutTime, string Indicate, string SensorNumber);
        /// <summary>
        /// 通讯数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        SensorLog InsertSensorLog(SensorLogInput entity);

        /// <summary>
        /// 通讯日志
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="Exception"></param>
        /// <param name="ReceiptCmd"></param>
        /// <returns></returns>
        [HttpPost]
        SensorLog InsertSensorLog(string Content, string Exception, string ReceiptCmd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSensorsListOutput GetAllSensorsList(SearchSensorsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetSensorInfoOutPut GetSensorInfoOnlineOrNot(SearchSensorsInput input);


        /// <summary>
        /// 获取地磁信息
        /// </summary>
        /// <returns></returns>
        //List<SensorsDetail> GetSensorsDetail();




        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        List<SensorDto> GetSensorByBerthsecId(int berthsecid);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<SensorInduciblesDto> GetAllSensor();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSensorsFaultOutput GetFaultSensorList(SearchSensorFaultInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSensorRunStatusOutput GetSensorRunStatusList(SearchSensorRunStatusInput input);

        /// <summary>
        /// 获取基站下车检器数据
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <returns></returns>
        [HttpPost]
        List<SensorDto> GetSensors(string TransmitterNumber);

        /// <summary>
        /// 获取基站信息
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <returns></returns>
        [HttpPost]
        Transmitter GetTransmitterInfo(string TransmitterNumber);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetSensorParkRation GetSensorPRatio(SearchSensorsInput input);

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        void InsertSensorMagnetic(string TransmitterNumber, string SensorNumber, int Number, int Voltage, int Signal, int X, int Y, int Z);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetSensorLogOutput GetSensorLogsList(SearchSensorLogInput input);

        ///// <summary>
        ///// string SendDeviceByINmotion(INmotionDto msg);
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //string SendDeviceByINmotion(INmotionDto dto);


        /// <summary>
        /// Post地磁请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        string SendDeviceByINmotion();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetSensorPosOutPut GetSensorPosList(SearchSensorPosInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetSensorPosOutPut GetSensorBerthList(SearchSensorPosInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="device"></param>
        /// <param name="flag"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        [HttpGet]
        string SendDeviceByINmotionTest(string time, string device, string flag, string sequence);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        bool RemoteInsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, int BerthsecId, string OutBatchNo);
    }
}
