using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.Employees.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Employee))]
    public class EmployeePDADto : EntityDto<long>
    {
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
        public string AccountStatusName { get; set; }

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
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 工作组分配 标识是否已分配为所选工作组
        /// </summary>
        public string isselected { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Latitude { get; set; }
    }
}
