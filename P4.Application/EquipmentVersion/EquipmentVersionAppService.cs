using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using P4.EquipmentVersion.Dtos;
using Abp.Configuration;
using P4.Equipments;

namespace P4.EquipmentVersion
{
    /// <summary>
    /// PDA设备软件版本管理
    /// </summary>
    public class EquipmentVersionAppService : P4AppServiceBase, IEquipmentVersionAppService
    {
        #region Var
        private readonly IRepository<P4.Equipments.EquipmentVersion> _abpEquipmentVersionRepository;
        private readonly IEquipmentVersionRepository _equipmentVersionRepository;
        private readonly ISettingStore _settingStore;
        private readonly IRepository<Equipment, long> _abpEquipmentRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpEquipmentVersionRepository"></param>
        /// <param name="equipmentVersionRepository"></param>
        /// <param name="settingStore"></param>
        /// <param name="abpEquipmentRepository"></param>
        public EquipmentVersionAppService(IRepository<P4.Equipments.EquipmentVersion> abpEquipmentVersionRepository, IEquipmentVersionRepository equipmentVersionRepository, ISettingStore settingStore, IRepository<Equipment, long> abpEquipmentRepository)
        {
            _abpEquipmentVersionRepository = abpEquipmentVersionRepository;
            _equipmentVersionRepository = equipmentVersionRepository;
            _settingStore = settingStore;
            _abpEquipmentRepository = abpEquipmentRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Update(Dtos.CreateOrUpdateEquipmentVersionInput input)
        {
            _abpEquipmentVersionRepository.Update(input.MapTo<P4.Equipments.EquipmentVersion>());
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllEquipmentVersionOutput GetAll(Dtos.SearchEquipmentVersionInput input)
        {
            return _equipmentVersionRepository.GetAll(input);
        }

        /// <summary>
        /// 检测最新版本号
        /// </summary>
        /// <param name="OldVersion">设备版本号</param>
        /// <param name="PDA">设备编号</param>
        /// <param name="Type">设备型号</param>
        /// <returns></returns>
        public string GetEquipmentVersion(string OldVersion, string PDA, string Type)
        {
            var temp = _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "PDAUpgrate");
            var model = _abpEquipmentVersionRepository.Single(entity => entity.IsActive == true && entity.EqupmentType == Type);
            var equipment = _abpEquipmentRepository.Single(entity => entity.PDA == PDA);
            if (bool.Parse(temp.Value) || model.IsUpgrade || equipment.IsUpgrade)
            {
                if (VersionToInt(model.Version) > VersionToInt(OldVersion))
                {
                    return model.Version;
                }
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private Int64 VersionToInt(string version)
        {
            return Int64.Parse(version.Replace(".", "").Replace("V", "").Replace("C", ""));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Insert(CreateOrUpdateEquipmentVersionInput input)
        {
            _abpEquipmentVersionRepository.Insert(input.MapTo<P4.Equipments.EquipmentVersion>());
            return null;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Delete(int id)
        {
            _abpEquipmentVersionRepository.Delete(id);
            return null;
        }
    }
}
