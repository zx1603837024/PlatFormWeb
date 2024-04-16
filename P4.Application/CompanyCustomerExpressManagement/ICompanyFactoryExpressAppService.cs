using Abp.Application.Services;
using P4.CompanyCustomerExpressManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CompanyCustomerExpressManagement
{
    public interface ICompanyFactoryExpressAppService : IApplicationService
    {
        /// <summary>
        /// 添加公司工厂快递(返修发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Insert(CreateOrUpdateCompanyFactoryExpress input);

        /// <summary>
        /// 查询公司工厂快递信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllCompanyFactoryExpress GetAllCompanyCustomerExpressList(SearchCompanyFactoryExpress input);

        /// <summary>
        /// 修改公司工厂快递信息
        /// </summary>
        /// <param name="input"></param>
        void Update(CreateOrUpdateCompanyFactoryExpress input);

        /// <summary>
        /// 删除公司工厂快递信息
        /// </summary>
        /// <param name="input"></param>
        void Delete(CreateOrUpdateCompanyFactoryExpress input);

        /// <summary>
        /// 工厂确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int CompanyFactoryExpressConfirm(int Id);
        /// <summary>
        /// 通过id获取返修批次号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetBatchNumById(int id);
        /// <summary>
        /// 工厂发回给公司
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int FactoryInsert(CreateOrUpdateCompanyFactoryExpress input,int selectedId);
        /// <summary>
        /// 公司确认收(工厂)货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int CompanyConfirm(int Id);
    }
}
