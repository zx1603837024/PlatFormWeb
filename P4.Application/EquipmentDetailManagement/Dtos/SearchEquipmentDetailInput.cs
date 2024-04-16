using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentDetailManagement.Dtos
{
    public class SearchEquipmentDetailInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {

        //public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        public virtual string EqId { get; set; }
    }
}
