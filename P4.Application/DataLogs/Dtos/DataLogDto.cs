using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace P4.DataLogs.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Abp.DataLog.Datalog))]
    public class DataLogDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OperateType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EntityKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreationUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  string EntityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DataLogItemDto> LogItems { get; set; } 
    }
}
