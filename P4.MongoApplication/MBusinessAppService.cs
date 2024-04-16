using Abp.Application.Services;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MongoApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class MBusinessAppService : ApplicationService, IMBusinessAppService
    {

        private readonly IPictureRepository _pictureRepository;

        public MBusinessAppService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        [UnitOfWork(false)]
        public void Create()
        {
            _pictureRepository.Insert(new MongoCore.MBusinessDetailPicture());
        }
    }
}
