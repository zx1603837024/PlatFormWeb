using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using P4.DataLogs.Dtos;

namespace P4.DataLogs
{
    /// <summary>
    /// 
    /// </summary>
    public class DataLogsAppService : P4AppServiceBase, IDataLogsAppService
    {
        #region Var
        private readonly IRepository<Abp.DataLog.Datalog, long> _datalogAppService;
        private readonly IRepository<Abp.DataLog.DataLogItem, long> _datalogItemAppService;
        private readonly IDataLogsRepository _dataLogsRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datalogAppService"></param>
        /// <param name="dataLogsRepository"></param>
        /// <param name="datalogItemAppService"></param>
        public DataLogsAppService(IRepository<Abp.DataLog.Datalog, long> datalogAppService, IDataLogsRepository dataLogsRepository, IRepository<Abp.DataLog.DataLogItem, long> datalogItemAppService)
        {
            _datalogAppService = datalogAppService;
            _dataLogsRepository = dataLogsRepository;
            _datalogItemAppService = datalogItemAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Dtos.GetAllDataLogsOutput GetTopDataLog(int count)
        {
            List<Dtos.DataLogDto> list = new List<Dtos.DataLogDto>();
            var entity = _dataLogsRepository.GetTopDataLog(count);
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
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllDataLogsOutput GetAllDataLogByPage(Dtos.DataLogInput input)
        {
            List<Dtos.DataLogDto> list = new List<Dtos.DataLogDto>();
            var entity = _dataLogsRepository.GetAllDataLogByPage(input);
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
        public void DeleteDatalogInfo(long Id)
        {
            _datalogAppService.Delete(Id);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllDataLogItemsOutput GetAllDataLogItemById(DataLogItemInput input)
        {
            return new GetAllDataLogItemsOutput()
            {
                rows = _datalogItemAppService.GetAll().Where(entity => entity.DataLogId == input.DataLogId).Orders(input).ToList().MapTo<List<DataLogItemDto>>(),
                records = int.MaxValue,
                total = int.MaxValue
            };
        }
    }
}
