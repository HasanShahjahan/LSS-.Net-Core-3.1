using LSS.HCM.Core.DataObjects.Settings;
using MongoDB.Driver;

namespace LSS.HCM.Core.Infrastructure.DbContext
{
    public class HardwareApiQueriesContext : IDatabaseQueriesContext
    {
        private readonly AppSettings _appSettings;
        public HardwareApiQueriesContext(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public IMongoDatabase MongoDbInitialization()
        {
            var client = new MongoClient(_appSettings.DatabaseSettings.ConnectionString);
            var database = client.GetDatabase(_appSettings.DatabaseSettings.DatabaseName);
            return database;
        }
    }
}
