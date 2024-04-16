using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Companys.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(Company))]
    public class CreateCompanyInput : EntityRequestInput, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
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
        public string Contacts
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password5 { get; set; }

     
     
      
     

        
    }
}
