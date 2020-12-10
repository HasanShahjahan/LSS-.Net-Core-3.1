using MongoDB.Driver;

namespace LSS.HCM.Core.Infrastructure.DbContext
{
    public interface IDatabaseQueriesContext
    {
        IMongoDatabase MongoDbInitialization();
    }
}
