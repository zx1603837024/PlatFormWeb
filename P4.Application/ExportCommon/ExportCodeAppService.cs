using Abp.Domain.Repositories;
using P4.ExportCommon.Dtos;
using Abp.AutoMapper;

namespace P4.ExportCommon
{
    /// <summary>
    /// 
    /// </summary>
    public class ExportCodeAppService : P4AppServiceBase, IExportCodeAppService
    {
        #region Var
        private readonly IRepository<P4.ExportCommon.ExportCode> _abpExportCodeRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpExportCodeRepository"></param>
        public ExportCodeAppService(IRepository<P4.ExportCommon.ExportCode> abpExportCodeRepository)
        {
            _abpExportCodeRepository = abpExportCodeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ExportCodeDto GetExportCodeDefault(string Code)
        {
            return _abpExportCodeRepository.FirstOrDefault(entity => entity.Code == Code && entity.IsActive == true).MapTo<ExportCodeDto>();
        }
        
    }
}
