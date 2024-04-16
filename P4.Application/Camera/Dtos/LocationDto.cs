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
    public class LocationDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Rect RECT { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Rect
    {
        /// <summary>
        /// 
        /// </summary>
       public int left { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int right { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int bottom { get; set; }
    }
}
