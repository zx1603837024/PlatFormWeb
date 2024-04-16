using Abp.Application.Services;
using P4.ExportCommon.Dtos;

namespace P4.ExportCommon
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExportCodeAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        ExportCodeDto GetExportCodeDefault(string Code);
    }
}
