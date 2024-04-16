using P4.Berthsecs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllInspectorApiOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Inspector Inspector { get; set; }
    }
}
