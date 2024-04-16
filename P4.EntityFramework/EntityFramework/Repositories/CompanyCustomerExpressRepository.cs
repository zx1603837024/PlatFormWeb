using P4.CompanyCustomerExpressManagement;
using P4.CompanyCustomerExpressManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.EquipmentMaintain;
using P4.Companys;
using Abp.EntityFramework;

namespace P4.EntityFramework.Repositories
{
    public class CompanyCustomerExpressRepository : P4RepositoryBase<CompanyCustomerExpress>, ICompanyCustomerExpressRepository
    {
        public CompanyCustomerExpressRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
        /// <summary>
        /// 查询公司客户快递表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllCompanyCustomerExpress GetCompanyCustomerExpressAll(SearchCompanyCustomerExpress input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new CompanyCustomerExpressDto
            {
                Id = c.Id,
                BatchNum = c.BatchNum,
                CompanyCustomerExpressId = c.CompanyCustomerExpressId,
                CompanyCustomerExpressSerialNum = c.CompanyCustomerExpressSerialNum,
                CompanyCustomerExpressState = c.CompanyCustomerExpressState,
                EquipmentDeliveryType = c.EquipmentDeliveryType,
                CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(entity => entity.Id == c.CompanyId).CompanyName
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllCompanyCustomerExpress()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
