using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Microsoft.AspNet.Identity;
using P4.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Users
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ListResultOutput<UserDto> GetUsers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllUserOutput GetAllUsers(UserInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Update();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        int UpdateImage(byte[] image);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object SqlQuery();

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(UserDto model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Add(UserDto entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(UserDto entity);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        User GetUserInfo(long Id);
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ICollection<UserLogin> Logins(long Id);

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        Task<IdentityResult> ChangePasswordAsync(User user, string newPassword);

        /// <summary>
        /// 通过邮件重置密码
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="Code"></param>
        void ResetPasswordAsync(string emailAddress, string Code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permissionlist"></param>
        /// <returns></returns>
        Task SavePermission(User user, List<Permission> permissionlist);
    }
}
