using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ReworkFlowManagement.Dto
{
    [AutoMapFrom(typeof(ReworkPicture))]
    public class ReworkPictureDto : EntityDto
    {
        /// <summary>
        /// 返修流程id
        /// </summary>
        public virtual int ReworkPictureId { get; set; }

        public byte[] Image { get; set; }
    }
}
