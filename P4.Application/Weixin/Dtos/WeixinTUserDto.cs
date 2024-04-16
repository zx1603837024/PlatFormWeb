using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 微信用户管理
    /// </summary>
    [AutoMapFrom(typeof(TUser))]
    public class WeixinTUserDto : EntityDto
    {
        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string password2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string openId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public char remember { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime registerDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string registerDateStr
        {
            get { return registerDate.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? lastLoginDate { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string lastLoginDateStr
        {
            get {
                if (lastLoginDate.HasValue)
                    return lastLoginDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int levels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarNumber1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Model1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarNumber2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Model2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarNumber3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Model3 { get; set; }
    }
}
