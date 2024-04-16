using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.WeixinPushMsg.Dtos;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace P4.WeixinPushMsg
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinHistoryMsgAppService : ApplicationService, IWeixinHistoryMsgAppService
    {
        #region Var
        private readonly IRepository<Weixin.WeixinPushMsg> _abpweixinPushMsgRepository;
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpweixinPushMsgRepository"></param>
        public WeixinHistoryMsgAppService(IRepository<Weixin.WeixinPushMsg> abpweixinPushMsgRepository)
        {
            _abpweixinPushMsgRepository = abpweixinPushMsgRepository;
        }

        /// <summary>
        /// 微信历史消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWeixinPushMsgOutput GetAllWeixinPushMsgList(SearchWeixinPushMsgInput input)
        {
            int records = _abpweixinPushMsgRepository.GetAll().Filters(input).ToList().Count;
            var model = _abpweixinPushMsgRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinPushMsgDto>>();
                return new GetAllWeixinPushMsgOutput()
                {
                    rows = _abpweixinPushMsgRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<WeixinPushMsgDto>>(),
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
                };
            }
        }
    }

