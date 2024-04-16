using Abp.Domain.Repositories;
using P4.EquipmentMaintain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentMaintain
{
    public interface IEquipmentsMRepository : IRepository<EquipmentsM>
    {
        GetAllEquipmentsMOutput GetAllEquipmentsMList(SearchEquipmentsMInput input);
    }
}
