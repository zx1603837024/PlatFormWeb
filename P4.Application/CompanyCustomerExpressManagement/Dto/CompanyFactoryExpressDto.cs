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
    [AutoMapFrom(typeof(CompanyFactoryExpress))]
    public class CompanyFactoryExpressDto : EntityDto
    {
        /// <summary>
        /// 公司工厂快递流水号
        /// </summary>
        public virtual string CompanyFactoryExpressSerialNum { get; set; }

        /// <summary>
        /// 公司工厂快递单号
        /// </summary>
        public virtual string CompanyFactoryExpressId { get; set; }

        /// <summary>
        /// 公司工厂快递状态
        /// </summary>
        public virtual string CompanyFactoryExpressState { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public virtual int BatchNum { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public virtual string FactoryId { get; set; }

        /// <summary>
        /// 设备发货类型(正常发货/返修)
        /// </summary>
        public virtual string EquipmentDeliveryType { get; set; }
    }
}
