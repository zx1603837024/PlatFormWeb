using Abp.Application.Services;
using P4.ParkingLot.Dto;
using P4.ParkingLot.Dtos;
using P4.Parks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Parks
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParkAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkOutput GetParkAll(ParkInput input);

        /// <summary>
        /// 获取所有的封闭停车场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkOutput GetCurbPark(ParkInput input);

        /// <summary>
        /// 获取所有的通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetParkChannelOutput GetAllChannel(GetParkChannelInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Insert(CreateOrUpdateParkInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Modify(CreateOrUpdateParkInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ParkDto Load(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ParkDto> GetParkAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ParkAppDto> GetParkAppAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ParkDto> InspectorGetParkAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        List<ParkDto> GetParkByRegionID(int regionId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ParkDto> GetParkNumber();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ParkInfoMap> GetParkInfoMapList();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ParkDto> GetUseDataPermissions();

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ParkInfoMap GetParkInfoById(int Id);



        /// <summary>
        /// 保存停车场坐标
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        int SaveParkAddress(ParkInfoMap map);


        /// <summary>
        /// 添加停车场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       string InsertChannel(CreateOrUpdateParkChannelInput input);

        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="Id"></param>
        void DeleteChannel(int Id);

        /// <summary>
        /// 编辑通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ModifyChannerl(CreateOrUpdateParkChannelInput input);

        /// <summary>
        /// 获取绑定停车场平台的数据
        /// </summary>
        /// <returns></returns>
        List<ParkDto> GetBindParkingParks();

    }
}
