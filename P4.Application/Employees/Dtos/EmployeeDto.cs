using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.EmployeeCharges.Dto;
using System;

namespace P4.Employees.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Employee))]
    public class EmployeeDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string BerthsecId { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCard { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public string AccountStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccountStatusName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 是否手机确认
        /// </summary>
        public bool IsPhoneConfirmed { get; set; }

        /// <summary>
        /// 是否签到
        /// </summary>
        public bool CheckIn { get; set; }

        /// <summary>
        /// 签到时间
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
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 是否签退
        /// </summary>
        public bool CheckOut { get; set; }

        /// <summary>
        /// 签退类型
        /// </summary>
        public string CheckOuttype { get; set; }

        /// <summary>
        /// 签退用户
        /// </summary>
        public long? CheckOutUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Users.User User { get; set; }

        /// <summary>
        /// 签退时间
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
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 分公司
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
        /// 
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Y { get; set; }

        /// <summary>
        /// 泊位段名称
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UpLastTime { get; set; }

        /// <summary>
        /// 软件版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmployeeChargesDto EmployeeCharges { get; set; }
    }
}
