using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    [AutoMapFrom(typeof(Inspector))]
    public  class CreatOrUpdateInspector:EntityRequestInput, IOperDto
    {
        public string oper { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }
        public string BankCard { get; set; }
        public string AccountStatus { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public bool CheckIn { get; set; }
        public DateTime? CheckInTime { get; set; }

        public string DeviceCode { get; set; }
        public bool CheckOut { get; set; }

        public string CheckOuttype { get; set; }
        public long? CheckOutUserId { get; set; }

        public virtual Users.User User { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Address { get; set; }

        public int TenantId { get; set; }
        public int CompanyId { get; set; }

        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
}
