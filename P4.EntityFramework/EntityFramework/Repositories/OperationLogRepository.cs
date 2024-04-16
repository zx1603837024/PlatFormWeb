using Abp.EntityFramework;
using P4.OperationLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.OperationLog.Dtos;
using Abp.Localization;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperationLogRepository : P4RepositoryBase<Abp.DataLog.OperationLog, long>, IOperationLogRepository
    {
        public OperationLogRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public GetAllOperationLogOutput GetAllOperationLogList(OperationLog.Dtos.OperationLogInput input)
        {
            var query = Table.Select(c => new OperationLogDto
            {
                Id = c.Id,
                CreationTime = c.CreationTime,
                Name = c.Name,
                EntityKey = c.EntityKey,
                OperatingType = Context.Set<Dictionarys.DictionaryValue>()
                .FirstOrDefault(entity => entity.TypeCode == P4Consts.OperationType && entity.ValueCode == ((int)c.OperateType).ToString()).ValueData,
                CreationUserName = Context.Set<Users.User>().FirstOrDefault(user => user.Id == c.CreatorUserId).Surname
            }).Orders(input).Filters(input);
            var records = query.Count();
            return new GetAllOperationLogOutput()
            {
                rows = query.PageBy(input).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 操作数据top
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public OperationLog.Dtos.GetAllOperationLogOutput GetOperationLogToTopList(int count)
        {
            var query = Table.Select(c => new OperationLogDto
            {
                Id = c.Id,
                CreationTime = c.CreationTime,
                Name = c.Name,
                EntityKey = c.EntityKey,
                OperatingType = Context.Set<Dictionarys.DictionaryValue>()
                .FirstOrDefault(entity => entity.TypeCode == P4Consts.OperationType && entity.ValueCode == ((int)c.OperateType).ToString()).ValueData,
                CreationUserName = Context.Set<Users.User>().FirstOrDefault(user => user.Id == c.CreatorUserId).Surname
            }).OrderByDescending(entity => entity.Id).Take(count);
            return new GetAllOperationLogOutput()
            {
                rows = query.ToList()
            };
        }
    }
}
