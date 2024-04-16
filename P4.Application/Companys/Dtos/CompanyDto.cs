using System;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
namespace P4.Companys.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(OperatorsCompany))]
    public class CompanyDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoginCredentials
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelePhone
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
        public string Contacts
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastModificationTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long LastModifierUserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CreatorUserId
        { get; set; }

        /// <summary>
        /// 密码1
        /// </summary>
        [MaxLength(20)]
        public virtual string Password1 { get; set; }

        /// <summary>
        /// 密码2
        /// </summary>
        [MaxLength(20)]
        public virtual string Password2 { get; set; }

        /// <summary>
        /// 密码3
        /// </summary>
        [MaxLength(20)]
        public virtual string Password3 { get; set; }

        /// <summary>
        /// 密码4
        /// </summary>
        [MaxLength(20)]
        public virtual string Password4 { get; set; }

        /// <summary>
        /// 密码5
        /// </summary>
        [MaxLength(20)]
        public virtual string Password5 { get; set; }
    }
}
