﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.InspectorCharges.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectorChargeInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string operateDateBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string operateDateEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime begindt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime enddt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        public string employeeIdInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string employeeNameInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }
    }
}