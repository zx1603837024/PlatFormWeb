using Abp.Application.Services;
using P4.PrintAdvertisement.Dtos;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
namespace P4.PrintAdvertisement
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrintAdAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllPrintAdsOutput GetAllPrintAdsList(PrintAdInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool DeletePrintAd(CreatOrUpdatePrintAdInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool UpdatePrintAd(CreatOrUpdatePrintAdInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool InsertPrintAd(CreatOrUpdatePrintAdInput input);

        /// <summary>
        /// 打印列表
        /// </summary>
        /// <returns></returns>
        List<PrintAdDto> GetAllPrintAdsList();
    }
}
