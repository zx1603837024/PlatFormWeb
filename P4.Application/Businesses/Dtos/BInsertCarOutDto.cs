using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 批量出场实体
    /// </summary>
    public class BInsertCarOutDto
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Arrearage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarOutTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarPayTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OutOperaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsOutCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsStopTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsReceivable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PayStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IsPay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FeeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StopTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Money { get; set; }
    }
}
