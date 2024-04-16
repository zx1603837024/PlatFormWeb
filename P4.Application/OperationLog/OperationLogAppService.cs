using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.DataLog;
using Abp.Domain.Repositories;

namespace P4.OperationLog
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperationLogAppService : P4AppServiceBase, IOperationLogAppService
    {
        #region Var
        private readonly IRepository<Abp.DataLog.OperationLog, long> _operationLogAppService;
        private readonly IOperationLogRepository _operationLogRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationLogAppService"></param>
        /// <param name="operationLogRepository"></param>
        public OperationLogAppService(IRepository<Abp.DataLog.OperationLog, long> operationLogAppService, IOperationLogRepository operationLogRepository)
        {
            _operationLogAppService = operationLogAppService;
            _operationLogRepository = operationLogRepository;
        }


        /// <summary>
        /// 操作权限列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllOperationLogOutput GetAllOperationLogList(Dtos.OperationLogInput input)
        {
            List<Dtos.OperationLogDto> list = new List<Dtos.OperationLogDto>();
            var entity = _operationLogRepository.GetAllOperationLogList(input);
            foreach (var e in entity.rows)
            {
                e.Name = L(e.Name);
                list.Add(e);
            }
            entity.rows = list;

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Dtos.GetAllOperationLogOutput GetOperationLogToTopList(int count)
        {
            List<Dtos.OperationLogDto> list = new List<Dtos.OperationLogDto>();
            var entity = _operationLogRepository.GetOperationLogToTopList(count);
            foreach (var e in entity.rows)
            {
                e.Name = L(e.Name);
                list.Add(e);
            }
            entity.rows = list;
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteOperationLog(long Id)
        {
            _operationLogAppService.Delete(Id);
        }
    }
}
