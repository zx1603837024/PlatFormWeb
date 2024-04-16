using P4.Dictionarys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 系统日志模型
    /// </summary>
    public class SystemLogModel
    {
        /// <summary>
        /// 字典操作数据下拉菜单
        /// </summary>
        public List<GetAllValueDataDto> DictionaryValue { get; set; }
    }
}