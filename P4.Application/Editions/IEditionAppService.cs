using Abp.Application.Features;
using Abp.Application.Services;
using P4.Editions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Editions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEditionAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEditionOutput GetAll(SearchEditionInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<EditionDto> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Add(CreateOrUpdateEditionInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editionId"></param>
        /// <returns></returns>
        string Delete(int editionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Update(CreateOrUpdateEditionInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editionId"></param>
        /// <returns></returns>
        string SystemPermission(int editionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savetype"></param>
        /// <param name="chooseeditionid"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        bool SaveEditionFeature(string savetype, int chooseeditionid, string nodes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editionId"></param>
        /// <returns></returns>
        List<EditionFeatureSetting> GetEditionFeature(int editionId);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="editionId">版本id</param>
        /// <returns></returns>
        List<ElTreeDto> GetMenuTreeForEle(int editionId);

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="input"></param>
        bool SaveMenu(SaveMenuInput input);
    }
}
