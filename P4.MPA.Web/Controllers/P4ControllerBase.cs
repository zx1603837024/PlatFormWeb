using Abp.Web.Mvc.Controllers;

namespace P4.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class P4ControllerBase : AbpController
    {
        /// <summary>
        /// 下拉菜单模板
        /// </summary>
        public string option = "<option value=\"{0}\">{1}</option>";

        /// <summary>
        /// 下拉菜单，全部
        /// </summary>
        public string alloption = "<option value=\"0\">全部</option>";
        public string nooption = "<option value=\"0\">请选择</option>";
        public string emptyoption = "<option value=\"0\">   </option>";
        protected P4ControllerBase()
        {
            LocalizationSourceName = P4Consts.LocalizationSourceName;
        }
    }
}