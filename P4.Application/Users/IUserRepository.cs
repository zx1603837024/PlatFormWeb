using Abp.Domain.Repositories;
using P4.Users.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace P4.Users
{
    /// <summary>
    /// 构建ef框架支持sql语句执行接口
    /// </summary>
    public interface IUserRepository : IRepository<User, long>
    {
        int ExecuteSqlCommand(string sql, SqlParameter[] param);
        object SqlQuery();

        long UserInsert(User user);

        //GetAllUserOutput GetUserAll(UserInput input);
    }
}
