using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Businesses;
using System.Collections.Generic;
using P4.ParkCharges.Dto;
using System;
using Abp.Domain.Uow;

namespace P4.ParkCharges
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkChargesAppService : ApplicationService, IParkChargesAppService
    {
        #region Var
        private readonly IBusinessRepository _businessRepository;
        private readonly IRepository<BusinessDetail, long> _businessDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessRepository"></param>
        /// <param name="businessDetailRepository"></param>
        public ParkChargesAppService(IUnitOfWorkManager unitOfWorkManager, IBusinessRepository businessRepository,IRepository<BusinessDetail, long> businessDetailRepository)
        {
            _businessDetailRepository = businessDetailRepository;
            _businessRepository = businessRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ParkChargesDto> GetParkChargesDayReport(ParkChargeInput input) 
        {
            List<ParkChargesDto> parkchargesDaylist = new List<ParkChargesDto>();
            parkchargesDaylist = _businessRepository.GetMoneyTotalGroupbyParkEchar(input);
            return parkchargesDaylist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ParkChargesDto> GetBerthsecChargesDayReportEchar(ParkChargeInput input)
        {
            List<ParkChargesDto> parkchargesDaylist = new List<ParkChargesDto>();
            parkchargesDaylist = _businessRepository.GetMoneyTotalGroupbyBerthsecEchar(input);
            return parkchargesDaylist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkChargesOutput GetParkChargesDayReport1(ParkChargeInput input)
        {
            //GetMoneyInput getmoneyinput = new GetMoneyInput();
            //getmoneyinput.BeginTime = input.begindt;
            //getmoneyinput.EndTime = input.enddt;
          
            List<ParkChargesDto> parkchargesDaylist = new List<ParkChargesDto>();
            //parkchargesDaylist = _businessRepository.GetMoneyTotalGroupbyPark(input);
            return _businessRepository.GetMoneyTotalGroupbyPark(input);
           
        }

        /// <summary>
        /// 泊位段报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkChargesOutput GetBerthsecChargesDayReport(ParkChargeInput input)
        {
            List<ParkChargesDto> parkchargesDaylist = new List<ParkChargesDto>();
            return _businessRepository.GetMoneyTotalGroupbyBerthsec(input);
        }

        /// <summary>
        /// 欠费报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkChargesOutput GetBerthescOwnOutput(ParkChargeInput input)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete);
            return _businessRepository.GetMonthOwnBerthsec(input);
        }

        /// <summary>
        /// 欠费报表详细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ParkChargesDto> OwnReportDetail(ParkChargeInput input)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete);
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            return _businessRepository.GetOwnReportDetail(input);
        }
    }
}
