using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.Inspectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Inspector))]
    public class InspectorDto : EntityDto<long>
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
        public string CheckInTimeStr
        {
            get
            {
                if (CheckInTime.HasValue && CheckIn)
                    return CheckInTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return "";
            }
        }
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
        public string CheckOutTimeStr
        {
            get
            {
                if (CheckOutTime.HasValue && CheckOut)
                    return CheckOutTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return "";
            }
        }

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

        /// <summary>
        /// 
        /// </summary>
        public string isselected { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string UpLastTime { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Y { get; set; }

        /// <summary>
        /// 设备版本
        /// </summary>
        public string Version { get; set; }
    }
}
