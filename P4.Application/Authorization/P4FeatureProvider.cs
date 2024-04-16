using Abp.Application.Features;
using P4.Features;

namespace P4.Authorization
{
    /// <summary>
    /// 功能权限管理
    /// </summary>
    public class P4FeatureProvider : FeatureProvider
    {
        #region Var
        private readonly IFeaturesAppService _featureLimitationAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="featureLimitationAppService"></param>
        public P4FeatureProvider(IFeaturesAppService featureLimitationAppService)
        {
            _featureLimitationAppService = featureLimitationAppService;
        }

        /// <summary>
        /// 设置版本功能权限
        /// </summary>
        /// <param name="context"></param>
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            var entity = _featureLimitationAppService.GetAll();
            foreach (var temp in entity)
            {
                context.Create(temp.Name, defaultValue: temp.DefaultValue);
            }
        }
    }
}
