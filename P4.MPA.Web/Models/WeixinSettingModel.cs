using P4.Employees.Dtos;
using P4.Weixin.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinSettingModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<EmployeeDto> employeeList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Users.Dto.UserDto> UserList { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public WeixinConfigDto weixinconfigmodel { get; set; }

        public string AllOption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BackPayUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool BerthDetail { get; set; }

        public string ZappId { get; set; }

        public string privateKey { get; set; }

        public string alipayPulicKey { get; set; }

        public string serverUrl { get; set; }
    }
}