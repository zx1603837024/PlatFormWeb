using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.CompanyCustomerExpressManagement.Dto;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Linq.Extensions;

namespace P4.CompanyCustomerExpressManagement
{
    public class CompanyFactoryExpressAppService : ApplicationService, ICompanyFactoryExpressAppService
    {
        private readonly IRepository<CompanyFactoryExpress> _abpCompanyFactoryExpressRepository;
        private readonly IRepository<CompanyCustomerExpress> _abpCompanyCustomerExpressRepository;
        private readonly IRepository<ReworkFlow> _abpReworkFlowRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpCompanyFactoryExpressRepository"></param>
        public CompanyFactoryExpressAppService(IRepository<CompanyFactoryExpress> abpCompanyFactoryExpressRepository, IRepository<ReworkFlow> abpReworkFlowRepository, IRepository<CompanyCustomerExpress> abpCompanyCustomerExpressRepository)
        {
            _abpCompanyFactoryExpressRepository = abpCompanyFactoryExpressRepository;
            _abpReworkFlowRepository = abpReworkFlowRepository;
            _abpCompanyCustomerExpressRepository = abpCompanyCustomerExpressRepository;
        }


        /// <summary>
        /// 添加公司工厂快递(返修发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Insert(CreateOrUpdateCompanyFactoryExpress input)
        {
            //流水号重复
            if (_abpCompanyFactoryExpressRepository.FirstOrDefault(dic => dic.CompanyFactoryExpressSerialNum == input.CompanyFactoryExpressSerialNum) != default(CompanyFactoryExpress))
                return 1;
            //该批次已发货
            else if (_abpCompanyFactoryExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) != default(CompanyCustomerExpress))
                return 2;
            //该批次不是返修批次
            else if (_abpReworkFlowRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) == default(CompanyCustomerExpress))
                return 3;
            else if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum && dic.CompanyCustomerExpressState == "公司已收货") == default(CompanyCustomerExpress))
                return 4;
            else
            {
                return _abpCompanyFactoryExpressRepository.Insert(input.MapTo<CompanyFactoryExpress>()) == null ? 1 : 0;
            }
        }

        /// <summary>
        /// 工厂发回给公司
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int FactoryInsert(CreateOrUpdateCompanyFactoryExpress input, int selectedId)
        {
           CompanyFactoryExpress companyFactoryExpress= _abpCompanyFactoryExpressRepository.Load(selectedId);
            //流水号重复
           if (_abpCompanyFactoryExpressRepository.FirstOrDefault(dic => dic.CompanyFactoryExpressSerialNum == input.CompanyFactoryExpressSerialNum) != default(CompanyFactoryExpress))
               return 1;
           //该批次已发货给公司
           else if (_abpCompanyFactoryExpressRepository.GetAllList(dic => dic.BatchNum == input.BatchNum).Count() > 1)
               return 2;
           //工厂已收货才能转发
           else if (companyFactoryExpress.CompanyFactoryExpressState != "工厂已发货")
               return 3;
           else
           {
               return _abpCompanyFactoryExpressRepository.Insert(input.MapTo<CompanyFactoryExpress>()) == null ? 1 : 0;
           }
        }

        /// <summary>
        /// 查询公司工厂快递信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllCompanyFactoryExpress GetAllCompanyCustomerExpressList(SearchCompanyFactoryExpress input)
        {
            int records = _abpCompanyFactoryExpressRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllCompanyFactoryExpress()
            {
                rows = _abpCompanyFactoryExpressRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<CompanyFactoryExpressDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 修改公司工厂快递信息
        /// </summary>
        /// <param name="input"></param>
        public void Update(CreateOrUpdateCompanyFactoryExpress input)
        {
            var model = _abpCompanyFactoryExpressRepository.Load(input.Id);
            model.CompanyFactoryExpressId = input.CompanyFactoryExpressId;
            model.FactoryId = input.FactoryId;
            _abpCompanyFactoryExpressRepository.Update(model);
        }

        /// <summary>
        /// 删除公司工厂快递信息
        /// </summary>
        /// <param name="input"></param>
        public void Delete(CreateOrUpdateCompanyFactoryExpress input)
        {
            _abpCompanyFactoryExpressRepository.Delete(input.Id);
        }

        /// <summary>
        /// 通过Id确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int CompanyFactoryExpressConfirm(int Id)
        {
            var model = _abpCompanyFactoryExpressRepository.Load(Id);
            if (model.CompanyFactoryExpressState == "工厂已收货")
            {
                return 1;
            }
            else if (model.CompanyFactoryExpressState == "公司已发货")
            {
                model.CompanyFactoryExpressState = "工厂已收货";
                _abpCompanyFactoryExpressRepository.Update(model);
                return 0;
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// 公司确认收(工厂)货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int CompanyConfirm(int Id)
        {
            var model = _abpCompanyFactoryExpressRepository.Load(Id);
            if (model.CompanyFactoryExpressState == "公司已收货")
            {
                return 1;
            }
            else if (model.CompanyFactoryExpressState == "工厂已发货")
            {
                model.CompanyFactoryExpressState = "公司已收货";
                _abpCompanyFactoryExpressRepository.Update(model);
                return 0;
            }
            else
            {
                return 2;
            }
        }
        

        /// <summary>
        /// 通过id获取返修批次号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetBatchNumById(int id)
        {
            var model = _abpCompanyFactoryExpressRepository.Load(id);
            return model.BatchNum;
        }
    }
}
