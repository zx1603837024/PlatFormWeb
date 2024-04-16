using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Authorization.DataPermissions;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Localization;
using P4.Berthsecs;
using P4.Companys;
using P4.DataPermissions;
using P4.Menus;
using P4.Parks;
using P4.Permissions;
using P4.Permissions.Dto;
using P4.Regions;
using System.Collections.Generic;
using System.Linq;
using P4.MultiTenancy;
using P4.Users;
using P4.Editions;
using P4.Roles;
using Abp.Domain.Uow;
using P4.Common;

namespace P4.Tree
{
    /// <summary>
    /// 
    /// </summary>
    public class TreeAppService : P4AppServiceBase, ITreeAppService
    {

        #region Var
        private readonly IMenuAppService _menuApplicationService;
        private readonly IPermissionAppService _permissionApplicationService;
        private readonly IDataPermissionsAppService _datapermissionApplicationService;

        private readonly IRepository<OperatorsCompany> _operatorsCompanyRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<Park> _parkRepository;
        private readonly IRepository<Berthsec> _berthsecRepository;

        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<Tenant> _abpTenantRepository;
        private readonly UserManager _userManager;
        private readonly IEditionAppService _abpEditionAppService;
        private readonly IRoleAppService _abpRoleAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuApplicationService"></param>
        /// <param name="permissionApplicationService"></param>
        /// <param name="datapermissionApplicationService"></param>
        /// <param name="operatorsCompanyRepository"></param>
        /// <param name="regionRepository"></param>
        /// <param name="parkRepository"></param>
        /// <param name="berthsecRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="abpTenantRepository"></param>
        /// <param name="userManager"></param>
        /// <param name="abpEditionAppService"></param>
        /// <param name="abpRoleAppService"></param>
        /// <param name="unitOfWorkManager"></param>
        public TreeAppService(IMenuAppService menuApplicationService, IPermissionAppService permissionApplicationService,
            IDataPermissionsAppService datapermissionApplicationService, IRepository<OperatorsCompany> operatorsCompanyRepository,
            IRepository<Region> regionRepository, IRepository<Park> parkRepository, IRepository<Berthsec> berthsecRepository,
            IRepository<UserRole, long> userRoleRepository, IRepository<Tenant> abpTenantRepository, UserManager userManager, IEditionAppService abpEditionAppService,
            IRoleAppService abpRoleAppService, IUnitOfWorkManager unitOfWorkManager)
        {
            _menuApplicationService = menuApplicationService;
            _permissionApplicationService = permissionApplicationService;
            _datapermissionApplicationService = datapermissionApplicationService;
            _operatorsCompanyRepository = operatorsCompanyRepository;
            _regionRepository = regionRepository;
            _parkRepository = parkRepository;
            _berthsecRepository = berthsecRepository;
            _userRoleRepository = userRoleRepository;
            _abpTenantRepository = abpTenantRepository;
            _userManager = userManager;
            _abpEditionAppService = abpEditionAppService;
            _abpRoleAppService = abpRoleAppService;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string OperPermission(string id, string type)
        {
            string treeString = "";
            List<ZTree> ztreelist = new List<ZTree>();
            long longid = long.Parse(id);
            //List<Menu> list = _menuApplicationService.GetMenus();
            if (type == "user")
            {
                List<UserRole> userrolelist = _userRoleRepository.GetAll().Where(ur => ur.UserId == longid).ToList();
                //查询用户下的角色权限 并填充权限树
                // List<RolePermissionSetting> rolepermissionList = new List<RolePermissionSetting>();
                if (userrolelist != null)
                    ztreelist = GetRoleOperPermissionTreeByUser(userrolelist, longid);
                //获取用户权限
                List<UserPermissionSetting> userpermissionList = new List<UserPermissionSetting>();
                userpermissionList = _permissionApplicationService.GetAllUserPermissionByUserid(longid);
                foreach (var node in ztreelist)
                {
                    if (node.check == true)//根据check==true 将角色权限的状态改为不可更改
                    {
                        node.chkDisabled = true;//默认状态不可更改
                        node.doCheck = false;
                    }
                    if (userpermissionList.Exists(rp => node.name.Contains(rp.Name + ")")))
                    {
                        node.doCheck = true;
                        node.check = true;
                        node.chkDisabled = false;//是否不可更改
                    }
                    treeString += @"{ id: '" + node.id + "', pId: '" + node.pId + "', name: '" + node.name + "', checked: " + node.check.ToString().ToLower() + ", doCheck: " + node.doCheck.ToString().ToLower() + ",open: " + node.open.ToString().ToLower() + ",chkDisabled:" + node.chkDisabled.ToString().ToLower() + " },";
                }
                if (string.IsNullOrWhiteSpace(treeString))
                    return "{}";
                else
                    return treeString.Substring(0, treeString.Length - 1);
            }
            else if (type == "role")
            {
                ztreelist = GetRoleOperPermissionTree(longid);
                foreach (var node in ztreelist)
                {
                    treeString += @"{ id: '" + node.id + "', pId: '" + node.pId + "', name: '" + node.name + "', checked: " + node.check.ToString().ToLower() + ", doCheck: " + node.doCheck.ToString().ToLower() + ",open: " + node.open.ToString().ToLower() + " },";
                }
                return treeString.Substring(0, treeString.Length - 1);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string DataPermission(string id, string type)
        {
            string treeString = "";
            List<ZTree> ztreelist = new List<ZTree>();
            //List<Menu> list = _menuApplicationService.GetMenus();
            long longid = long.Parse(id);
            if (type == "user")
            {
                List<UserRole> userrolelist = _userRoleRepository.GetAll().Where(ur => ur.UserId == longid).ToList();
                //查询用户下的角色权限 并填充权限树             
                if (userrolelist != null)
                    ztreelist = GetRoleDataPermissionTreeByUser(userrolelist);
                //foreach (UserRole ur in userrolelist)
                //{
                //    ztreelist.AddRange(GetRoleDataPermissionTree(ur.RoleId));
                //}
                //获取用户权限
                //List<UserDataPermissionSetting> userDatapermissionList = new List<UserDataPermissionSetting>();
                //userDatapermissionList = _datapermissionApplicationService.GetAllUserDataPermissionByUserid(longid);
                List<string> userDatapermissionList = GetUserDataPermission(longid);
                foreach (var node in ztreelist)
                {
                    if (node.check == true)//根据check==true 将角色权限的状态改为不可更改
                    {
                        node.chkDisabled = true;//默认状态不可更改
                        node.doCheck = false;
                    }
                    if (userDatapermissionList.Count > 0 && userDatapermissionList.FirstOrDefault(com => node.id == com) != default(string))
                    {
                        node.doCheck = true;
                        node.check = true;
                        node.chkDisabled = false;//是否可更改
                    }
                    treeString += @"{ id: '" + node.id + "', pId: '" + node.pId + "', name: '" + node.name + "', checked: " + node.check.ToString().ToLower() + ", doCheck: " + node.doCheck.ToString().ToLower() + ",open: " + node.open.ToString().ToLower() + ",chkDisabled:" + node.chkDisabled.ToString().ToLower() + " },";

                }
                return treeString.Substring(0, treeString.Length - 1);
            }
            else if (type == "role")
            {
                ztreelist = GetRoleDataPermissionTree(longid);
                foreach (var node in ztreelist)
                {
                    treeString += @"{ id: '" + node.id + "', pId: '" + node.pId + "', name: '" + node.name + "', checked: " + node.check.ToString().ToLower() + ", doCheck: " + node.doCheck.ToString().ToLower() + ",open: " + node.open.ToString().ToLower() + " },";
                }
                return treeString.Substring(0, treeString.Length - 1);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<ZTree> GetRoleOperPermissionTree(long roleid)
        {
            List<ZTree> ztreelist = new List<ZTree>();
            List<RolePermissionSetting> rolepermissionList = new List<RolePermissionSetting>();
            rolepermissionList = _permissionApplicationService.GetAllRolePermissionByRoleid(roleid);
            List<PermissionSetting> permissionList = rolepermissionList.Cast<PermissionSetting>().ToList();
            
            List<Menu> listtemp = _menuApplicationService.GetMenus();
            List<Menu> list = new List<Menu>();
            Authorization.Role role = new Authorization.Role();
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany))
            {
                role = _abpRoleAppService.GetRoleById((int)roleid);
            }
            if (role.TenantId.HasValue)
            {
                var tenantid = _abpTenantRepository.FirstOrDefault(entity => entity.Id == role.TenantId.Value);
                var editionFeatureList = _abpEditionAppService.GetEditionFeature(tenantid.EditionId.Value);
                foreach (var v in listtemp)
                {
                    if (editionFeatureList.Exists(entry => entry.Name == v.RequiredPermissionName))
                        list.Add(v);
                }
            }
            else
            {
                list = listtemp;
            }

            ProcessMenuStructure("0", list, permissionList, ztreelist);
            return ztreelist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<ZTree> GetRoleDataPermissionTree(long roleid)
        {
            List<ZTree> ztreelist = new List<ZTree>();
            List<RoleDataPermissionSetting> roleDatapermissionList = new List<RoleDataPermissionSetting>();

            roleDatapermissionList.AddRange(_datapermissionApplicationService.GetAllRoleDataPermissionByRoleid(roleid));
            List<DataPermissionSetting> dataPermissionList = roleDatapermissionList.Cast<DataPermissionSetting>().ToList();
            ztreelist = GetDataTree(dataPermissionList);
            return ztreelist;
        }

        /// <summary>
        /// 一个用户可能对应多角色
        /// 一个角色对应一条数据权限信息
        /// </summary>
        /// <param name="userrolelist"></param>
        /// <returns></returns>
        public List<ZTree> GetRoleOperPermissionTreeByUser(List<UserRole> userrolelist, long userid)
        {
            List<ZTree> ztreelist = new List<ZTree>();
            List<RolePermissionSetting> rolepermissionList = new List<RolePermissionSetting>();
            foreach (UserRole ur in userrolelist)
                rolepermissionList.AddRange(_permissionApplicationService.GetAllRolePermissionByRoleid(ur.RoleId));
            List<PermissionSetting> permissionList = rolepermissionList.Cast<PermissionSetting>().ToList();
            List<Menu> listtemp = _menuApplicationService.GetMenus();
            List<Menu> list = new List<Menu>();
            Users.User user = new Users.User();
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany))
            {
                user = _userManager.GetUserById(userid);
            }
            if (user.TenantId.HasValue)
            {
                var tenantid = _abpTenantRepository.FirstOrDefault(entity => entity.Id == user.TenantId.Value);
                var editionFeatureList = _abpEditionAppService.GetEditionFeature(tenantid.EditionId.Value);
                foreach (var v in listtemp)
                {
                    if (editionFeatureList.Exists(entry => entry.Name == v.RequiredPermissionName))
                        list.Add(v);
                }
            }
            else
            {
                list = listtemp;
            }
            ProcessMenuStructure("0", list, permissionList, ztreelist);
            return ztreelist;
        }
        /// <summary>
        /// 一个用户可能对应多角色
        /// 一个角色对应一条数据权限信息
        /// </summary>
        /// <param name="userrolelist"></param>
        /// <returns></returns>
        public List<ZTree> GetRoleDataPermissionTreeByUser(List<UserRole> userrolelist)
        {
            List<ZTree> ztreelist = new List<ZTree>();
            List<RoleDataPermissionSetting> roleDatapermissionList = new List<RoleDataPermissionSetting>();
            foreach (UserRole ur in userrolelist)
                roleDatapermissionList.AddRange(_datapermissionApplicationService.GetAllRoleDataPermissionByRoleid(ur.RoleId));
            List<DataPermissionSetting> dataPermissionList = roleDatapermissionList.Cast<DataPermissionSetting>().ToList();
            ztreelist = GetDataTree(dataPermissionList);
            return ztreelist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string> GetUserDataPermission(long userid)
        {
            List<string> datapermissinglist = new List<string>();
            List<UserDataPermissionSetting> userDatapermissionList = new List<UserDataPermissionSetting>();
            userDatapermissionList = _datapermissionApplicationService.GetAllUserDataPermissionByUserid(userid);
            if (userDatapermissionList.Count < 1)
                return datapermissinglist;
            string comStr = "cmp" + userDatapermissionList[0].CompanyIds;
            string regStr = "rgn" + userDatapermissionList[0].RegionIds;
            string parStr = "prk" + userDatapermissionList[0].ParkIds;
            string berStr = "bts" + userDatapermissionList[0].BerthsecIds;
            comStr = comStr.Replace(",", ",cmp");
            regStr = regStr.Replace(",", ",rgn");
            parStr = parStr.Replace(",", ",prk");
            berStr = berStr.Replace(",", ",bts");
            List<string> companyidlist = string.IsNullOrWhiteSpace(userDatapermissionList[0].CompanyIds) == true ? new List<string>() : new List<string>(comStr.Split(','));
            List<string> regionsidlist = string.IsNullOrWhiteSpace(userDatapermissionList[0].RegionIds) == true ? new List<string>() : new List<string>(regStr.Split(','));
            List<string> parkidlist = string.IsNullOrWhiteSpace(userDatapermissionList[0].ParkIds) == true ? new List<string>() : new List<string>(parStr.Split(','));
            List<string> berthsecidlist = string.IsNullOrWhiteSpace(userDatapermissionList[0].BerthsecIds) == true ? new List<string>() : new List<string>(berStr.Split(','));

            datapermissinglist.AddRange(companyidlist);
            datapermissinglist.AddRange(regionsidlist);
            datapermissinglist.AddRange(parkidlist);
            datapermissinglist.AddRange(berthsecidlist);
            return datapermissinglist;
        }
        /// <summary>
        /// 数据权限树
        /// </summary>
        /// <param name="DatapermissionList"></param>
        /// <returns></returns>
        public List<ZTree> GetDataTree(List<DataPermissionSetting> DatapermissionList)
        {
            List<Company> userCompany = new List<Company>();
            string[] companyidlist = null;
            string[] regionsidlist = null;
            string[] parkidlist = null;
            string[] berthsecidlist = null;
            //解析userDatapermissionList  if (string.IsNullOrWhiteSpace(name))
            if (DatapermissionList != null && DatapermissionList.Count > 0)
            {
                foreach (DataPermissionSetting modal in DatapermissionList)
                {
                    companyidlist = string.IsNullOrWhiteSpace(modal.CompanyIds) == true ? null : modal.CompanyIds.Split(',');
                    regionsidlist = string.IsNullOrWhiteSpace(modal.RegionIds) == true ? null : modal.RegionIds.Split(',');
                    parkidlist = string.IsNullOrWhiteSpace(modal.ParkIds) == true ? null : modal.ParkIds.Split(',');
                    berthsecidlist = string.IsNullOrWhiteSpace(modal.BerthsecIds) == true ? null : modal.BerthsecIds.Split(',');
                }
            }

            List<ZTree> ztreelist = new List<ZTree>();
            // ztreelist
            //商户信息
            ZTree ztree = new ZTree();
            ztree.id = "tnt" + (AbpSession.TenantId ?? 0).ToString();
            ztree.pId = "0";
            ztree.name = AbpSession.UserName;
            ztree.open = true;
            ztree.check = true;
            ztree.doCheck = true;
            ztreelist.Add(ztree);

            List<OperatorsCompany> companyList = _operatorsCompanyRepository.GetAllList();
            if (companyList == null || companyList.Count < 1)
                return ztreelist;
            foreach (OperatorsCompany modal in companyList)
            {
                ztree = new ZTree();
                ztree.id = "cmp" + modal.Id;
                ztree.pId = "tnt" + modal.TenantId;
                ztree.name = modal.CompanyName;
                ztree.doCheck = true;
                if (companyidlist != null && companyidlist.FirstOrDefault(com => int.Parse(com) == modal.Id) != default(string))
                {
                    ztree.check = true;
                    ztree.open = true;
                }
                ztreelist.Add(ztree);
            }



            List<Region> regionList = _regionRepository.GetAllList();
            if (regionList == null || regionList.Count < 1)
                return ztreelist;
            foreach (Region modal in regionList)
            {
                ztree = new ZTree();
                ztree.id = "rgn" + modal.Id;
                ztree.pId = "cmp" + modal.CompanyId;
                ztree.name = modal.RegionName;
                ztree.doCheck = true;
                if (regionsidlist != null && regionsidlist.FirstOrDefault(reg => int.Parse(reg) == modal.Id) != default(string))
                {
                    ztree.check = true;
                    ztree.open = true;
                }
                ztreelist.Add(ztree);
            }
            List<Park> parkList = _parkRepository.GetAllList();
            if (parkList == null || parkList.Count < 1)
                return ztreelist;
            foreach (Park modal in parkList)
            {
                ztree = new ZTree();
                ztree.id = "prk" + modal.Id;
                ztree.pId = "rgn" + modal.RegionId;
                ztree.name = modal.ParkName;
                ztree.doCheck = true;
                if (parkidlist != null && parkidlist.FirstOrDefault(par => int.Parse(par) == modal.Id) != default(string))
                {
                    ztree.check = true;
                    ztree.open = true;
                }
                ztreelist.Add(ztree);
            }
            List<Berthsec> berthList = _berthsecRepository.GetAllList();
            if (berthList == null || berthList.Count < 1)
                return ztreelist;
            foreach (Berthsec modal in berthList)
            {
                ztree = new ZTree();
                ztree.id = "bts" + modal.Id;
                ztree.pId = "prk" + modal.ParkId;
                ztree.name = modal.BerthsecName;
                ztree.doCheck = true;
                if (berthsecidlist != null && berthsecidlist.FirstOrDefault(ber => int.Parse(ber) == modal.Id) != default(string))
                {
                    ztree.check = true;
                    ztree.open = true;
                }
                ztreelist.Add(ztree);
            }
            return ztreelist;


        }
        /// <summary>
        /// 操作权限树
        /// </summary>
        /// <param name="name"></param>
        /// <param name="menus"></param>
        /// <param name="permissionList"></param>
        /// <param name="treelist"></param>
        /// <returns></returns>
        private IList<MenuItemDefinition> ProcessMenuStructure(string name, List<Menu> menus, List<PermissionSetting> permissionList, List<ZTree> treelist)
        {
            try
            {
                IList<MenuItemDefinition> menulist = new List<MenuItemDefinition>();

                //判断是否是最底层菜单，若是最底层，则添加增删改查等叶子节点
                List<Menu> ms = menus.FindAll(menu => menu.FatherCode == name);
                if (ms == null || ms.Count < 1)
                {
                    Permission permission = PermissionManager.GetPermissionOrNull(name);
                   
                    //if (permission != null)
                    foreach (var childPermission in permission.Children)
                    {
                        ZTree ztree = new ZTree();
                        ztree.id = childPermission.Name;
                        ztree.pId = name;
                        if (childPermission.Name.Contains(".Field"))
                            ztree.name = L(childPermission.Name) + "(" + childPermission.Name + ")";
                        else
                            ztree.name = L(childPermission.Name.Split('.')[1]) + "(" + childPermission.Name + ")";
                        ztree.doCheck = true;
                        if (permissionList.Exists(user => user.Name == childPermission.Name))
                        {
                            // ztree.open = true;
                            ztree.check = true;
                        }
                        else
                        {
                            //ztree.open = true;
                            ztree.check = false;
                        }
                        treelist.Add(ztree);
                    }

                }

                foreach (var model in ms)
                {

                    ZTree ztree = new ZTree();
                    ztree.id = model.Name;
                    ztree.pId = model.FatherCode;
                    ztree.name = L(model.RequiredPermissionName) + "(" + model.RequiredPermissionName + ")";
                    //if (docheckFlag == true)
                    ztree.doCheck = true;

                    if (permissionList.Exists(user => user.Name == model.Name))
                    {
                        ztree.open = true;
                        ztree.check = true;
                    }
                    else
                    {
                        ztree.open = false;
                        ztree.check = false;
                    }

                    MenuItemDefinition menu = new MenuItemDefinition(
                        model.Name,
                        new LocalizableString(model.RequiredPermissionName, P4Consts.LocalizationSourceName),
                        url: model.Url,
                        icon: model.Icon,
                        requiredPermissionName: model.RequiredPermissionName
                        );
                    menu.Items = ProcessMenuStructure(model.RequiredPermissionName, menus, permissionList, treelist);
                    menulist.Add(menu);
                    treelist.Add(ztree);
                }
                return menulist;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// 解析数据权限树
        /// </summary>
        /// <param name="zlist">选中节点集合</param>
        /// <returns>数据权限对象</returns>
        public DataPermission AnalyzeDataPermission(List<ZTree> zlist)
        {
            DataPermission dp = new DataPermission();
            foreach (ZTree zt in zlist)
            {
                switch (zt.id.Substring(0, 3))
                {
                    case "tnt":
                        dp.TenantId = int.Parse(zt.id.Substring(3, zt.id.Length - 3));
                        break;
                    case "cmp":
                        dp.CompanyIds += zt.id.Substring(3, zt.id.Length - 3) + ",";
                        break;
                    case "rgn":
                        dp.RegionIds += zt.id.Substring(3, zt.id.Length - 3) + ",";
                        break;
                    case "prk":
                        dp.ParkIds += zt.id.Substring(3, zt.id.Length - 3) + ",";
                        break;
                    case "bts":
                        dp.BerthsecIds += zt.id.Substring(3, zt.id.Length - 3) + ",";
                        break;
                    default:
                        break;
                }
            }
            //去除多余逗号
            dp.CompanyIds = string.IsNullOrWhiteSpace(dp.CompanyIds) == true ? null : dp.CompanyIds.Substring(0, dp.CompanyIds.Length - 1);
            dp.RegionIds = string.IsNullOrWhiteSpace(dp.RegionIds) == true ? null : dp.RegionIds.Substring(0, dp.RegionIds.Length - 1);
            dp.ParkIds = string.IsNullOrWhiteSpace(dp.ParkIds) == true ? null : dp.ParkIds.Substring(0, dp.ParkIds.Length - 1);
            dp.BerthsecIds = string.IsNullOrWhiteSpace(dp.BerthsecIds) == true ? null : dp.BerthsecIds.Substring(0, dp.BerthsecIds.Length - 1);
            return dp;

        }
    }

}
