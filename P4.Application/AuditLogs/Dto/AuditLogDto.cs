using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using System;
namespace P4.AuditLogs.Dto
{
    /// <summary>
    /// 审计日志
    /// </summary>
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}您在{1}操作了{2}耗时{3}秒,访问的地址{4},浏览器信息{5}", UserId, ExecutionTime, MethodName, ExecutionDuration * 0.001, ClientIpAddress, BrowserInfo);
        }
    }
}
