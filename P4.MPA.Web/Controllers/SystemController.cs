using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Authorization;
using P4.DataPermissions;
using P4.DataPermissions.Dtos;
using P4.Menus;
using P4.Menus.Dto;
using P4.Permissions;
using P4.Permissions.Dto;
using P4.Roles;
using P4.Roles.Dto;
using P4.Tenants;
using P4.Tenants.Dto;
using P4.Tree;
using P4.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    /// <summary>
    /// 系统管理
    /// </summary>
    [AbpMvcAuthorize]
    public class SystemController : P4ControllerBase
    {
        #region Var
        private readonly IMenuAppService _menuApplicationService;
        private readonly ITenantAppService _tenantApplicationService;
        private readonly IRoleAppService _roleApplicationService;
        private readonly IDataPermissionsAppService _dataPermissionApplicationService;
        private readonly IPermissionAppService _permissionApplicationService;
        private readonly UserManager _userManagerApplicationService;
        private readonly ITreeAppService _treeApplicationService;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="menuApplicationService">构造函数注入</param>
        /// <param name="roleApplicationService">构造函数注入</param>
        /// <param name="tenantApplicationService">构造函数注入</param>
        public SystemController(IMenuAppService menuApplicationService, ITenantAppService tenantApplicationService,
            IRoleAppService roleApplicationService, IDataPermissionsAppService dataPermissionApplicationService,
            IPermissionAppService permissionApplicationService, UserManager userManagerApplicationService, ITreeAppService treeApplicationService,
            UserManager userManager, RoleManager roleManager)
        {
            _menuApplicationService = menuApplicationService;
            _roleApplicationService = roleApplicationService;
            _tenantApplicationService = tenantApplicationService;
            _dataPermissionApplicationService = dataPermissionApplicationService;
            _permissionApplicationService = permissionApplicationService;
            _userManagerApplicationService = userManagerApplicationService;
            _treeApplicationService = treeApplicationService;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        public ActionResult TestTree1()
        {
            return View();
        }

        #region menus management
        /// <summary>
        /// 系统菜单管理
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize(Permissions = new string[] { "MenusManagement" })]
        public ActionResult Menus()
        {

            return View();
        }

        /// <summary>
        /// 获取菜单数据列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenus()
        {

            var list = _menuApplicationService.GetTreeMenus();
            return this.Json(list);
        }

        /// <summary>
        /// 增删改业务逻辑处理
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessMenu(Menu entity, string oper)
        {
            switch (oper)
            {
                case "del":
                    _menuApplicationService.Delete(entity.Id);
                    break;
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse());
        }

        #endregion


        #region Roles Management
        [AbpMvcAuthorize("RolesManagement")]
        public ActionResult Roles()
        {
            return View();
        }


        public JsonResult GetRoles()
        {

            var list = _roleApplicationService.GetAll();

            return this.Json(list.Items);
        }

        public JsonResult GetAllRoleList(RoleInput entity)
        {
            return this.Json(_roleApplicationService.GetAllRoleList(entity));
        }


        public JsonResult ProcessRoles(CreateOrUpdateRoleInput input)
        {
            JsonResult returnJson = new JsonResult();
            switch (input.oper)
            {
                case "add":
                    returnJson = ValueInsert(input);
                    break;
                case "del":
                    returnJson = ValueDelete(input);
                    break;
                case "edit":
                    returnJson = ValueEdit(input);
                    break;
                default:
                    break;

            }
            return this.Json(new MvcAjaxResponse());
        }

        [AbpMvcAuthorize("Role.Insert")]
        public JsonResult ValueInsert(CreateOrUpdateRoleInput input)
        {
            
            if (_roleApplicationService.RoleInsert(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("角色名称重复！！")));
            else
                return this.Json(new MvcAjaxResponse(true));
           
        }

        [AbpMvcAuthorize("Role.Delete")]
        public JsonResult ValueDelete(CreateOrUpdateRoleInput input)
        {

            if (_roleApplicationService.RoleDelete(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("该角色已分配给了用户，请先解除绑定！")));
            else
                return this.Json(new MvcAjaxResponse(true));
            //_roleApplicationService.RoleDelete(input);
            //return this.Json("12121");
        }

        [AbpMvcAuthorize("Role.Update")]
        public JsonResult ValueEdit(CreateOrUpdateRoleInput input)
        {
            if (_roleApplicationService.RoleUpdate(input) == 1)
                return this.Json(new MvcAjaxResponse(new ErrorInfo("请确认角色名是否重复，角色是否为静态")));
            else
                return this.Json(new MvcAjaxResponse(true));
            //_roleApplicationService.RoleUpdate(input);
            //return this.Json("12121");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string RoleOperPermission(string roleid)
        {
            return _treeApplicationService.OperPermission(roleid, "role");

        }


        public string RoleDataPermission(string roleid)
        {
            return _treeApplicationService.DataPermission(roleid, "role");

        }

        public async Task<JsonResult> SavePermission(string savetype, int chooseroleid, string nodes)
        {
            nodes = nodes.Replace("checked", "check");
            List<ZTree> zlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ZTree>>(nodes);
            List<Permission> permissionlist = new List<Permission>();
            //int roleid = int.Parse(chooseroleid);
            //P4.Users.User u = _userApplicationService.GetUserInfo(userid);
            Role role = new Role() { Id = chooseroleid };
            if (savetype == "data")
            {
                CreateOrUpdateDataPermissionsInput input = new CreateOrUpdateDataPermissionsInput();
                input.RoleId = chooseroleid;
                input.datapermission = _treeApplicationService.AnalyzeDataPermission(zlist);
                _dataPermissionApplicationService.RoleDataPermissionsInsert(input);

                // DataPermission dp= anasyDataPermission(zlist);
            }
            else if (savetype == "oper")
            {
                foreach (ZTree zt in zlist)
                {
                    Permission permission = PermissionManager.GetPermissionOrNull(zt.id);
                    permissionlist.Add(permission);
                }
                await _roleManager.SetGrantedPermissions(role, permissionlist);
            }
            return this.Json(new MvcAjaxResponse());
        }


        #endregion


        #region DataPermissions Management

        //[AbpMvcAuthorize("RolesDataPermissionsManagement")]
        //public ActionResult DataPermissions()
        //{
        //    return View();
        //}
        //public JsonResult GetAllRolesDataPermissionsList(DataPermissionsInput entity)
        //{
        //    return this.Json(_dataPermissionApplicationService.GetAllDataPermissionsList(entity));
        //}

        //public JsonResult RolesDataPermissionsProcessRoles(CreateOrUpdateDataPermissionsInput input)
        //{
        //    JsonResult returnJson = new JsonResult();
        //    switch (input.oper)
        //    {
        //        case "add":
        //            returnJson = DataPermissionsInsert(input);
        //            break;
        //        case "del":
        //            returnJson = DataPermissionsDelete(input);
        //            break;
        //        case "edit":
        //            returnJson = DataPermissionsEdit(input);
        //            break;
        //        default:
        //            break; ;

        //    }
        //    return returnJson;
        //}
        //[AbpMvcAuthorize("DataPermissions.Insert")]
        //public JsonResult DataPermissionsInsert(CreateOrUpdateDataPermissionsInput input)
        //{
        //    _dataPermissionApplicationService.UserDataPermissionsInsert(input);
        //    return this.Json("12121");
        //}

        //[AbpMvcAuthorize("DataPermissions.Delete")]
        //public JsonResult DataPermissionsDelete(CreateOrUpdateDataPermissionsInput input)
        //{
        //    _dataPermissionApplicationService.DataPermissionsDelete(input);
        //    return this.Json("12121");
        //}

        //[AbpMvcAuthorize("DataPermissions.Update")]
        //public JsonResult DataPermissionsEdit(CreateOrUpdateDataPermissionsInput input)
        //{
        //    _dataPermissionApplicationService.DataPermissionsUpdate(input);
        //    return this.Json("12121");
        //}

        #endregion



        #region RolesPermissions Management

        //[AbpMvcAuthorize("RolesPermissionsManagement")]
        //public ActionResult RolesPermissions()
        //{
        //    return View();
        //}
        //public JsonResult GetAllRolesPermissionsList(RolePermissionsInput entity)
        //{
        //    return this.Json(_permissionApplicationService.GetAllRolePermissionsList(entity));
        //}

        //public JsonResult RolePermissionsProcess(CreateOrUpdateRolePermissionsInput input)
        //{
        //    JsonResult returnJson = new JsonResult();
        //    switch (input.oper)
        //    {
        //        case "add":
        //            returnJson = RolePermissionsInsert(input);
        //            break;
        //        case "del":
        //            returnJson = RolePermissionsDelete(input);
        //            break;
        //        case "edit":
        //            returnJson = RolePermissionsEdit(input);
        //            break;
        //        default:
        //            break;

        //    }
        //    return returnJson;
        //}
        //[AbpMvcAuthorize("RolesPermissionsManagement.Insert")]
        //public JsonResult RolePermissionsInsert(CreateOrUpdateRolePermissionsInput input)
        //{
        //    _permissionApplicationService.RolePermissionsInsert(input);
        //    return this.Json("12121");
        //}


        //[AbpMvcAuthorize("RolesPermissionsManagement.Delete")]
        //public JsonResult RolePermissionsDelete(CreateOrUpdateRolePermissionsInput input)
        //{
        //    _permissionApplicationService.RolePermissionsDelete(input);
        //    return this.Json("12121");
        //}

        //[AbpMvcAuthorize("RolesPermissionsManagement.Update")]
        //public JsonResult RolePermissionsEdit(CreateOrUpdateRolePermissionsInput input)
        //{
        //    _permissionApplicationService.RolePermissionsUpdate(input);
        //    return this.Json("12121");
        //}

        #endregion


        #region Tenants Management
        [AbpMvcAuthorize("TenantsManagement")]
        public ActionResult Tenants()
        {
            return View();
        }

        public string GetTenantsSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            if (!AbpSession.TenantId.HasValue)
            {
                strtemp.Append(alloption);
                foreach (var model in _tenantApplicationService.GetAll().Items)
                {
                    strtemp.AppendFormat(option, model.Id, model.Name);
                }
            }
            else
            {
                var tenant = _tenantApplicationService.FirstOrDefault(AbpSession.TenantId.Value);
                strtemp.AppendFormat(option, tenant.Id, tenant.Name);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public JsonResult GetTenants(TenantInput input)
        {
            var list = _tenantApplicationService.GetAll(input);
            return this.Json(list);
        }

        public JsonResult ProcessTenants(CreateOrUpdateTenantInput input)
        {
            JsonResult returnJson = new JsonResult();

            switch (input.oper)
            {
                case "add":
                    returnJson = TenantInsert(input);
                    break;
                case "del":
                    returnJson = TenantDelete(input);
                    break;
                case "edit":
                    returnJson = TenantUpdate(input);
                    break;
            }
            return this.Json(new MvcAjaxResponse());
        }


        public string GetRolesSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select style='height:50px'>");
            foreach (var model in _roleApplicationService.GetAll().Items)
            {
                strtemp.AppendFormat(option, model.Id, model.Name);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        [AbpMvcAuthorize("TenantsManagement.Insert")]
        public JsonResult TenantInsert(CreateOrUpdateTenantInput input)
        {
            _tenantApplicationService.TenantInsert(input);
            return this.Json("12121");
        }

        [AbpMvcAuthorize("TenantsManagement.Delete")]
        public JsonResult TenantDelete(CreateOrUpdateTenantInput input)
        {
            _tenantApplicationService.TenantDelete(input);
            return this.Json("123123");
        }
        [AbpMvcAuthorize("TenantsManagement.Update")]
        public JsonResult TenantUpdate(CreateOrUpdateTenantInput input)
        {
            _tenantApplicationService.TenantUpdate(input);
            return this.Json("123123");
        }

        /// <summary>
        /// 帮助
        /// </summary>
        /// <returns></returns>
        public ActionResult FAQ()
        {
            return View();
        }

        /// <summary>
        /// 菜单树形展示
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("MenusManagement")]
        public ActionResult TreeMenus()
        {
            //List<Menu> list = _menuApplicationService.GetMenus();
            //ViewBag.TreeData = ProcessMenuStructure("0", list);           
            return View();
        }


        //public JsonResult GetOperFunction(string names)
        //{

        //    List<MenuDto> menuList = new List<MenuDto>();
        //    if (string.IsNullOrWhiteSpace(names))
        //        return this.Json("");
        //    List<string> namelist  = new List<string>();
        //    DataContractJsonSerializer _Json = new DataContractJsonSerializer(namelist.GetType());
        //    byte[] _Using = System.Text.Encoding.UTF8.GetBytes(names);
        //    System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
        //    _MemoryStream.Position = 0;
        //    try
        //    { 
        //        namelist = (List<string>)_Json.ReadObject(_MemoryStream);
        //    }
        //    catch (Exception)
        //    {
        //        namelist.Add(names.Replace("\"",""));
        //    }
        //    for (int i = 0; i < namelist.Count; i++)
        //    {

        //      string name = namelist[i].Split('(')[1].Replace(")", "");
        //        var temp = PermissionManager.GetAllPermissions().FirstOrDefault(dic => dic.Name == name).Children;

        //        MenuDto menuDto = new MenuDto();
        //        menuDto.promissName = new List<string>();
        //        menuDto.Name = L(name);//所属菜单名称
        //        if (temp != null && temp.Count > 0)               
        //        {
        //            menuDto.RequiredPermissionName = temp[0].Name;//菜单名称，唯一key
        //            foreach (var model in temp)
        //            {
        //                menuDto.promissName.Add(model.Name);//多语言化转换名称
        //                int k = model.Name.IndexOf('.');
        //                menuDto.LocalizableString += model.Name.Substring(k, model.Name.Length - k);//多语言化转换名称
        //            }
        //        }                     
        //        menuList.Add(menuDto);
        //    }
        //    return this.Json(menuList);
        //}




        public ActionResult TestTree()
        {
            return View();
        }

        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        //public async Task<JsonResult> SetUserPermission(string permission, string fatherpermission)
        //{
        //    var var = PermissionManager.GetPermissionOrNull("").Children;
        //    Users.User user = new Users.User() { Id = AbpSession.UserId.Value };
        //    List<Abp.Authorization.Permission> permissionList = new List<Abp.Authorization.Permission>();
        //    foreach (var str in permission.Split(','))
        //    {
        //        Abp.Authorization.Permission permissionModel = new Abp.Authorization.Permission(str);
        //        permissionList.Add(permissionModel);
        //    }
        //    //await _userManagerApplicationService.SetGrantedPermissionsAsync(user, permissionList);

        //    return this.Json("");
        //}

        /// <summary>
        /// 处理树形菜单权限
        /// </summary>
        /// <param name="name"></param>
        /// <param name="menus"></param>
        /// <returns></returns>

        //private string ProcessMenuStructure(string name, List<Menu> menus)
        //{
        //    string format = "'{0}': ! name: '{1}', type: '{2}' ~,";
        //    StringBuilder strs = new StringBuilder("var tree_data = {");
        //    StringBuilder nodeStrs = new StringBuilder();
        //    StringBuilder childStrs = new StringBuilder();
        //    foreach (var model in menus.FindAll(menu => menu.FatherCode == name))//一层菜单
        //    {
        //        if (menus.FindAll(menu => menu.FatherCode == model.RequiredPermissionName).Count > 0)
        //        {
        //            strs.AppendFormat(format, model.RequiredPermissionName, L(model.RequiredPermissionName) + "(" + model.RequiredPermissionName + ")", "folder");//中英文切换
        //            string temp = " tree_data['" + model.RequiredPermissionName + "']['additionalParameters'] = {'children' : {";
        //            foreach (var modelnode in menus.FindAll(menu => menu.FatherCode == model.RequiredPermissionName))//二层菜单
        //            {
        //                if (menus.FindAll(menu => menu.FatherCode == modelnode.RequiredPermissionName).Count > 0)//存在二级菜单
        //                {
        //                    temp += string.Format(format, modelnode.RequiredPermissionName, L(modelnode.RequiredPermissionName) + "(" + modelnode.RequiredPermissionName + ")", "folder");//中英文切换
        //                    string tempchild = " tree_data['" + model.RequiredPermissionName + "']['additionalParameters']['children']['" + modelnode.RequiredPermissionName + "']['additionalParameters'] = {'children' : {";
        //                    foreach (var modelchild in menus.FindAll(menu => menu.FatherCode == modelnode.RequiredPermissionName))//三层菜单
        //                    {
        //                        tempchild += string.Format(format, modelchild.RequiredPermissionName, L(modelchild.RequiredPermissionName) + "(" + modelchild.RequiredPermissionName + ")", "item");//目前系统支持到三级菜单，中英文切换
        //                    }
        //                    childStrs.Append(tempchild.Substring(0, tempchild.Length - 1) + "}};");
        //                }
        //                else//不存在二级菜单
        //                {
        //                    temp += string.Format(format, modelnode.RequiredPermissionName, L(modelnode.RequiredPermissionName) + "(" + modelnode.RequiredPermissionName + ")", "item");//中英文切换
        //                }
        //            }
        //            nodeStrs.Append(temp.Substring(0, temp.Length - 1) + "}};");
        //        }
        //        else
        //        {
        //            strs.AppendFormat(format, model.RequiredPermissionName, L(model.RequiredPermissionName) + "(" + model.RequiredPermissionName + ")", "item");//中英文切换
        //        }
        //    }
        //    return (strs.ToString().Substring(0, strs.ToString().Length - 1) + "};" + nodeStrs.ToString() + childStrs.ToString()).Replace("!", "{").Replace("~", "}");
        //}


        #endregion
    }
}