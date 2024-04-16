using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    [AutoMapFrom(typeof(InspectorTaskFeedback))]
    public class InspectorTaskFeedbacksDto : EntityDto<long>
    {
        public string BerthNumber { get; set; }

        public string EmployeeName { get; set; }

        public string TaskContent { get; set; }

        public string Remark { get; set; }

        public string PicUrl1 { get; set; }

        public DateTime? UploadTime { get; set; }
    }
}
