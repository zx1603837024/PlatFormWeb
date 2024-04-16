using Abp.Application.Features;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using Abp.AutoMapper;

namespace P4.Features
{
    /// <summary>
    /// 功能限制管理
    /// </summary>
    public class FeaturesAppService : P4AppServiceBase, IFeaturesAppService
    {

        #region Var

        private readonly IRepository<FeatureLimitation> _featureLimitationAppService;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="featureLimitationAppService"></param>
        public FeaturesAppService(IRepository<FeatureLimitation> featureLimitationAppService)
        {
            _featureLimitationAppService = featureLimitationAppService;
        }

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public List<Dtos.FeaturesDto> GetAll()
        {
            return _featureLimitationAppService.GetAll().MapTo<List<Dtos.FeaturesDto>>();
        }
    }
}
