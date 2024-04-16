using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.Businesses.Dtos;
using Abp.Domain.Repositories;
using Abp.Auditing;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    public class PictureStoreAppService : ApplicationService, IPictureStoreAppService
    {

        #region Var
        private readonly  IRepository<PictureStore> _pictureStoreRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureStoreRepository"></param>
        public PictureStoreAppService(IRepository<PictureStore> pictureStoreRepository)
        {
            _pictureStoreRepository = pictureStoreRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BusinessDetailId"></param>
        /// <returns></returns>
        public PicMongoDto GetPicture(int BusinessDetailId)
        {
            PicMongoDto entity = new PicMongoDto();
            var model = _pictureStoreRepository.FirstOrDefault(entry => entry.BusinessDetailId == BusinessDetailId);
            if (model != default(PictureStore))
            {
                entity.Image = model.Image;
                entity.FileSavePath = model.FileSavePath;
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        [DisableAuditing]
        public void InsertPicture(PicMongoDto model)
        {
            PictureStore entity = new PictureStore();
            entity.BusinessDetailGuid = model.BusinessDetailGuid;
            entity.BusinessDetailId = model.BusinessDetailId;
            entity.Image = model.Image;
            entity.PicType = model.PicType;
            _pictureStoreRepository.Insert(entity);
        }
    }
}
