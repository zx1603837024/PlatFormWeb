using Abp.Application.Services;

namespace P4
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class P4AppServiceBase : ApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        protected P4AppServiceBase()
        {
            LocalizationSourceName = P4Consts.LocalizationSourceName;
        }
    }
}