using P4.Menus;
using System.Collections.Generic;

namespace P4.Web.Models.Layout
{
    /// <summary>
    /// 
    /// </summary>
    public class TopActiveModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Menu> Shortcuts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TenantField { get; set; }

        /// <summary>
        /// 浏览菜单
        /// </summary>
        public string ActiveMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyStatus { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public string MenuType { get; set; }
    }
}