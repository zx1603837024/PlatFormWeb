using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CarownerApp.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class HistoryParkingDto
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string parkname
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string date
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string total
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long orderid
        { get; set; }


    }
}
