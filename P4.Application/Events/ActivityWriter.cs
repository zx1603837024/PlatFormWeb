using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using P4.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityWriter : IEventHandler<EntityChangedEventData<User>>, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<User> eventData)
        {
            var temp = eventData.EventTime;
        }
    }
}
