using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Editions.Dtos
{
    /// <summary>
    /// elementui使用treeModel
    /// </summary>
    public class ElTreeDto
    {
        /// <summary>
        /// 对应数据库ID
        /// </summary>
        public int key { get; set; }

        /// <summary>
        /// 编号 校验唯一性
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// url链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public string pId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }

        /// <summary>
        /// 权限功能 只包括：Insert Update Delete Info Search Refresh
        /// </summary>
        public List<string> basePowers { get; set; }

        /// <summary>
        /// 自定义权限
        /// </summary>
        public List<ElTreeDto_Power> customPower { get; set; }

        /// <summary>
        /// 子级菜单
        /// </summary>
        public List<ElTreeDto> children { get; set; }
    }

    /// <summary>
    /// 权限描述
    /// </summary>
    public class ElTreeDto_Power
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string desc { get; set; }
    }
}
