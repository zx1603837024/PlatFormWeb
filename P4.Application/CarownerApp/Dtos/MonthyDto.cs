namespace P4.CarownerApp.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class MonthyDto
    {
        /// <summary>
        /// 有效期限
        /// </summary>
        public string limitdate { get; set; }

        /// <summary>
        /// 剩余天数
        /// </summary>
        public string limitday { get; set; }

        /// <summary>
        /// 有效时段
        /// </summary>
        public string limittime { get; set; }

        /// <summary>
        /// 停车场
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 停车名称
        /// </summary>
        public string parkname { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
    }
}
