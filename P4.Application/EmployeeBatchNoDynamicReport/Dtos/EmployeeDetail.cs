using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EmployeeBatchNoDynamicReport.Dtos
{
    /// <summary>
    /// 收费员明细
    /// </summary>
    public class EmployeeDetail
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime CheckInTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string strInTime { get; set; }
        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime CheckOutTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string strOutTime { get; set; }
        /// <summary>
        /// 签到批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 泊位段
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 泊位段ID
        /// 可能是多个
        /// </summary>
        public string BerthsecId { get; set; }
    }
}
