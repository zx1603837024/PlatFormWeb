using Abp.Application.Services.Dto;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSensorCheckTotalOutput : IOutputDto
    {
        /// <summary>
        /// 在线
        /// </summary>
        public int SensorOnLineCount { get; set; }
        /// <summary>
        /// 不在线
        /// </summary>
        public int SensorOffLineCount { get; set; }

        /// <summary>
        /// 车检器总数
        /// </summary>
        public int SensorCount { get; set; }

       
    }
}
