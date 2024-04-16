using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using P4.Dictionarys;

namespace P4.Events
{
    /// <summary>
    /// 字典表事件总线
    /// </summary>
    public class DictionaryActivityWriter : IEventHandler<EntityChangedEventData<DictionaryValue>>, ITransientDependency
    {

        ICacheManager _cacheManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        public DictionaryActivityWriter(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<DictionaryValue> eventData)
        {
            _cacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheDictionaryValue, eventData.Entity.TenantId ?? 0)).Remove(eventData.Entity.TypeCode);
        }
    }
}
