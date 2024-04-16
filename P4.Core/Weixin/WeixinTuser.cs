using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Weixin
{
    /// <summary>
    /// 微信注册用户
    /// </summary>
    [Table("TUser")]
    public class TUser : Entity, IMustHaveTenant
    {
        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string nickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(15)]
        public string tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string qq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(155)]
        public string password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(155)]
        public string password2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string openId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public char remember { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime registerDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? lastLoginDate { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int levels { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(50)]
        public string CarNumber1 { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public int? Model1 { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(50)]
        public string CarNumber2 { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public int? Model2 { get; set; }


        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(50)]
        public string CarNumber3 { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public int? Model3 { get; set; }

        /// <summary>
        /// 上次发送微信的时间
        /// </summary>
        public DateTime? SendWeixinDatetime { get; set; }

        /// <summary>
        /// 已发送微信的次数
        /// </summary>
        public int SendWeixinNumber { get; set; }
        
    }
}
