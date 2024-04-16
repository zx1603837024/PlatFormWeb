using Abp.Application.Services;
using P4.Card.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Card
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIPassBlackCardAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<IPassBlackCardDto> GetIPassBlackCardList();
    }
}
