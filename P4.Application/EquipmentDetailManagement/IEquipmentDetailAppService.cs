using Abp.Application.Services;
using P4.EquipmentDetailManagement.Dtos;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentDetailManagement
{
    public interface IEquipmentDetailAppService : IApplicationService
    {
        GetAllEquipmentDetailOutput GetAllEquipmentDetail(SearchEquipmentDetailInput input);

        void Delete(CreateOrUpdateEquipmentDetailInput input);

        int Update(CreateOrUpdateEquipmentDetailInput input);

        int Insert(CreateOrUpdateEquipmentDetailInput input);

        /// <summary>
        /// 通过EqId查询PartsId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<EquipmentDetail> GetPartsIdByEqId(string id);

    }
}
