using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors
{
    /// <summary>
    /// 巡查任务回馈
    /// </summary>
    [Table("AbpInspectorTaskFeedbacks")]
    public class InspectorTaskFeedback : Entity<long>
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual long InspectorTaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? BerthsecId { get; set; }

        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(200)]
        public virtual string BerthNumber { get; set; }

        public virtual long? EmployeeId { get; set; }

        /// <summary>
        /// 任务内容
        /// </summary>
        [MaxLength(500)]
        public virtual string TaskContent { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public virtual DateTime UploadTime { get; set; }

        /// <summary>
        /// 任务完成说明
        /// </summary>
        [MaxLength(500)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [MaxLength(200)]
        public virtual string PicUrl1 { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [MaxLength(200)]
        public virtual string PicUrl2 { get; set; }


        /// <summary>
        /// 图片路径
        /// </summary>
        [MaxLength(200)]
        public virtual string PicUrl3 { get; set; }
    }
}
