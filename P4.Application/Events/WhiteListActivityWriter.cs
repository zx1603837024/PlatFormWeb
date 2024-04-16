using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using P4.WhiteLists;

namespace P4.Events
{
    /// <summary>
    /// 白名单事件总线
    /// </summary>
    public class WhiteListActivityWriter : IEventHandler<EntityChangedEventData<WhiteList>>, ITransientDependency
    {
        ICacheManager _cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        public WhiteListActivityWriter(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<WhiteList> eventData)
        {
            _cacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, eventData.Entity.CompanyId)).Remove(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, eventData.Entity.CompanyId));
            //_cacheManager.GetCache(Abp.Configuration.SettingManager.CacheWhiteCarValue).Remove(Abp.Configuration.SettingManager.CacheWhiteCarValue);
        }
    }
}
