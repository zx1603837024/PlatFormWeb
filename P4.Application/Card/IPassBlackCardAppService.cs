using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.Card.Dtos;
using Abp.Domain.Repositories;
using Abp.AutoMapper;

namespace P4.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class IPassBlackCardAppService : ApplicationService, IIPassBlackCardAppService
    {

        #region Var
        private readonly IRepository<IPassBlackCard> _abpIPassBlackCardRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpIPassBlackCardRepository"></param>
        public IPassBlackCardAppService(IRepository<IPassBlackCard> abpIPassBlackCardRepository)
        {
            _abpIPassBlackCardRepository = abpIPassBlackCardRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IPassBlackCardDto> GetIPassBlackCardList()
        {
            return _abpIPassBlackCardRepository.GetAllList(entity => entity.IsActive == true).MapTo<List<IPassBlackCardDto>>();
        }
    }
}
