using System;

namespace P4.Businesses.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class CreateOrUpdateEscpeDetails 
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid id { get; set; }
    }
}
