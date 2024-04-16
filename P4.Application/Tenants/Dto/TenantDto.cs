using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tenants.Dto
{
    /// <summary>
    /// 商户Dto
    /// </summary>
    [AutoMapTo(typeof(Tenant))]
    public class TenantDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string TenancyName
        { get; set; }
        public int DeviceType
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastModificationTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? LastModifierUserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CreatorUserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DomainName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HQ
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string X
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Y
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Telephone
           
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Contacts
        { get; set; }


        /// <summary>
        /// 版本名称
        /// </summary>
        public string EditionName
        { get; set; }

    }
}
