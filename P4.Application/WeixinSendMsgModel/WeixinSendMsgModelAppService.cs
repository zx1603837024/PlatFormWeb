using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.WeixinSendMsgModel.Dtos;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.Linq.Extensions;

namespace P4.WeixinSendMsgModel
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinSendMsgModelAppService : ApplicationService, IWeixinSendMsgModelAppService
    {
        #region Var
        private readonly IRepository<Weixin.WeixinSendMsgModel> _abpWeixinSendMsgModelRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpWeixinSendMsgModelRepository"></param>
        public WeixinSendMsgModelAppService(IRepository<Weixin.WeixinSendMsgModel> abpWeixinSendMsgModelRepository)
        {
            _abpWeixinSendMsgModelRepository = abpWeixinSendMsgModelRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Add(CreateOrUpdateWeixinSendMsgModelInput input)
        {
            input.IsActive = true;
            _abpWeixinSendMsgModelRepository.Insert(input.MapTo<P4.Weixin.WeixinSendMsgModel>());
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string Delete(int Id)
        {
            _abpWeixinSendMsgModelRepository.Delete(Id);
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinSendMsgModelOutput GetAll(SearchWeixinSendMsgModelInput input)
        {
            int records = _abpWeixinSendMsgModelRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllWeixinSendMsgModelOutput()
            {
                rows = _abpWeixinSendMsgModelRepository.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList().MapTo<List<WeixinSendMsgModelDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Update(CreateOrUpdateWeixinSendMsgModelInput input)
        {
            var model = _abpWeixinSendMsgModelRepository.Load(input.Id);
            model.Crux = input.Key;
            model.Msg = input.Msg;
            _abpWeixinSendMsgModelRepository.Update(model);
            return "";
        }
    }
}
