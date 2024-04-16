using Abp.Application.Services;
using P4.Regions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Regions
{
    /// <summary>
    /// 区域管理
    /// </summary>
    public interface IRegionAppService : IApplicationService
    {
        /// <summary>
        /// 查询区域数据（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllRegionsOutput GetAllRegionsList(GetAllRegionsInput input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Insert(CreateOrUpdateDtoInput input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Modify(CreateOrUpdateDtoInput input);

        /// <summary>
        /// 查询所有区域数据
        /// </summary>
        /// <returns></returns>
        List<RegionDto> GetAllRegion();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        RegionDto Load(int Id);

        /// <summary>
        /// 权限转换成name
        /// </summary>
        /// <returns></returns>
        List<RegionDto> GetUseDataPermissions();
    }
}
