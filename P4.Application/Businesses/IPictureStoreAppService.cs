using Abp.Application.Services;
using P4.Businesses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPictureStoreAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void InsertPicture(PicMongoDto model);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PicMongoDto GetPicture(int BusinessDetailId);
    }
}
