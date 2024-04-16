using Abp.Application.Services;
using P4.PayLevelSettingsNs.Dtos;

namespace P4.PayLevelSettingsNs
{
    public interface IPayLevelSettingAppService : IApplicationService
    {
        PayLevelOutput GetPayLevels(PayLevelInput input);

        void Delete(int Id);
        void Update1(CreateOrUpdatePayLevel input);
        void Add1(CreateOrUpdatePayLevel input);
    }
}
