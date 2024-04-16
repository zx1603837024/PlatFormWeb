using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class LoginViewModel
    {

        public string TenancyName { get; set; }

        public string id { get; set; }

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 是否记住登录
        /// </summary>
        public string RememberMe { get; set; }

        /// <summary>
        /// 总部地址
        /// </summary>
        public string HQ { get; set; }

        public string DomainName { get; set; }

        /// <summary>
        /// 提交类型
        /// </summary>
        public string RequestType { get; set; }
    }
}