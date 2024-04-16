using Abp.Application.Services;
using P4.VideoEquipBusinessDetails.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.VideoEquipBusinessDetails
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVideoEquipBusinessDetailAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllVideoEqBusinessDetailOutput GetAllVideoEqBusinessDetaillist(VideoEquipBusinessDetailInput input);

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
        GetAllVideoEqBusinessDetailOutput GetAllVideoPileBusinessDetaillist(VideoEquipBusinessDetailInput input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllVideoEqBusinessDetailOutput GetAllVideoRoadRollBusinessDetaillist(VideoEquipBusinessDetailInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete1(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete2(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void Update(CreateOrUpdateVideoEqBusDetail input);
    }


}
