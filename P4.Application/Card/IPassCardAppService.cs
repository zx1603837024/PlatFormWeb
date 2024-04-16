/********************************************************************************
** auth： 黎通
** date： 2017/07/26 10:00:50
** desc： 尚未编写描述
** Ver.:  V1.0.0
*********************************************************************************/

using System;
using Abp.Application.Services;
using P4.Card.Dtos;
using Abp.Domain.Repositories;
using Abp.Authorization;
using Abp.Linq.Extensions;
using System.Linq;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace P4.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class IPassCardAppService : ApplicationService, IIPassCardAppService
    {
        #region Var
        private readonly IRepository<IPassCard> _abpIPassCardRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpIPassCardRepository"></param>
        public IPassCardAppService(IRepository<IPassCard> abpIPassCardRepository) {
            _abpIPassCardRepository = abpIPassCardRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool FreshSettlement(int Id)
        {
            var model = _abpIPassCardRepository.Load(Id);
            model.IsActive = false;
            model.Status = 0;
            model.SettlementTime = null;
            model.LineNumber = null;
            model.PackageName = null;
            model.ReturnResult = "重新结算";
            _abpIPassCardRepository.Update(model);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllIPassCardOutput GetList(GetAllIPassCardInput input)
        {
            int records = _abpIPassCardRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllIPassCardOutput()
            {
                rows = _abpIPassCardRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<IPassCardDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="Package"></param>
        /// <param name="Money"></param>
        /// <param name="PayDate"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool InsertIPassCard(Guid guid, string Package, int Money, DateTime PayDate)
        {
            if (_abpIPassCardRepository.FirstOrDefault(entity => entity.guid == guid) != null)
                return true;
            IPassCard model = new IPassCard();
            model.guid = guid;
            model.Package = Package;
            model.Status = 0;
            model.IsActive = false;
            model.Money = Money;
            model.PayDate = PayDate;
            _abpIPassCardRepository.Insert(model);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="Status"></param>
        /// <param name="ReturnResult"></param>
        /// <returns></returns>
        public bool Settlement(Guid guid, int Status, string ReturnResult)
        {
            var model = _abpIPassCardRepository.FirstOrDefault(entity => entity.guid == guid);
            model.Status = Status;
            model.ReturnResult = ReturnResult;
            _abpIPassCardRepository.Update(model);
            return true;
        }
    }
}
