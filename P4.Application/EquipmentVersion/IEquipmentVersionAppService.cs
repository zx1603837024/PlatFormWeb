using Abp.Application.Services;
using P4.EquipmentVersion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentVersion
{

    /// <summary>
    /// PDA设备软件版本管理
    /// </summary>
    public interface IEquipmentVersionAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Update(CreateOrUpdateEquipmentVersionInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Insert(CreateOrUpdateEquipmentVersionInput input);

        string Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEquipmentVersionOutput GetAll(SearchEquipmentVersionInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OldVersion"></param>
        /// <param name="PDA"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        string GetEquipmentVersion(string OldVersion, string PDA, string Type);
    }
}
