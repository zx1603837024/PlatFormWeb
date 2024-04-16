using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.DataLogs.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Abp.DataLog.DataLogItem))]
    public class DataLogItemDto : EntityDto<long>
    {
        /// <summary>
        /// 获取或设置 字段
        /// </summary>
        [MaxLength(40)]
        public string Field { get; set; }

        /// <summary>
        /// 获取或设置 字段名称
        /// </summary>
        [MaxLength(50)]
        public string FieldName { get; set; }

        /// <summary>
        /// 获取或设置 旧值
        /// </summary>
        [MaxLength(200)]
        public string OriginalValue { get; set; }

        /// <summary>
        /// 获取或设置 新值
        /// </summary>
        [MaxLength(200)]
        public string NewValue { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string DataType { get; set; }
    }
}
