using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.PayLevelSetting;
using P4.PayLevelSettingsNs.Dtos;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.PayLevelSettingsNs
{
    public class PayLevelSettingAppService : ApplicationService, IPayLevelSettingAppService
    {
        #region Var
        private readonly IRepository<PayLevelSettings, int> _abpVideoFaultrepository;
        private readonly IPayLevelRepository _VideoFaultrepository;
        #endregion

        public PayLevelSettingAppService(IRepository<PayLevelSettings, int> a, IPayLevelRepository b)

        {
            _abpVideoFaultrepository = a;
            _VideoFaultrepository = b;
        }
        public Dtos.PayLevelOutput GetPayLevels(Dtos.PayLevelInput input)
        {
            return _VideoFaultrepository.GetAllVideoEqFaultsList(input, AbpSession);
        }

        public void Delete(int Id)
        {
            _VideoFaultrepository.Delete(Id);
        }
        public void Update1(CreateOrUpdatePayLevel input)
        {
            _VideoFaultrepository.Update1(input, AbpSession);
        }
        public void Add1(CreateOrUpdatePayLevel input)
        {
            _VideoFaultrepository.Add1(input, AbpSession);
        }
    }
}
