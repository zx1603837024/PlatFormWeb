using System;
using Abp.Domain.Repositories;
using P4.SmsManagements.Dtos;
using P4.Smss;
using System.Linq;
using System.Collections.Generic;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace P4.SmsManagements
{
    /// <summary>
    /// 短信模板管理
    /// </summary>
    public class SmsModelAppService : ISmsModelAppService
    {
        #region Var
        private readonly IRepository<SmsModel> _abpSmsModelRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public SmsModelAppService(IRepository<SmsModel> abpSmsModelRepository)
        {
            _abpSmsModelRepository = abpSmsModelRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Delete(CreateOrUpdateDtoInput input)
        {
            _abpSmsModelRepository.Delete(input.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSmsModelOutput GetAllSmsModel(SearchSmsModelInput input)
        {
            int records = _abpSmsModelRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllSmsModelOutput()
            {
                rows = _abpSmsModelRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<SmsModelDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public SmsModelDto GetSmsModelByType(string modelType)
        {
            return _abpSmsModelRepository.FirstOrDefault(entity => entity.ModelType == modelType).MapTo<SmsModelDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Insert(CreateOrUpdateDtoInput input)
        {
            _abpSmsModelRepository.Insert(input.MapTo<SmsModel>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Update(CreateOrUpdateDtoInput input)
        {

            var model = _abpSmsModelRepository.Load(input.Id);

            if (!string.IsNullOrWhiteSpace(input.ModelType))
            {
                model.ModelType = input.ModelType;
            }

            model.Url = input.Url;
            model.SmsContext = input.SmsContext;
            model.UserName = input.UserName;
            model.Password = input.Password;

            _abpSmsModelRepository.Update(model);
        }
    }
}
