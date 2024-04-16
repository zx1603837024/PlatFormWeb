namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class DecisionWeixinDto
    {
        /// <summary>
        /// 微信注册用户
        /// </summary>
        public int WeixinRegisterCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int WeixinNewRegisterCount { get; set; }

        /// <summary>
        /// 微信投诉记录
        /// </summary>
        public int WeixinIdeaCount { get; set; }

        /// <summary>
        /// 微信回复记录
        /// </summary>
        public int WeixinRelyIdeaCount { get; set; }
    }
}
