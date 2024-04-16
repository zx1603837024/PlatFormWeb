using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using P4.MongoApplication;
using P4.MongoCore;

namespace P4.MongoDB.MongoDB.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class PictureRepository : MongoDbRepositoryBase<MBusinessDetailPicture>, IPictureRepository
    {
        public PictureRepository(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {

        }
    }
}
