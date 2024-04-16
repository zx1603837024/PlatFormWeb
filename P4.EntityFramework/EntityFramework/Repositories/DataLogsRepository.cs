using Abp.EntityFramework;
using P4.DataLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Dictionarys;
using Abp.Linq.Extensions;
using P4.DataLogs.Dtos;

namespace P4.EntityFramework.Repositories
{
    public class DataLogsRepository : P4RepositoryBase<Abp.DataLog.Datalog, long>, IDataLogsRepository
    {

        public DataLogsRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public DataLogs.Dtos.GetAllDataLogsOutput GetTopDataLog(int count)
        {
            var query = Table.Select(c => new DataLogs.Dtos.DataLogDto
                {
                    Id = c.Id,
                    CreationTime = c.CreationTime,
                    Name = c.Name,
                    EntityKey = c.EntityKey,
                    OperateType = Context.Set<Dictionarys.DictionaryValue>()
                    .FirstOrDefault(entity => entity.TypeCode == P4Consts.OperationType && entity.ValueCode == ((int)c.OperateType).ToString()).ValueData,
                    CreationUserName = Context.Set<Users.User>().FirstOrDefault(user => user.Id == c.CreatorUserId).Surname
                }).OrderByDescending(entity => entity.CreationTime).Take(count);
            return new DataLogs.Dtos.GetAllDataLogsOutput()
            {
                rows = query.ToList()
            };
        }

        /// <summary>
        /// 获取数据日志列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DataLogs.Dtos.GetAllDataLogsOutput GetAllDataLogByPage(DataLogs.Dtos.DataLogInput input)
        {
            //DateTime Dt = DateTime.Now;
            string Datetime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            DateTime DD = Convert.ToDateTime(Datetime);
            var query = Table.Select(c => new DataLogs.Dtos.DataLogDto
                {
                    Id = c.Id,
                    CreationTime = c.CreationTime,
                    Name = c.Name,
                    CreatorUserId = c.CreatorUserId,
                    EntityKey = c.EntityKey,
                    OperateType = Context.Set<Dictionarys.DictionaryValue>()
                    .FirstOrDefault(entity => entity.TypeCode == P4Consts.OperationType && entity.ValueCode == ((int)c.OperateType).ToString()).ValueData,
                    CreationUserName = Context.Set<Users.User>().FirstOrDefault(user => user.Id == c.CreatorUserId).Surname
                }).Where(c => c.CreationTime > DD).Orders(input).Filters(input);

            var records = query.Count();
            List<DataLogDto> haoba = query.PageBy(input).ToList();
            return new GetAllDataLogsOutput()
            {
                rows = query.PageBy(input).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
