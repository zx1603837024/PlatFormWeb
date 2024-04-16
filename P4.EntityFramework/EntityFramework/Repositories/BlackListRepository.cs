using Abp.EntityFramework;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using P4.BlackLists;
using P4.BlackLists.Dtos;
using P4.Dictionarys;
using System.Linq;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 黑名单
    /// </summary>
    public class BlackListRepository : P4RepositoryBase<BlackList>, IBlackListRepository
    {
        private readonly ICacheManager _abpCacheManager;//缓存

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextProvider"></param>
        /// <param name="abpCacheManager"></param>
        public BlackListRepository(IDbContextProvider<P4DbContext> dbContextProvider, ICacheManager abpCacheManager)
            : base(dbContextProvider)
        {
            _abpCacheManager = abpCacheManager;
        }

        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBlackListsOutput GetBlackListAll(BlackListsInput input)
        {
            int records = Table.Filters(input).Count();

            var query = Table.Select(c => new BlackListsDto
            {
                Id = c.Id,
                CarOwner = c.CarOwner,
                CompanyId = c.CompanyId,
                CompanyName = Context.Set<Companys.OperatorsCompany>().FirstOrDefault(entity => entity.Id == c.CompanyId).CompanyName,
                RelateNumber = c.RelateNumber,
                IsActive = c.IsActive,
                IdNumber = c.IdNumber,
                Remarks = c.Remarks,
                VehicleType = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.VehicleType && dictype.TypeCode == P4Consts.VehicleType).ValueData
            }).Orders(input).Filters(input).PageBy(input);

            return new GetAllBlackListsOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
