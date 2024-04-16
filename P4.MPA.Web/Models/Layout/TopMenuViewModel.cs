using Abp.Application.Navigation;

namespace P4.Web.Models.Layout
{
    /// <summary>
    /// 
    /// </summary>
    public class TopMenuViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public UserMenu MainMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActiveMenuItemName { get; set; }

        /// <summary>
        /// 保存点击的菜单树形
        /// </summary>
        public UserMenuItem ActiveMenuItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MenuType { get; set; }
    }
}