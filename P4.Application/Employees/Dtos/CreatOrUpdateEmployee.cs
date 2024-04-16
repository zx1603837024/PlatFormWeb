using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Employees.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Employee))]
    public   class CreatOrUpdateEmployee: EntityRequestInput, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
         public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BankCard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccountStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPhoneConfirmed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CheckIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CheckOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CheckOuttype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CheckOutUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Users.User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }
     
    }
}
