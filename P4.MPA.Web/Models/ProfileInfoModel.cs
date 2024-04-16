using Abp.Authorization;
using P4.DataLogs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProfileInfoModel
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public Users.User user { set; get; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public List<string> RegionList { get; set; }

        public List<string> ParkList { get; set; }

        public List<string> BerthsecList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> UserRoles { get; set; }

        /// <summary>
        /// 功能权限
        /// </summary>
        public List<Permission> Permissions { get; set; }

        public GetAllDataLogsOutput DataLogs { get; set; }
    }
}