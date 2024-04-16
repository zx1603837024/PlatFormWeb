using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services;
using P4.Configuration;
using System.IO;
using P4.Messages.Dto;
using Abp.Linq.Extensions;
using Abp.Authorization.SignalRMessage;

namespace P4.Messages
{

    /// <summary>
    /// 
    /// </summary>
    public class MessageAppService : ApplicationService, IMessageAppService
    {
        #region Var
        private readonly IRepository<Message, int> _abpMessageRepository;
        private readonly IRepository<SignalRMessageType, int> _abpSignalRMessageTypeRepository;
        private readonly IRepository<UserSubscribeMessageSetting, long> _abpSubscribeMessageSettingRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpMessageRepository"></param>
        /// <param name="abpSignalRMessageTypeRepository"></param>
        /// <param name="abpSubscribeMessageSettingRepository"></param>
        public MessageAppService(IRepository<Message, int> abpMessageRepository, IRepository<SignalRMessageType, int> abpSignalRMessageTypeRepository, IRepository<UserSubscribeMessageSetting, long> abpSubscribeMessageSettingRepository)
        {
            _abpMessageRepository = abpMessageRepository;
            _abpSignalRMessageTypeRepository = abpSignalRMessageTypeRepository;
            _abpSubscribeMessageSettingRepository = abpSubscribeMessageSettingRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Abp.Application.Services.Dto.ListResultOutput<Dto.MessageDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<Dto.MessageDto>()
            {
                Items = _abpMessageRepository.GetAll().Where(message => message.IsRead == false && message.ReviceUserId == AbpSession.UserId.Value).OrderByDescending(p => p.CreationTime).Take(5).ToList().MapTo<List<Dto.MessageDto>>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dto.GetAllMessageOutput GetAll(Dto.MessageInput input)
        {
            int records = _abpMessageRepository.Count(entity => entity.ReviceUserId == AbpSession.UserId.Value);
            return new Dto.GetAllMessageOutput()
            {
                rows = _abpMessageRepository.GetAll().Where(entity => entity.ReviceUserId == AbpSession.UserId.Value).OrderBy(p => p.ReadTime).Orders(input).PageBy(input).ToList().MapTo<List<MessageDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()
        {
            return _abpMessageRepository.Count(message => message.IsRead == false && message.ReviceUserId == AbpSession.UserId.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSignalRMessageTypeOutput GetSignalRMessageTypeList(SignalRMessageTypeInput input)
        {
            var usersignalr = _abpSubscribeMessageSettingRepository.GetAllList(model => model.IsGranted && model.UserId == AbpSession.UserId.Value);
            var allsignalr = _abpSignalRMessageTypeRepository.GetAll().MapTo<List<SignalRMessageTypeDto>>();
            List<SignalRMessageTypeDto> signalr = new List<SignalRMessageTypeDto>();
            List<SignalRMessageTypeDto> selectSignalr = new List<SignalRMessageTypeDto>();
            foreach (var entity in allsignalr)
            {
                if (usersignalr.FirstOrDefault(entry => entry.TypeGroup == entity.TypeGroup) != null)
                {
                    selectSignalr.Add(entity);
                }
                else
                {
                    signalr.Add(entity);
                }
            }
            return new GetAllSignalRMessageTypeOutput() { rows = signalr, selectrows = selectSignalr };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public bool ReadMessage(List<int> Ids)
        {
            foreach (var v in Ids)
            {
                var entity = _abpMessageRepository.Load(v);
                entity.IsRead = true;
                entity.ReadTime = DateTime.Now;
                _abpMessageRepository.Update(entity);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void SaveUserSubscribeMessageSetting(string val)
        {
            var usersignalr = _abpSubscribeMessageSettingRepository.GetAllList(m => m.UserId == AbpSession.UserId.Value);
            var allsignalr = _abpSignalRMessageTypeRepository.GetAll().MapTo<List<SignalRMessageTypeDto>>();
            foreach (var signalr in allsignalr)
            {
                if (val.Contains(signalr.TypeGroup))
                {
                    var model = usersignalr.FirstOrDefault(entity => entity.TypeGroup == signalr.TypeGroup);
                    if (model != null && !model.IsGranted)
                    {
                        model.IsGranted = true;
                        _abpSubscribeMessageSettingRepository.Update(model);
                    }
                    else if (model == null)
                    {
                        _abpSubscribeMessageSettingRepository.Insert(new UserSubscribeMessageSetting()
                        {
                            TypeCode = signalr.TypeCode,
                            TypeGroup = signalr.TypeGroup,
                            UserId = AbpSession.UserId.Value,
                            IsGranted = true
                        });
                    }
                }
                else
                {
                    var model = usersignalr.FirstOrDefault(entity => entity.TypeGroup == signalr.TypeGroup && entity.IsGranted);
                    if (model != null)
                    {
                        model.IsGranted = false;
                        _abpSubscribeMessageSettingRepository.Update(model);
                    }
                }
            }
        }
    }
}
