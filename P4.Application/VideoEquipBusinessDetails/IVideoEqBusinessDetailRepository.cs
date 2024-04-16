using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using P4.VideoEquipBusinessDetails.Dtos;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoEquipBusinessDetails
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVideoEquipBusinessDetailRepository : IRepository<VideoEquipBusinessDetail, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        GetAllVideoEqBusinessDetailOutput GetAllVideoEqBusinessDetaillist(VideoEquipBusinessDetailInput input, IAbpSession abpsession);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        GetAllVideoEqBusinessDetailOutput GetAllVideoPileBusinessDetaillist(VideoEquipBusinessDetailInput input, IAbpSession abpsession);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        GetAllVideoEqBusinessDetailOutput GetAllVideoRoadRollBusinessDetaillist(VideoEquipBusinessDetailInput input, IAbpSession abpsession);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        void UpdatePlate(CreateOrUpdateVideoEqBusDetail input, IAbpSession abpsession);
    }
}
