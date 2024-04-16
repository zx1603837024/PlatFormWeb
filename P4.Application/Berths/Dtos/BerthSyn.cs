using System;
using System.ComponentModel.DataAnnotations;

namespace P4.Berths
{
    /// <summary>
    /// 
    /// </summary>
    public  class BerthSyn
    {
        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }
        /// <summary>
        /// 泊位再停状态
        /// 1: 在停
        /// 2: 未停
        /// 3: 15分钟免费停
        /// </summary>
        [MaxLength(10)]
        public  string BerthStatus { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public  string RelateNumber { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>

        public  DateTime? InCarTime { get; set; }
        /// <summary> 
        /// 
        /// 出场时间
        /// </summary>
        public  DateTime? OutCarTime { get; set; }
        /// <summary>
        /// 唯一key
        /// </summary>
        public  Guid guid { get; set; }
    }
}
