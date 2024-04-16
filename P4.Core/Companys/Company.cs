using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Companys
{
    /// <summary>
    /// P3外部公司列表
    /// 认证用
    /// </summary>
    [Table("AbpCompanys")]
    public class Company : FullAuditedEntity<int>, IPassivable
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        [MaxLength(200)]
        public string CompanyName
        { get; set; }
        /// <summary>
        /// 登录凭证
        /// </summary>
        [MaxLength(100)]
        public string LoginCredentials
        { get; set; }
        [MaxLength(30)]
        public string X
        { get; set; }
        [MaxLength(30)]
        public string Y
        { get; set; }
       



        public string TelePhone
        { get; set; }
        public string Address
        { get; set; }

        public string Contacts
        { get; set; }

       

        public bool IsActive { get; set; }
    }
}
