using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Businesses;
using P4.EmployeeCharges.Dto;
using System.Collections.Generic;
using System;

namespace P4.EmployeeCharges
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeChargesAppService: ApplicationService, IEmployeeChargesAppService 
    {
        #region Var
        private readonly IBusinessRepository _businessRepository;
        private readonly IRepository<BusinessDetail, long> _businessDetailRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessRepository"></param>
        /// <param name="businessDetailRepository"></param>
        public EmployeeChargesAppService(IBusinessRepository businessRepository, IRepository<BusinessDetail, long> businessDetailRepository)
        {
            _businessDetailRepository = businessDetailRepository;
            _businessRepository = businessRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<EmployeeChargesDto> GetEmployeeChargesDayReport(EmployeeChargeInput input)
        {
            //GetMoneyInput getmoneyinput = new GetMoneyInput();
            //getmoneyinput.BeginTime = input.begindt;
            //getmoneyinput.EndTime = input.enddt;

            List<EmployeeChargesDto> employeechargesDaylist = new List<EmployeeChargesDto>();
            employeechargesDaylist = _businessRepository.GetMoneyTotalGroupbyEmployeeEchars(input);
            //List<User> roles = _userRepository.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList();
          //  parkchargesDaylist = _businessDetailRepository.GetAll().Where(bd => bd.CarInTime >= beginDateTime && bd.CarInTime <= endDateTime).ToList();
            //parkchargesDaylist = _businessDetailRepository.GetAll().Where(bd => bd.CarInTime >= beginDateTime && bd.CarInTime <= endDateTime).ToList();
            return employeechargesDaylist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<EmployeeChargesDto> GetChargesRateDayEcharReport(EmployeeChargeInput input)
        {
            //GetMoneyInput getmoneyinput = new GetMoneyInput();
            //getmoneyinput.BeginTime = input.begindt;
            //getmoneyinput.EndTime = input.enddt;

            List<EmployeeChargesDto> employeechargesDaylist = new List<EmployeeChargesDto>();
            employeechargesDaylist = _businessRepository.GetMoneyTotalGroupbyRateEchars(input);
            return employeechargesDaylist;
        }

        /// <summary>
        /// 收费员报表列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeChargesOutput GetEmployeeChargesDayReport1(EmployeeChargeInput input)
        {
            //GetMoneyInput getmoneyinput = new GetMoneyInput();
            //getmoneyinput.BeginTime = input.begindt;
            //getmoneyinput.EndTime = input.enddt;
            List<EmployeeChargesDto> employeechargesDaylist = new List<EmployeeChargesDto>();
            return _businessRepository.GetMoneyTotalGroupbyEmployee(input);
           
        }

        /// <summary>
        /// 收费率报表列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeChargesOutput GetChargeRateDayReport(EmployeeChargeInput input)
        {
            //GetMoneyInput getmoneyinput = new GetMoneyInput();
            //getmoneyinput.BeginTime = input.begindt;
            //getmoneyinput.EndTime = input.enddt;
            List<EmployeeChargesDto> employeechargesDaylist = new List<EmployeeChargesDto>();
            return _businessRepository.GetMoneyTotalGroupbyDate(input);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeChargesOutput GetEmployeeChargesDayReportDetail(EmployeeChargeInput input)
        {
            //GetMoneyInput getmoneyinput = new GetMoneyInput();
            //getmoneyinput.BeginTime = input.begindt;
            //getmoneyinput.EndTime = input.enddt;
            List<EmployeeChargesDto> employeechargesDaylist = new List<EmployeeChargesDto>();
            return _businessRepository.GetMoneyTotalGroupbyEmployeeDetail(input);
        }

    }
}
