using System.Linq;
using P4.PrintAdvertisement.Dtos;
using Abp.Domain.Repositories;
using P4.Equipments;

using Abp.Linq.Extensions;
using Abp.AutoMapper;
using System.Collections.Generic;
using System;

namespace P4.PrintAdvertisement
{
    /// <summary>
    /// 
    /// </summary>
    public class PrintAdAppService : IPrintAdAppService
    {
        #region Var
        private readonly IRepository<PrintAd> _printAdRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printAdRepository"></param>
        public PrintAdAppService(IRepository<PrintAd> printAdRepository)
        {
            _printAdRepository = printAdRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllPrintAdsOutput GetAllPrintAdsList(PrintAdInput input)
        {
            var records = _printAdRepository.GetAll().Filters(input).Count();
            return new GetAllPrintAdsOutput(){
                rows = _printAdRepository.GetAll().Filters(input).ToList().MapTo<List<PrintAdDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool DeletePrintAd(CreatOrUpdatePrintAdInput input)
        {
            _printAdRepository.Delete(input.Id);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool UpdatePrintAd(CreatOrUpdatePrintAdInput input)
        {
            _printAdRepository.Update(input.MapTo<PrintAd>());
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool InsertPrintAd(CreatOrUpdatePrintAdInput input)
        {
            _printAdRepository.Insert(input.MapTo<PrintAd>());
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PrintAdDto> GetAllPrintAdsList()
        {
            return _printAdRepository.GetAllList(entity => entity.IsActive == true && entity.BeginTime < DateTime.Now && entity.EndTime > DateTime.Now).MapTo<List<PrintAdDto>>();
        }
    }
}
