using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using P4.BlackLists;

namespace P4.Events
{
    /// <summary>
    /// 黑名单事件总线
    /// </summary>
    public class BlackListActivityWriter : IEventHandler<EntityChangedEventData<BlackList>>, ITransientDependency
    {
        ICacheManager _cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        public BlackListActivityWriter(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<BlackList> eventData)
        {
            _cacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheBlackCarValue, eventData.Entity.CompanyId)).Remove(string.Format(Abp.Configuration.SettingManager.CacheBlackCarValue, eventData.Entity.CompanyId));       
        }
    }
}
