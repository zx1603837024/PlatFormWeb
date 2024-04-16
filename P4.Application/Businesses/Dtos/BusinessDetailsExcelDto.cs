using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// Excel 模板参数
    /// </summary>
    public class BusinessDetailsExcelDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string BdeTenant { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeRegionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeParkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeBerthsecName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeBerthNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdePlateNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeCarInTimeString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeCarOutTimeString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeStopTimes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal BdePrepaid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal BdeReceivable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal BdeFactReceive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal BdeArrearage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeStatusName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeStopTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeInEmployeeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeInDeviceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeOutEmployeeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeOutDeviceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeChargeEmployeeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdeChargeDeviceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdSensorsInCarTimeString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdSensorsOutCarTimeString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BdSensorsStopTimes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BdSensorsReceivable { get; set; }
    }
}
