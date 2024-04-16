using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.Companys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Companys
{
    public interface ICompanyAppService : IApplicationService
    {
        GetAllCompanyOutput GetAll(OperatorCompanyInput input);
        ListResultOutput<CompanyDto> GetAll();
        CompanyDto FirstOrDefault(int CompanyId);

         string Insert(CreateCompanyInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
         void SaveCompany(CompanyDto entity);

        string Update(CreateCompanyInput input);
        List<CompanyDto> GetAllCompanyName();

        void Delete(CreateCompanyInput input);
        //CompanyDto GetCompany(GetCompanyInput input);

        /// <summary>
        /// 通过id查询分公司名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<CompanyDto> GetAllCompanyName(int id);
    }
}
