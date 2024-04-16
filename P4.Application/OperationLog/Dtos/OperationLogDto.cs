using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using Abp.DataLog;

namespace P4.OperationLog.Dtos
{
    /// <summary>
    /// 操作日志
    /// </summary>
    [AutoMapFrom(typeof(Abp.DataLog.OperationLog))]
    public class OperationLogDto : EntityDto<long>
    {

        public string Name { get; set; }

        public string OperatingType { get; set; }

        public string EntityKey { get; set; }

        public DateTime CreationTime { get; set; }

        public string CreationUserName { get; set; }

        public long CreatorUserId { get; set; }
    }
}
