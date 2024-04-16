using P4.Dictionarys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class EquipmentsEditModel
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public  string EqId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public  string EqName { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public List<GetAllValueDataDto> EqVersion { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public  int BatchNum { get; set; }

        /// <summary>
        /// 生产工厂
        /// </summary>
        public  string EqFactory { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public  DateTime CreationTime { get; set; }

        /// <summary>
        /// 分公司编号
        /// </summary>
        public  int? TenantId { get; set; }

        /// <summary>
        /// 企业编号
        /// </summary>
        public  int? CompanyId { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartsType { get; set; }
        /// <summary>
        /// 配件编号
        /// </summary>
        public string PartsId { get; set; }

        ///// <summary>
        ///// 设备型号数组
        ///// </summary>
        //public List<string> EqVersionType
        //{
        //    get { return new List<string>() { "A6", "A7", "A8", "A9", "A10" }; }
        //}
    }
}