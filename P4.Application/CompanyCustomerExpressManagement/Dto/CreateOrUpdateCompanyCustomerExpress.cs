using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CompanyCustomerExpressManagement.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(CompanyCustomerExpress))]
    public class CreateOrUpdateCompanyCustomerExpress : EntityDto, IInputDto, IOperDto
    {

        /// <summary>
        /// 公司客户快递流水号
        /// </summary>
        public virtual string CompanyCustomerExpressSerialNum { get; set; }

        /// <summary>
        /// 公司客户快递单号
        /// </summary>
        public virtual string CompanyCustomerExpressId { get; set; }

        /// <summary>
        /// 公司客户快递状态
        /// </summary>
        public virtual string CompanyCustomerExpressState { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public virtual int BatchNum { get; set; }

        ///// <summary>
        ///// 客户
        ///// </summary>
        //public virtual string Customer { get; set; }

        /// <summary>
        /// 设备发货类型(正常发货/返修)
        /// </summary>
        public virtual string EquipmentDeliveryType { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }
    }
}
