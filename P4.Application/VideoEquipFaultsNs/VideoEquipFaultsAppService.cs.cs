using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoEquipFaultsNs
{
    /// <summary>
    /// 实现接口
    /// </summary>
    public class VideoEquipFaultsAppService : ApplicationService, IVideoEquipFaultsAppService
    {
        #region Var
        private readonly IRepository<VideoEquipFaults, long> _abpVideoFaultrepository;
        private readonly IVideoEqFaultRepository _VideoFaultrepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public VideoEquipFaultsAppService(IRepository<VideoEquipFaults, long> a, IVideoEqFaultRepository b)

        {
            _abpVideoFaultrepository = a;
            _VideoFaultrepository = b;
        }

        /// <summary>
        /// 实现获取异常数据列表的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.VeqFaultOutput GetAllVideoEqFaults(Dtos.VeqFaultInput input)
        {
            return _VideoFaultrepository.GetAllVideoEqFaultsList(input, AbpSession);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _VideoFaultrepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.VeqFaultOutput GetAllVideoEqFaults1(Dtos.VeqFaultInput input)
        {
            return _VideoFaultrepository.GetAllVideoEqFaultsList1(input, AbpSession);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.VeqFaultOutput GetAllVideoEqFaults2(Dtos.VeqFaultInput input)
        {
            return _VideoFaultrepository.GetAllVideoEqFaultsList2(input, AbpSession);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete1(int Id)
        {
            _VideoFaultrepository.Delete(Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete2(int Id)
        {
            _VideoFaultrepository.Delete(Id);
        }
    }
}
