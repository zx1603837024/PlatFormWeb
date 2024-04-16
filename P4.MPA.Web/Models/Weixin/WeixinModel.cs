using System.Collections.Generic;

namespace P4.Web.Models.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinHistory> WeixinHistory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Weixinplate> Weixinplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Weixinuser Weixinuser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<park> park { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<parking> parking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Wexinparkorder> Wexinparkorder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long InspectorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Berths.Dtos.BerthDto> BerthList { get; set; }
    }
}