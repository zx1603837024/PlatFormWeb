using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.Dictionarys.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(DictionaryValue))]
    public class DictionaryValueInput : EntityRequestInput, IOrder, IOperDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool _search { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sord { get; set; }
    }
}
