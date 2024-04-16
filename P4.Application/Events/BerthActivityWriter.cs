using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using P4.Berths;

namespace P4.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class BerthActivityWriter : IEventHandler<EntityChangedEventData<Berths.Berth>>, ITransientDependency
    {
        #region Var
        private readonly IRepository<Berthsecs.Berthsec> _berthsecRepository;
        private readonly IRepository<Berth, long> _abpBerthRepository;
        private readonly IRepository<Parks.Park> _parkRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecRepository"></param>
        /// <param name="abpBerthRepository"></param>
        /// <param name="parkRepository"></param>
        public BerthActivityWriter(IRepository<Berthsecs.Berthsec> berthsecRepository, IRepository<Berth, long> abpBerthRepository, IRepository<Parks.Park> parkRepository)
        {
            _berthsecRepository = berthsecRepository;
            _abpBerthRepository = abpBerthRepository;
            _parkRepository = parkRepository;
        }

        /// <summary>
        /// 事件更改求出车场管理的泊位数
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<Berths.Berth> eventData)
        {
            var parkidcount =  _abpBerthRepository.Count(entity => entity.IsActive == true && entity.ParkId == eventData.Entity.ParkId);
            var berthsecisactivecount =  _abpBerthRepository.Count(entity => entity.IsActive == true && entity.BerthsecId == eventData.Entity.BerthsecId);
            var berthseccount =  _abpBerthRepository.Count(entity => entity.BerthsecId == eventData.Entity.BerthsecId);

            var entrypark = _parkRepository.Load(eventData.Entity.ParkId);
            entrypark.BerthCount = parkidcount;
            _parkRepository.Update(entrypark);
           
            var entry = _berthsecRepository.Load(eventData.Entity.BerthsecId);
            entry.BerthCount = berthsecisactivecount.ToString() + "/" + berthseccount.ToString();
            _berthsecRepository.Update(entry);
        }
    }
}
