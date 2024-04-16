using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using P4.AuditLogs.Dto;
using Abp.Application.Services;
using Abp.Linq.Extensions;

namespace P4.AuditLogs
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditLogAppService : ApplicationService, IAuditLogAppService
    {
        #region  Var
        private readonly IRepository<AuditLog, long> _abpAuditLogRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpAuditLogRepository"></param>
        public AuditLogAppService(IRepository<AuditLog, long> abpAuditLogRepository)
        {
            _abpAuditLogRepository = abpAuditLogRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ListResultOutput<Dto.AuditLogDto> GetAll(long Id)
        {
           
            return new ListResultOutput<Dto.AuditLogDto>()
            {
                Items = _abpAuditLogRepository.GetAll().OrderByDescending(model => model.ExecutionTime).Take(20).MapTo<List<AuditLogDto>>().ToList()
            };
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllAuditLogOutput GetAll(SearchAuditLogInput input)
        {
            int records = _abpAuditLogRepository.GetAll().Filters(input).Count();
            return new GetAllAuditLogOutput()
            {
                rows = _abpAuditLogRepository.GetAll().Filters(input)
                .Orders(input)
                .PageBy(input).ToList().MapTo<List<AuditLogDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
