namespace P4.Camera.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequestDto : BaseRequestDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string sn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mac { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string agrver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hardver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string softver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string idaddr { get; set; }

    }
}
