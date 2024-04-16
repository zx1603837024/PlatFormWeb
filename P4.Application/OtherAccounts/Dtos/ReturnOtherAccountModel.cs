namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ReturnOtherAccountModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string TelePhone { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 钱包
        /// </summary>
        public decimal Wallet { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 是否自动扣款
        /// </summary>
       public bool AutoDeduction { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }
    }
}
