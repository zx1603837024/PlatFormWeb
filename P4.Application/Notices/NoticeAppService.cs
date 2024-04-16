using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Configuration;
using P4.Configuration;
using Abp.Application.Services;
using P4.Notices.Dto;
using Abp.Linq.Extensions;

namespace P4.Notices
{
    /// <summary>
    /// 
    /// </summary>
    public class NoticeAppService : ApplicationService, INoticeAppService
    {
         #region Var
        private readonly IRepository<Notice, int> _abpNoticeRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpNoticeRepository"></param>
        public NoticeAppService(IRepository<Notice, int> abpNoticeRepository)
        {
            _abpNoticeRepository = abpNoticeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Abp.Application.Services.Dto.ListResultOutput<Dto.NoticeDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<Dto.NoticeDto>()
            {
                Items = _abpNoticeRepository.GetAll().Where(notice => notice.IsRead == false && notice.ReviceUserId == AbpSession.UserId.Value).OrderByDescending(p => p.CreationTime).Take(SettingManager.GetSettingValue<int>(MySettingProvider.DefaultRowSize)).ToList().MapTo<List<Dto.NoticeDto>>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dto.GetAllNoticeOutput GetAll(Dto.NoticeInput input)
        {
            int records = _abpNoticeRepository.Count(entity => entity.ReviceUserId == AbpSession.UserId.Value);
            return new Dto.GetAllNoticeOutput()
            {
                rows = _abpNoticeRepository.GetAll().Where(entity => entity.ReviceUserId == AbpSession.UserId.Value).OrderBy(p => p.ReadTime).PageBy(input).ToList().MapTo<List<NoticeDto>>(),
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
            return _abpNoticeRepository.Count(notice => notice.IsRead == false && notice.ReviceUserId == AbpSession.UserId.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public bool ReadNotice(List<int> Ids)
        {
            foreach (var v in Ids)
            {
                var entity = _abpNoticeRepository.Load(v);
                entity.IsRead = true;
                entity.ReadTime = DateTime.Now;
                _abpNoticeRepository.Update(entity);
            }
            return true;
        }
    }
}
