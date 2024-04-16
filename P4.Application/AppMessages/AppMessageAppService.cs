using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.AppMessages.Dtos;
using Abp.AutoMapper;

namespace P4.AppMessages
{
    /// <summary>
    /// 
    /// </summary>
    public class AppMessageAppService : ApplicationService, IAppMessageAppService
    {
        #region Var
        private readonly IRepository<AppMessage, long> _abpAppMessageRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpAppMessageRepository"></param>
        public AppMessageAppService(IRepository<AppMessage, long> abpAppMessageRepository)
        {
            _abpAppMessageRepository = abpAppMessageRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="page"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public List<AppMessageDto> GetAllMessages(string p, int v, int page, string mobile)
        {
            return _abpAppMessageRepository.GetAll().OrderByDescending(entry=> entry.Id).Take(page * 10).Skip((page - 1) * 10).ToList().MapTo<List<AppMessageDto>>();
        }
    }
}
