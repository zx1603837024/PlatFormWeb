using P4.CompanyCustomerExpressManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CompanyCustomerExpressManagement
{
    public interface ICompanyCustomerExpressRepository
    {
        /// <summary>
        /// 查询客户公司快递表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllCompanyCustomerExpress GetCompanyCustomerExpressAll(SearchCompanyCustomerExpress input);
    }
}
