using System;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace P4.Berthsecs.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Berthsec))]
    public class BerthsecDto : EntityDto, ICustomValidate
    {
        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CheckOutName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CheckInName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RegionId { get; set; }

       /// <summary>
       /// 
       /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BeginNumeber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int EndNumeber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CustomNumeber { get; set; }

        
        /// <summary>
        /// 早班费率
        /// </summary>
        public int RateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RateName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FeeModel { get; set; }

        /// <summary>
        /// 中班费率
        /// </summary>
        public int? RateId1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RateName1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FeeModel1 { get; set; }


        /// <summary>
        /// 晚班费率
        /// </summary>
        public int? RateId2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RateName2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FeeModel2 { get; set; }

        /// <summary>
        /// <summary>
        /// 是否启用
        /// </summary>

        public bool IsActive { get; set; }

        /// <summary>
        /// 签到标记位
        /// true为签到
        /// </summary>
        public bool CheckInStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CheckOutStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CheckInEmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CheckOutEmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CheckInDeviceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CheckOutDeviceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void AddValidationErrors(System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult> results)
        {
            if (IsActive == false)
            {
                CheckInStatus = false;
                CheckOutStatus = false;
            }

            if (CheckInStatus == false)
            { CheckInDeviceCode = null; CheckInTime = null; CheckInEmployeeId = null; }

            if (CheckOutStatus == false)
            { CheckOutDeviceCode = null; CheckOutTime = null; CheckOutEmployeeId = null; }

        }

        /// <summary>
        /// 工作组分配 标识是否已分配为所选工作组
        /// </summary>
        public string isselected { get; set; }

        /// <summary>
        /// 泊位总数
        /// </summary>
        public string BerthCount { get; set; }

        /// <summary>
        /// 未被签到为false
        /// 被签到为true
        /// </summary>
        public bool UseStatus { get; set; }
        /// <summary>
        /// 是否需获取车检器状态
        /// true获取，false不获取
        /// </summary>
        public bool PushStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lat { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Lng { get; set; }




        /// <summary>
        /// 道闸进口数
        /// </summary>
        public int? SignoInSize { get; set; }
        /// <summary>
        /// 道闸出口数
        /// </summary>
        public int? SignoOutSize { get; set; }
        /// <summary>
        /// 通讯时间
        /// </summary>
        public DateTime SignoCommunationTime { get; set; }
    }
}
