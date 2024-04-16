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
    public class PicDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public LocationDto location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeStampDto timeStamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string imageFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int imageFileLen { get; set; }
    }
}
