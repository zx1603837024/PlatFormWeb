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
        /// �������ݿ�ű�
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(P4.EntityFramework.P4DbContext context)
        {
            //д���������
            //new InitialDataBuilder().Build(context);
        }
    }
}
