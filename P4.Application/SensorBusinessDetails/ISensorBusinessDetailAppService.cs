using Abp.Application.Services;
using P4.SensorBusinessDetails.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.SensorBusinessDetails
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISensorBusinessDetailAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSensorBusinessDetailOutput GetAllSensorBusinessDetaillist(SensorBusinessDetailInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        string GetSensorBusinessdetail(string guid);
    }
}
