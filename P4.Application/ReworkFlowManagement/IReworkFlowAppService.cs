using Abp.Application.Services;
using P4.CompanyCustomerExpressManagement.Dto;
using P4.EquipmentDetailManagement.Dtos;
using P4.EquipmentMaintain;
using P4.ReworkFlowManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ReworkFlowManagement
{
    public interface IReworkFlowAppService : IApplicationService
    {
        /// <summary>
        /// 添加返修流程表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expressinput"></param>
        /// <returns></returns>
        int Insert(CreateOrUpdateReworkFlowInput input);
        /// <summary>
        /// 判断图片是否已经上传
        /// </summary>
        /// <param name="reworkPictureId"></param>
        /// <returns>true:存在，false:不存在</returns>
        bool GetReworkPicture(int reworkPictureId);

        void InsertPicture(ReworkPictureDto model);

        int CreatePicture(ReworkPictureDto entity);

        /// <summary>
        /// 查询返修流程表详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllReworkFlowOutput GetReworkFlowList(SearchReworkFlowInput input);

        /// <summary>
        /// 通过id查询EqId，PartsId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReworkFlowDto GetReworkFlowById(int id);

        /// <summary>
        /// 添加维修描述
        /// </summary>
        /// <param name="input"></param>
        int UpdateMaintainDescription(CreateOrUpdateReworkFlowInput input);

        /// <summary>
        /// 查询返修图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ReworkPictureDto> GetPictureList(int id);

        ReworkPictureDto GetPicture(int id);
    }
}
