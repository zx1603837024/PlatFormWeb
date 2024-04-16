using Abp.Domain.Repositories;
using P4.VideoEquipFaultsNs.Dtos;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using P4.PayLevelSetting;
using P4.PayLevelSettingsNs.Dtos;

namespace P4.PayLevelSettingsNs
{
    public interface IPayLevelRepository : IRepository<PayLevelSettings, int>
    {
        PayLevelOutput GetAllVideoEqFaultsList(PayLevelInput input, IAbpSession abpsession);
        void Update1(CreateOrUpdatePayLevel input, IAbpSession abpsession);
        void Add1(CreateOrUpdatePayLevel input, IAbpSession abpsession);
    }
}
