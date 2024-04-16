using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Permissions.Dto;
using Abp.Linq.Extensions;
using Abp.Authorization.Users;
using Abp.Authorization.Roles;


namespace P4.Permissions
{

    /// <summary>
    /// 权限管理
    /// </summary>
    public class PermissionAppService : IPermissionAppService
    {
        #region  Var
        private readonly IRepository<PermissionSetting, long> _abpPermissionRepository;
        private readonly IRepository<UserPermissionSetting, long> _userPermissionSettingRepository;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionSettingRepository;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpPermissionRepository"></param>
        /// <param name="userPermissionSettingRepository"></param>
        /// <param name="rolePermissionSettingRepository"></param>
        public PermissionAppService(IRepository<PermissionSetting, long> abpPermissionRepository, IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
        {
            _abpPermissionRepository = abpPermissionRepository;
            _userPermissionSettingRepository = userPermissionSettingRepository;
            _rolePermissionSettingRepository = rolePermissionSettingRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Abp.Application.Services.Dto.ListResultOutput<PermissionDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<PermissionDto>() {
                Items = _abpPermissionRepository.GetAll().MapTo<List<PermissionDto>>().ToList()
            };
        }




        public GetAllRolePermissionsTypeOutput GetAllRolePermissionsList(RolePermissionsInput input)
        {
            int records = _abpPermissionRepository.Count();
            return new GetAllRolePermissionsTypeOutput()
            {

                rows = _abpPermissionRepository.GetAll().OrderByDescending(dic => dic.Id).PageBy(input).ToList().MapTo<List<PermissionDto>>(),
                records = records,
                total = records / input.rows + 1
            };
        }


        public void RolePermissionsInsert(CreateOrUpdateRolePermissionsInput input)
        {
            //if (_abpPermissionRepository.FirstOrDefault(dic => dic.UserId == input.UserId) != null)
            //{
            //    //已存在UserId
            //    return;
            //}

            UserPermissionSetting entity = new UserPermissionSetting();
            entity.Name = input.Name;
            entity.IsGranted = input.IsGranted;
            entity.MultiTenancySide = input.MultiTenancySide;
            //entity.TenantId = input.TenantId;
            //entity.RoleId = input.RoleId;
            //entity.UserId = input.UserId;
            //entity.CompanyIds = input.CompanyIds;
            //entity.RegionIds = input.RegionIds;
            //entity.ParkIds = input.ParkIds;
            //entity.BerthsecIds = input.BerthsecIds;

            _abpPermissionRepository.Insert(entity);
        }

        public void RolePermissionsUpdate(CreateOrUpdateRolePermissionsInput input)
        {


            var entity = _abpPermissionRepository.Load(input.Id);
            
            //if (entity.UserId != input.UserId && _abpPermissionRepository.FirstOrDefault(dic => dic.UserId == entity.UserId) != default(RolePermission))
            //    return;
            entity.IsGranted = input.IsGranted;
            entity.MultiTenancySide = input.MultiTenancySide;
           
           
            _abpPermissionRepository.Update(entity);

        }

        public void RolePermissionsDelete(CreateOrUpdateRolePermissionsInput input)
        {
            var entity = _abpPermissionRepository.Load(input.Id);
            //if (_abpUserRoleRepository.FirstOrDefault(dic => dic.RoleId == input.Id) != default(UserRole))
            //    return;

            _abpPermissionRepository.Delete(input.Id);

        }

        public List<UserPermissionSetting> GetAllUserPermissionByUserid(long userid)
        {
            return _userPermissionSettingRepository.GetAll().Where(user => user.UserId == userid && user.IsGranted == true).ToList();
        }
        public List<RolePermissionSetting> GetAllRolePermissionByRoleid(long roleid)
        {
            return _rolePermissionSettingRepository.GetAll().Where(role => role.RoleId == roleid && role.IsGranted == true).ToList();
        }
    }
}
