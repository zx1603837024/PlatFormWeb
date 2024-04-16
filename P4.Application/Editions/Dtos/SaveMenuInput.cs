using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Editions.Dtos
{
    /// <summary>
    /// 保存菜单
    /// </summary>
    public class SaveMenuInput
    {
        /// <summary>
        /// 菜单ID 判断是否是新增还是更新
        /// </summary>
        public int menuId { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string menuCode { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuName { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public string menuParentCode { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string menuUrl { get; set; }
        
        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<ElTreeDto_Power> menuPowers { get; set; }

        /// <summary>
        /// 版本ID
        /// </summary>
        public int editionId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
    }
}
