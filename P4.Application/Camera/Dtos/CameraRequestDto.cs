using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Camera.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraRequestDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string cmd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int picnum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string license { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  int direction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int colortype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int confidence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string newlicense { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int newtype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int newcolortype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int newconfidence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string devid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PicDataDto> picdata { get; set; }

    }
}
