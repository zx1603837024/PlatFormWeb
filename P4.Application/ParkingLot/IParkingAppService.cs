using Abp.Application.Services;
using P4.ParkingLot.Dto;
using P4.ParkingLot.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot
{
    /// <summary>
    /// 停车场服务类
    /// </summary>
    public interface IParkingAppService: IApplicationService
    {
        /// <summary>
        /// 修改包月
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ModifyMonthlyRent(ModifyMonthlyRentInput input);

        /// <summary>
        /// 删除包月
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string DleteMonthlyRent(ModifyMonthlyRentInput input);

        /// <summary>
        /// 新增包月
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateMonthlyRent(ModifyMonthlyRentInput input);


        /// <summary>
        /// 获取包月列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetParkingMonthlyRentListOutput GetParkingMonthlyRentList(GetParkingMonthlyRentListInput input);


        /// <summary>
        /// 获取停车场对应的通道列表
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        GetParkChannelOutput GetParkChannel(int parkId);


        /// <summary>
        /// 编辑通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ModifyParkChannel(ModifyParkChannelInput input);
    }
}
