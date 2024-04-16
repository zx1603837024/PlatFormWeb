using Abp.Domain.Repositories;
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
    public interface IParkRepository : IRepository<Park>
    {
        /// <summary>
        /// 返回车场管理数据列表
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
        /// 
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CompanyIds"></param>
        /// <returns></returns>
        List<ParkInfoMap> GetParkInfoMapList(int? TenantId, List<int> CompanyIds);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ParkAppDto> GetParkAppAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<ParkDto> GetParkNumber(int? tenantID);

        /// <summary>
        /// 获取所有通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetParkChannelOutput GetAllChannel(GetParkChannelInput input);
    }
}
