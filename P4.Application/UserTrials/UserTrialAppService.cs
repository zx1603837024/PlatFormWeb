using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Net.Mail;
using P4.UserTrials.Dtos;
using Abp.Linq.Extensions;
using Abp.Configuration;

namespace P4.UserTrials
{
    /// <summary>
    /// 
    /// </summary>
    public class UserTrialAppService : ApplicationService, IUserTrialAppService
    {
        #region Var
        private readonly IRepository<UserTrial> _userTrialAppService;
        private readonly IEmailSender _emailSender;
        private readonly ISettingStore _settingStore;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTrialAppService"></param>
        /// <param name="emailSender"></param>
        /// <param name="settingStore"></param>
        public UserTrialAppService(IRepository<UserTrial> userTrialAppService, IEmailSender emailSender, ISettingStore settingStore)
        {
            _userTrialAppService = userTrialAppService;
            _emailSender = emailSender;
            _settingStore = settingStore;
        }

        /// <summary>
        /// 申请试用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> SaveUserTrial(UserTrialInput input)
        {
            UserTrial model = new UserTrial()
            {
                Email = input.Email,
                CompanyName = input.CompanyName,
                TrueName = input.TrueName,
                Telephone = input.Telephone,
                Address = input.Address,
                CreationTime = DateTime.Now,
                IsActive = false
            };
            var settingAdm = await _settingStore.GetAllListAsync(null, null);
            var email = settingAdm.FirstOrDefault(entity => entity.Name == "EmailAdress").Value;//试用邮箱地址
            List<string> list = new List<string>(email.Split(';'));
            foreach (var item in list)
            {
                await _emailSender.SendAsync(item, "申请试用综合停车管理与交通诱导系统云平台","    客户"+ input.TrueName + "通过公司官网申请试用综合停车管理与交通诱导系统云平台，请尽快处理，谢谢！\r\n    联系电话:" + input.Telephone + "\r\n    所属公司:" + input.CompanyName + "\r\n    电子邮箱:" + input.Email + "\r\n    客户姓名:" + input.TrueName, false);
            }
            await _emailSender.SendAsync(input.Email, "申请试用成功", input.TrueName + "您好：" + "\r\n    您申请试用的资料我们已经收到，我们将尽快与您联系，感谢您的信赖！\r\n    祝您生活愉快！\r\n\r\n\r\n\r\n              南京方新智能科技有限公司 销售部\r\n              " + DateTime.Now.ToLongDateString().ToString(), false);
            var id = await _userTrialAppService.InsertAndGetIdAsync(model);
            return id;
        }

        /// <summary>
        /// 申请账号列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllUseTrialOutput GetAllUserTrials(Dtos.SearchUseTrialInput input)
        {
            int records = _userTrialAppService.Count();
            return new Dtos.GetAllUseTrialOutput()
            {
                rows = _userTrialAppService.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<UserTrialDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}