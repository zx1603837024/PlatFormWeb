using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using P4.MonthlyCars;

namespace P4.Events
{
    /// <summary>
    /// 包月车辆事件总线
    /// </summary>
    public class MonthCarActivityWriter : IEventHandler<EntityChangedEventData<MonthlyCar>>, ITransientDependency
    {
        ICacheManager _cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        public MonthCarActivityWriter(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 移除 缓存
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<MonthlyCar> eventData)
        {
            _cacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, eventData.Entity.CompanyId)).Remove(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, eventData.Entity.CompanyId));
        }
    }
}

