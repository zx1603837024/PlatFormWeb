using P4.Berthsecs.Dto;
using System;
using System.Collections.Generic;

namespace P4.Inspectors.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class InspectorRPB
    {
        /// <summary>
        /// 
        /// </summary>
        public List<int> regionId { get; set; }  //区域集合

        /// <summary>
        /// 
        /// </summary>
        public List<string> parkId { get; set; }//停车场集合

        /// <summary>
        /// 
        /// </summary>
        public List<int> berthsecId { get; set; }//泊位段集合

        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> berthsec { get; set; }
    }
}
