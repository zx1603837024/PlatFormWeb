using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EmployeeLoggings.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(EmployeeCheck))]
    public class EmployeeCheckDto : EntityDto<long>
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        public bool IsNormal { get; set; }
        /// <summary>
        /// 是否对账
        /// </summary>
        public bool IsRepeal { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get;set;}

        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 停车场ID
        /// </summary>
        public string ParkID { get; set; }

        /// <summary>
        /// 泊位段ID
        /// </summary>
        public string BerthsecID { get; set; }     
           
        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 是否签到
        /// </summary>
        public bool CheckIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CheckOut { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? CheckOutTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long EmployeeId { get; set; }
    }
}
