using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CarownerApp.Dtos
{
    /// <summary>
    /// 停车场Dto
    /// </summary>
    public class ParkDto
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual int Id
        { get; set; }

        /// <summary>
        /// 停车场名称
        /// </summary>
        public virtual string name
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string lat
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string lng
        { get; set; }

        /// <summary>
        /// 总车位数
        /// </summary>
        public virtual int total
        { get; set; }

        /// <summary>
        /// 空车位数
        /// </summary>
        public virtual int free
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string addr { get { return desc; } }

        /// <summary>
        /// 
        /// </summary>
        public virtual string phone { get { return ""; } }

        /// <summary>
        /// 
        /// </summary>
        public virtual string epay { get { return "1"; } }

        /// <summary>
        /// 
        /// </summary>
        public virtual string price { get { return "0"; } }

        /// <summary>
        /// 停车场收费类型
        /// 0收费停车场
        /// 1免费停车场
        /// </summary>
        public virtual string type { get { return "0"; } }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string desc
        { get; set; }

        public List<AppBerthListDto> berthslist { get; set; }

    }
}
