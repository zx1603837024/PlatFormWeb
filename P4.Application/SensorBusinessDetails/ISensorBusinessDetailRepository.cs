using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using P4.SensorBusinessDetails.Dtos;
using P4.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SensorBusinessDetails
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISensorBusinessDetailRepository : IRepository<SensorBusinessDetail, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        GetAllSensorBusinessDetailOutput GetAllSensorBusinessDetaillist(SensorBusinessDetailInput input, IAbpSession abpsession);
    }
}
