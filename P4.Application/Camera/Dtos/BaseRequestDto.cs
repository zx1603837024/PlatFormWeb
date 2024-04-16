namespace P4.Camera.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseRequestDto
    {
        /// <summary>
        /// 指令标示
        /// </summary>
        public string cmd { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string devid { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string id { get; set; }
    }
}
