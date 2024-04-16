using Abp.Domain.Repositories;
using P4.EquipmentVersion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentVersion
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEquipmentVersionRepository : IRepository<P4.Equipments.EquipmentVersion>
    {
        GetAllEquipmentVersionOutput GetAll(SearchEquipmentVersionInput input);
    }
}
