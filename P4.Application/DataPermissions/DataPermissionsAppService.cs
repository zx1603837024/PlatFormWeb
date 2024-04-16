using Abp.Application.Services;
using Abp.Authorization.DataPermissions;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using P4.DataPermissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Authorization.Roles;


namespace P4.DataPermissions
{
    /// <summary>
    /// 数据权限
    /// </summary>
    public class DataPermissionsAppService : ApplicationService, IDataPermissionsAppService
    {

        #region Var
        private readonly IRepository<UserDataPermissionSetting, long> _abpUserDataPermissionRepository;
        private readonly IRepository<RoleDataPermissionSetting, long> _abpRoleDataPermissionRepository;

        private readonly IRepository<UserRole, long> _abpUserRoleRepository;
        #endregion

        public DataPermissionsAppService(IRepository<UserDataPermissionSetting, long> abpUserDataPermissionRepository,
            IRepository<RoleDataPermissionSetting, long> abpRoleDataPermissionRepository,IRepository<UserRole, long> abpUserRoleRepository)
        {
            _abpUserDataPermissionRepository = abpUserDataPermissionRepository;
            _abpRoleDataPermissionRepository = abpRoleDataPermissionRepository;
            _abpUserRoleRepository = abpUserRoleRepository;
        }

        public Abp.Application.Services.Dto.ListResultOutput<DataPermissionsDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<DataPermissionsDto>()
            {
                Items = _abpUserDataPermissionRepository.GetAllList().MapTo<List<DataPermissionsDto>>().ToList()
            };
        }


        public GetAllDataPermissionsOutput GetAllDataPermissionsList(DataPermissionsInput input)
        {
            int records = _abpUserDataPermissionRepository.Count();
            return new GetAllDataPermissionsOutput()
            {
                //rows = _abpUserDataPermissionRepository.GetAll().OrderByDescending(dic => dic.Id).PageBy(input).ToList().MapTo<List<DataPermissionsDto>>(),


                rows = _abpUserDataPermissionRepository.GetAll().OrderByDescending(dic => dic.Id).PageBy(input).ToList().MapTo<List<DataPermissionsDto>>(),
                records = records,
                total = records / input.rows + 1
            };
        }

        /// <summary>
        /// 插入数据权限
        /// </summary>
        /// <param name="input"></param>
        public void UserDataPermissionsInsert(CreateOrUpdateDataPermissionsInput input)
        {
            if (_abpUserDataPermissionRepository.FirstOrDefault(dic => dic.UserId == input.UserId) != default(UserDataPermissionSetting))
            {
                //已存在UserId
                UserDataPermissionsUpdate(input);
                return;
            }

            UserDataPermissionSetting entity = new UserDataPermissionSetting();
            entity.UserId = input.UserId;
            entity.CompanyIds = input.datapermission.CompanyIds;
            entity.RegionIds = input.datapermission.RegionIds;
            entity.ParkIds = input.datapermission.ParkIds;
            entity.BerthsecIds = input.datapermission.BerthsecIds;

            _abpUserDataPermissionRepository.Insert(entity);

        }
        public void RoleDataPermissionsInsert(CreateOrUpdateDataPermissionsInput input)
        {
            if (_abpRoleDataPermissionRepository.FirstOrDefault(dic => dic.RoleId == input.RoleId) != default(RoleDataPermissionSetting))
            {
                //已存在UserId
                RoleDataPermissionsUpdate(input);
                return;
            }

            RoleDataPermissionSetting entity = new RoleDataPermissionSetting();
            entity.TenantId = input.datapermission.TenantId;
            entity.RoleId = input.RoleId;
            entity.CompanyIds = input.datapermission.CompanyIds;
            entity.RegionIds = input.datapermission.RegionIds;
            entity.ParkIds = input.datapermission.ParkIds;
            entity.BerthsecIds = input.datapermission.BerthsecIds;         
            _abpRoleDataPermissionRepository.Insert(entity);

        }

        public void UserDataPermissionsUpdate(CreateOrUpdateDataPermissionsInput input)
        {
            UserDataPermissionSetting entity = null;
            //如果是树形权限展示，id=0
            if (input.Id != 0)
            {
                entity = _abpUserDataPermissionRepository.Load(input.Id);

                if (entity.UserId != input.UserId && _abpUserDataPermissionRepository.FirstOrDefault(dic => dic.UserId == entity.UserId) != default(UserDataPermissionSetting))
                    return;
            }
            else
            {

                if (_abpUserDataPermissionRepository.FirstOrDefault(dic => dic.UserId == input.UserId) == default(UserDataPermissionSetting))
                {
                    UserDataPermissionsInsert(input);
                }
                entity = GetAllUserDataPermissionByUserid(input.UserId).FirstOrDefault();
            }
            entity.TenantId = input.datapermission.TenantId;
            entity.CompanyIds = input.datapermission.CompanyIds;
            entity.RegionIds = input.datapermission.RegionIds;
            entity.ParkIds = input.datapermission.ParkIds;
            entity.BerthsecIds = input.datapermission.BerthsecIds;
            _abpUserDataPermissionRepository.Update(entity);

        }
        public void RoleDataPermissionsUpdate(CreateOrUpdateDataPermissionsInput input)
        {
            RoleDataPermissionSetting entity = null;
            //如果是树形权限展示，id=0
            if (input.Id != 0)
            {
                entity = _abpRoleDataPermissionRepository.Load(input.Id);

                if (entity.RoleId != input.RoleId && _abpRoleDataPermissionRepository.FirstOrDefault(dic => dic.RoleId == entity.RoleId) != default(RoleDataPermissionSetting))
                    return;
            }
            else
            {

                if (_abpRoleDataPermissionRepository.FirstOrDefault(dic => dic.RoleId == input.RoleId) == default(RoleDataPermissionSetting))
                {
                    RoleDataPermissionsInsert(input);
                }
                entity = GetAllRoleDataPermissionByRoleid(input.RoleId).FirstOrDefault();
            }
            entity.TenantId = input.datapermission.TenantId;
            entity.RoleId = input.RoleId;
            entity.CompanyIds = input.datapermission.CompanyIds;
            entity.RegionIds = input.datapermission.RegionIds;
            entity.ParkIds = input.datapermission.ParkIds;
            entity.BerthsecIds = input.datapermission.BerthsecIds;
            _abpRoleDataPermissionRepository.Update(entity);

        }

        public void DataPermissionsDelete(CreateOrUpdateDataPermissionsInput input)
        {
            var entity = _abpUserDataPermissionRepository.Load(input.Id);
            //if (_abpUserRoleRepository.FirstOrDefault(dic => dic.RoleId == input.Id) != default(UserRole))
            //    return;
            _abpUserDataPermissionRepository.Delete(input.Id);
        }
        /// <summary>
        /// 根据用户id查找用户数据权限信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>List<UserDataPermissionSetting></returns>
        public List<UserDataPermissionSetting> GetAllUserDataPermissionByUserid(long userid)
        {
            return _abpUserDataPermissionRepository.GetAll().Where(user => user.UserId == userid).ToList();
        }
        /// <summary>
        /// 根据角色id查找角色数据权限信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>List<UserDataPermissionSetting></returns>
        public List<RoleDataPermissionSetting> GetAllRoleDataPermissionByRoleid(long roleid)
        {
            return _abpRoleDataPermissionRepository.GetAll().Where(role => role.RoleId == roleid).ToList();
        }



    }
}
