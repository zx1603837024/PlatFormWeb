using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models.Layout
{
    public class ProfileModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Navbar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sidebar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Breadcrumbs { get; set; }

        public string Rtl { get; set; }

        public string Container { get; set; }

        /// <summary>
        /// 皮肤
        /// </summary>
        public string Skin { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public string MenuType { get; set; }

        /// <summary>
        /// 菜单栏折叠
        /// </summary>
        public string SidebarCollapsed { get; set; }
    }
}