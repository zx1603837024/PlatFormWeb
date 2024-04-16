using Abp.Web.Mvc.Views;

namespace P4.Web.Views
{
    public abstract class P4WebViewPageBase : P4WebViewPageBase<dynamic>
    {

    }

    public abstract class P4WebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected P4WebViewPageBase()
        {
            LocalizationSourceName = P4Consts.LocalizationSourceName;
        }
    }
}