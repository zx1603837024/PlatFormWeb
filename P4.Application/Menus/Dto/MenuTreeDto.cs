using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Menus.Dto
{
    [AutoMapFrom(typeof(Menu))]
    public class MenuTreeDto : IShouldNormalize
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }

        private string _type;
        /// <summary>
        /// 菜单类型
        /// </summary>
        public string type
        {
            get { return "item"; }
            set { _type = value; }
        }

        /// <summary>
        /// 默认值赋值
        /// </summary>
        public void Normalize()
        {
            type = "item";
        }
    }
}
