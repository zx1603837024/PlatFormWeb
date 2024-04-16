using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace P4.EntityFramework.Repositories
{
    public abstract class P4RepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<P4DbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected P4RepositoryBase(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class P4RepositoryBase<TEntity> : P4RepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected P4RepositoryBase(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
