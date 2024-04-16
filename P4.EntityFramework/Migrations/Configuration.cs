using System.Data.Entity.Migrations;

namespace P4.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<P4.EntityFramework.P4DbContext>
    {
        /// <summary>
        /// 
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
            ContextKey = "P4";
        }

        /// <summary>
        /// 创建数据库脚本
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(P4.EntityFramework.P4DbContext context)
        {
            //写入基础数据
            //new InitialDataBuilder().Build(context);
        }
    }
}
