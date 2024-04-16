using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Webchat
{
    /// <summary>
    /// 广告管理
    /// </summary>
    [Table("AbpAds")]
    public class Ad : Entity
    {
        /// <summary>
        /// 广告图片
        /// </summary>
        public virtual string AdImage { get; set; }
    }
}
