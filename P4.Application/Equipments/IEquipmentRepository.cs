using Abp.Domain.Repositories;
using P4.Equipments.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEquipmentRepository : IRepository<Equipment, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEquipmentOutput GetAllEquipmentOutputList(EquipmentInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEquipmentMaintenanceOutput GetAllEquipmentMaintenanceOutputList(EquipmentMaintenanceInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<EquipmentDto> GetEquipmentNumGroupByUseStatus(int? TenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TenantID"></param>
        void CargoInfoExport(int? TenantID);
    }
}
