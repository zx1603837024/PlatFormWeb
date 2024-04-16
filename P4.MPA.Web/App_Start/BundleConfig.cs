using System.Web.Optimization;

namespace P4.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //~/Bundles/js
            bundles.Add(
                new ScriptBundle("~/Jqgrid/Language")
                    .Include("~/js/main.js")
                );
        }
    }
}