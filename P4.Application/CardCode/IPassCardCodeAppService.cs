using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;


using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using P4.CardCode.Dtos;
using P4.Card;

namespace P4.CardCode
{
    /// <summary>
    /// 
    /// </summary>
    public class IPassCardCodeAppService : ApplicationService, IIPassCardCodeAppService
    {
        #region Var
        private readonly IRepository<IpassCardCode> _abpIPassCardCodeRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpIPassCardCodeRepository"></param>
        public IPassCardCodeAppService(IRepository<IpassCardCode> abpIPassCardCodeRepository)
        {
            _abpIPassCardCodeRepository = abpIPassCardCodeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteIPassCardCode(CreateOrUpdateIPassCardCodeInput input)
        {
            _abpIPassCardCodeRepository.Delete(input.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GetAlllPassCardCodeOutput GetIPassCardCodeList()
        {
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAlllPassCardCodeOutput GetIPassCardCodeList(GetAllIPassCardCodeInput input)
        {
                 int records = _abpIPassCardCodeRepository.GetAll().Filters(input).ToList().Count;
            return new GetAlllPassCardCodeOutput()
            {
                rows = _abpIPassCardCodeRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<IPassCardCodeDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        public string InsertIPassCardCode(CreateOrUpdateIPassCardCodeInput input)
        {
            if (_abpIPassCardCodeRepository.FirstOrDefault(dic => dic.Code == input.Code && dic.Id != input.Id) != default(IpassCardCode))
            {
                return input.Code + "已经存在!";
            }
            IpassCardCode entity = new IpassCardCode();
            
            entity.Code = input.Code;
            entity.Value = input.Value;
            entity.IsActive = true;
            _abpIPassCardCodeRepository.Insert(entity);
            return null;
        }

        public string ModifyIPassCardCode(CreateOrUpdateIPassCardCodeInput input)
        {
            var entity = _abpIPassCardCodeRepository.Load(input.Id);
            
            if (_abpIPassCardCodeRepository.FirstOrDefault(dic => dic.Code == input.Code && dic.Id != input.Id) != default(IpassCardCode))
            {
                return input.Code + "已经存在!";
            }
            

            entity.Code = input.Code;
            entity.Value = input.Value;
            entity.IsActive = input.IsActive;
            _abpIPassCardCodeRepository.Update(entity);
            return null;
        }
    }
}