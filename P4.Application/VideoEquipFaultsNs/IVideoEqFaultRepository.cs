using Abp.Domain.Repositories;
using P4.VideoEquipFaultsNs.Dtos;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;

namespace P4.VideoEquipFaultsNs
{
    /// <summary>
    /// 仓库接口类
    /// </summary>
    public interface IVideoEqFaultRepository : IRepository<VideoEquipFaults, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        VeqFaultOutput GetAllVideoEqFaultsList(VeqFaultInput input, IAbpSession abpsession);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        VeqFaultOutput GetAllVideoEqFaultsList1(VeqFaultInput input, IAbpSession abpsession);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        VeqFaultOutput GetAllVideoEqFaultsList2(VeqFaultInput input, IAbpSession abpsession);
    }
}
