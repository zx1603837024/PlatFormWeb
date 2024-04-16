using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Equipment))]
    public class EquipmentDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PDA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sim { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool SD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Pasm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Printers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Standard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool GPS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Scan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UseStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? GetTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UseNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int16 UseStatusInt { get; set; }

        /// <summary>
        /// 是否升级
        /// </summary>
        public bool IsUpgrade { get; set; }
    }
}
