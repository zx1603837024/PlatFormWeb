using Abp.Application.Services;
using P4.Rates.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Rates
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRatesAppService : IApplicationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllRateOutput GetAllRateList(RateInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RateDto GetRateById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Delete(RateSaveOrUpdateDto input);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Edit(RateModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(RateModel model);

        /// <summary>
        /// 费率列表
        /// </summary>
        /// <returns></returns>
        List<RateDto> GetAllRateName();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="inCarTime"></param>
        /// <param name="outCarTime"></param>
        /// <param name="carType"></param>
        /// <param name="RateId"></param>
        /// <param name="parkId"></param>
        /// <param name="plateNumber"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        RateCalculateModel RateCalculate(int berthsecID, DateTime inCarTime, DateTime outCarTime, int carType, int RateId, int parkId, string plateNumber, int companyId);
    }
}
