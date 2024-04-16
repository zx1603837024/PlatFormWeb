using Abp.Domain.Repositories;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using P4.CompanyCustomerExpressManagement.Dto;
using Abp.SignalR.SignalrService;
using Abp.Application.Services;
using P4.Companys;
using P4.Companys.Dtos;

namespace P4.CompanyCustomerExpressManagement
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyCustomerExpressAppService : ApplicationService, ICompanyCustomerExpressAppService
    {
        /// <summary>
        /// 公司客户快递(发货/返修)
        /// </summary>
        private readonly IRepository<CompanyCustomerExpress> _abpCompanyCustomerExpressRepository;
        private readonly IRepository<EquipmentsM> _abpEquipmentsMRepository;
        private readonly IP4ChatService _p4ChatService;
        private readonly IRepository<Employees.Employee, long> _employeeAppService;
        private readonly ICompanyAppService _companyAppService;
        private readonly IRepository<ReworkFlow> _abpReworkFlowRepository;
        private readonly IRepository<CompanyFactoryExpress> _abpCompanyFactoryExpressRepository;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpCompanyCustomerExpressRepository"></param>
        /// <param name="abpEquipmentsMRepository"></param>
        /// <param name="p4ChatService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="companyAppService"></param>
        /// <param name="abpReworkFlowRepository"></param>
        /// <param name="abpCompanyFactoryExpressRepository"></param>
        public CompanyCustomerExpressAppService(IRepository<CompanyCustomerExpress> abpCompanyCustomerExpressRepository, IRepository<EquipmentsM> abpEquipmentsMRepository, IP4ChatService p4ChatService, IRepository<Employees.Employee, long> employeeAppService, ICompanyAppService companyAppService, IRepository<ReworkFlow> abpReworkFlowRepository, IRepository<CompanyFactoryExpress> abpCompanyFactoryExpressRepository)
        {
            _abpCompanyCustomerExpressRepository = abpCompanyCustomerExpressRepository;
            _abpEquipmentsMRepository = abpEquipmentsMRepository;
            _p4ChatService = p4ChatService;
            _employeeAppService = employeeAppService;
            _companyAppService = companyAppService;
            _abpReworkFlowRepository = abpReworkFlowRepository;
            _abpCompanyFactoryExpressRepository = abpCompanyFactoryExpressRepository;
        }
        /// <summary>
        /// 添加公司客户快递(发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Insert(Dto.CreateOrUpdateCompanyCustomerExpress input)
        {

            if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.CompanyCustomerExpressSerialNum == input.CompanyCustomerExpressSerialNum) != default(CompanyCustomerExpress))
                return 1;
            else if (_abpEquipmentsMRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) == default(CompanyCustomerExpress))
                return 2;
            else if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) != default(CompanyCustomerExpress))
                return 3;
            else
            {
                var employee = _employeeAppService.Load(AbpSession.UserId.Value);
                string message = "批次号:" + input.BatchNum + ",流水号:" + input.CompanyCustomerExpressSerialNum + ",快递单号:" + input.CompanyCustomerExpressId + "客户" + input.CompanyId + ",快递类型:" + input.EquipmentDeliveryType + "(" + employee.UserName + ")";
                _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendPrivateMessage(message, 5);

                return _abpCompanyCustomerExpressRepository.Insert(input.MapTo<CompanyCustomerExpress>()) == null ? 1 : 0;
            }
        }

        /// <summary>
        /// 查询发货详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllCompanyCustomerExpress GetAllCompanyCustomerExpressList(SearchCompanyCustomerExpress input)
        {
            int records = _abpCompanyCustomerExpressRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllCompanyCustomerExpress()
            {
                rows = _abpCompanyCustomerExpressRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<CompanyCustomerExpressDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompanyCustomerExpress> GetAllCompanyCustomerExpressListToSelect()
        {
            return _abpCompanyCustomerExpressRepository.GetAllList();
        }

        /// <summary>
        /// 修改发货信息
        /// </summary>
        /// <param name="input"></param>
        public void Update(CreateOrUpdateCompanyCustomerExpress input)
        {

            var model = _abpCompanyCustomerExpressRepository.Load(input.Id);
            model.CompanyCustomerExpressId = input.CompanyCustomerExpressId;
            model.CompanyId = Convert.ToInt32(input.CompanyName);
            model.EquipmentDeliveryType = input.EquipmentDeliveryType;
            _abpCompanyCustomerExpressRepository.Update(model);
        }

        /// <summary>
        /// 删除发货信息
        /// </summary>
        /// <param name="input"></param>
        public void Delete(CreateOrUpdateCompanyCustomerExpress input)
        {
            _abpCompanyCustomerExpressRepository.Delete(input.Id);
        }

        /// <summary>
        /// 获取分公司名称作下拉
        /// </summary>
        /// <returns></returns>
        public List<CompanyDto> GetAllCompanyName()
        {
            return _companyAppService.GetAllCompanyName();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CompanyDto> GetAllCompanyName(int id)
        {
            return _companyAppService.GetAllCompanyName(id);
        }

        /// <summary>
        /// 通过Id确认收货（客户）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int ConfirmReceiptById(int Id)
        {
            var model = _abpCompanyCustomerExpressRepository.Load(Id);
            if (model.CompanyCustomerExpressState == "客户已收货")
            {
                return 1;
            }
            else if (model.CompanyCustomerExpressState == "公司已发货")
            {
                model.CompanyCustomerExpressState = "客户已收货";
                _abpCompanyCustomerExpressRepository.Update(model);
                return 0;
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// 添加公司客户快递(返修)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int DeliveryInsert(Dto.CreateOrUpdateCompanyCustomerExpress input)
        {
            if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.CompanyCustomerExpressSerialNum == input.CompanyCustomerExpressSerialNum) != default(CompanyCustomerExpress))
                return 1;
            else if (_abpReworkFlowRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) == default(CompanyCustomerExpress))
                return 2;
            else if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) != default(CompanyCustomerExpress))
                return 3;
            else
            {
                return _abpCompanyCustomerExpressRepository.Insert(input.MapTo<CompanyCustomerExpress>()) == null ? 1 : 0;
            }
        }

        /// <summary>
        /// 通过id获取返修批次号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetBatchNumById(int id)
        {
            var model = _abpCompanyCustomerExpressRepository.Load(id);
            return model.BatchNum;
        }

        /// <summary>
        /// 添加公司客户快递(返修发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int ReworkInsert(Dto.CreateOrUpdateCompanyCustomerExpress input)
        {
            //流水号重复
            if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.CompanyCustomerExpressSerialNum == input.CompanyCustomerExpressSerialNum) != default(CompanyCustomerExpress))
                return 1;
            ////批次号重复
            //else if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) != default(CompanyCustomerExpress))
            //    return 3;
            //批次号存在
            if (_abpCompanyFactoryExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) != default(CompanyFactoryExpress))
            {
                if (_abpCompanyFactoryExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum && dic.CompanyFactoryExpressState == "公司已收货") != default(CompanyFactoryExpress))
                {
                    return _abpCompanyCustomerExpressRepository.Insert(input.MapTo<CompanyCustomerExpress>()) == null ? 1 : 0;
                }
                else
                {
                    //公司还未收货
                    return 2;
                }
            }
            else
            {
                //批次号不存在
                return 4;
            }
        }

        /// <summary>
        /// 公司确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int CompanyConfirmReceiptById(int Id)
        {
            var model = _abpCompanyCustomerExpressRepository.Load(Id);
            if (model.CompanyCustomerExpressState == "公司已收货")
            {
                return 1;
            }
            else if (model.CompanyCustomerExpressState == "客户已发货")
            {
                model.CompanyCustomerExpressState = "公司已收货";
                _abpCompanyCustomerExpressRepository.Update(model);
                return 0;
            }
            else
            {
                return 2;
            }
        }
    }
}
