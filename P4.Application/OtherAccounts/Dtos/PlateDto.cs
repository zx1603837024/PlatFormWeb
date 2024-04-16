using Abp.AutoMapper;
using P4.OtherPlateNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(OtherPlateNumber))]
    public class PlateDto
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 认证进度
        /// </summary>
        public int CarType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int is_default { get { return 0; } }
    }
}
