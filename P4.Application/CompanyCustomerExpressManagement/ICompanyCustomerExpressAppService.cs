using Abp.Application.Services;
using P4.CompanyCustomerExpressManagement.Dto;
using P4.Companys.Dtos;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CompanyCustomerExpressManagement
{
    public interface ICompanyCustomerExpressAppService:IApplicationService
    {
        /// <summary>
        /// 添加公司客户快递(发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Insert(CreateOrUpdateCompanyCustomerExpress input);

        /// <summary>
        /// 查询发货详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllCompanyCustomerExpress GetAllCompanyCustomerExpressList(SearchCompanyCustomerExpress input);

        /// <summary>
        /// 修改发货信息
        /// </summary>
        /// <param name="input"></param>
        void Update(CreateOrUpdateCompanyCustomerExpress input);

        /// <summary>
        /// 删除发货信息
        /// </summary>
        /// <param name="input"></param>
        void Delete(CreateOrUpdateCompanyCustomerExpress input);

        /// <summary>
        /// 获取分公司名称作下拉
        /// </summary>
        /// <returns></returns>
        List<CompanyDto> GetAllCompanyName();

        List<CompanyDto> GetAllCompanyName(int id);

        /// <summary>
        /// 通过Id确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int ConfirmReceiptById(int Id);

        List<CompanyCustomerExpress> GetAllCompanyCustomerExpressListToSelect();

        /// <summary>
        /// 添加公司客户快递(返修)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int DeliveryInsert(Dto.CreateOrUpdateCompanyCustomerExpress input);

        /// <summary>
        /// 通过id获取返修批次号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetBatchNumById(int id);

        /// <summary>
        /// 添加公司客户快递(返修发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int ReworkInsert(Dto.CreateOrUpdateCompanyCustomerExpress input);

        /// <summary>
        /// 公司确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int CompanyConfirmReceiptById(int Id);
    }
}
