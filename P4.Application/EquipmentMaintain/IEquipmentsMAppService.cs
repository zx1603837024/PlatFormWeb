using Abp.Application.Services;
using P4.EquipmentDetailManagement.Dtos;
using P4.EquipmentMaintain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentMaintain
{
    public interface IEquipmentsMAppService : IApplicationService
    {
        GetAllEquipmentsMOutput GetAllEquipmentsM(SearchEquipmentsMInput input);

        void Delete(CreateOrUpdateEquipmentsMInput input);

        void Update(CreateOrUpdateEquipmentsMInput input);

        void Insert(CreateOrUpdateEquipmentsMInput input);

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        EquipmentsMDto GetEquipmentDetailInfo(int Id);

        string GetEqld(string eqld);
        /// <summary>
        /// 两表查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEquipmentsMOutput GetAllEquipmentsMList(SearchEquipmentsMInput input);

        /// <summary>
        /// 通过批次号查询设备编号
        /// </summary>
        /// <param name="batchNum"></param>
        /// <returns></returns>
        List<EquipmentsM> GetEqIdByBatchNum(int batchNum);
    }
}
