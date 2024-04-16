using Abp.Authorization.Users;
using Abp.EntityFramework;
using P4.Authorization;
using P4.Users;
using P4.Users.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;

namespace P4.EntityFramework.Repositories
{
    public class UserRepository : P4RepositoryBase<User, long>, IUserRepository
    {
        public UserRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        { 
            
        }

        public int ExecuteSqlCommand(string sql, SqlParameter[] param)
        {
            return Context.Database.ExecuteSqlCommand(sql, param);
        }



        //public GetAllUserOutput GetUserAll(UserInput input)
        //{
           
        //    int records = Table.Count();
            
        //    return new GetAllUserOutput()
        //    {
        //        rows = Table.Select(c => new UserDto
        //            {
        //                Id = c.Id,
        //                UserName = c.UserName,
        //                Name = c.Name,
        //                Surname = c.Surname,
        //                EmailAddress = c.EmailAddress,
        //                //TenantId = c.TenantId,
        //                TenantName = c.Tenant.Name,
        //                IsEmailConfirmed = c.IsEmailConfirmed,
        //                IsActive = c.IsActive,
        //                Gender = c.Gender,
        //                LastModificationTime = c.LastModificationTime,
        //                LastLoginTime = c.LastLoginTime,
        //                //LastModifierUserId = c.LastModifierUserId,
        //                //RoleId = Context.Set<UserRole>().FirstOrDefault(roleid => roleid.UserId == c.Id).RoleId == null ? Context.Set<UserRole>().FirstOrDefault().RoleId:
        //                //Context.Set<UserRole>().FirstOrDefault(roleid => roleid.UserId == c.Id).RoleId
        //                LastModifierUserName=c.LastModifierUser.Name,
        //                Roles= c.Roles
                      
        //                //RoleDisplayName=c.Roles.Where(role=>role.UserId==c.Id).t
        //                //RoleDisplayName =Context.Set<Role>().FirstOrDefault(role=>role.Id==( Context.Set<UserRole>().FirstOrDefault(roleid => roleid.UserId == c.Id).RoleId)).Name == null ?
        //                //                 Context.Set<Role>().FirstOrDefault(role=>role.Id==( Context.Set<UserRole>().FirstOrDefault().RoleId)).Name :
        //                //                 Context.Set<Role>().FirstOrDefault(role => role.Id == (Context.Set<UserRole>().FirstOrDefault(roleid => roleid.UserId == c.Id).RoleId)).Name
        //            }).OrderByDescending(user => user.Id).PageBy(input).ToList(),
        //        records = records,
        //        total = records / input.rows + 1
        //    };
        //}

        public object SqlQuery()
        {
            return Context.Database.SqlQuery(typeof(DateTime), "select getdate()", new SqlParameter[] { });
        }



        public long UserInsert(User user)
        {

            return Context.Database.ExecuteSqlCommand("", null);
        }
    }
}
