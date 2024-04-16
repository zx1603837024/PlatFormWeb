using Abp.Application.Services.Dto;

namespace P4.Collectors.Dtos
{
    public class GetsignoOnlineOutput : IOutputDto
    {
        /// <summary>
        /// 在线
        /// </summary>
        public int SignoOnLineCount { get; set; }
        /// <summary>
        /// 不在线
        /// </summary>
        public int SignoOffLineCount { get; set; }

        /// <summary>
        /// 道闸总数
        /// </summary>
        public int SignoCount { get; set; }
    }
}
