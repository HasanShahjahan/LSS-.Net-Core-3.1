using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Infrastructure.DbContext;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace LSS.HCM.Core.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDatabaseQueriesContext _databaseQueriesContext;
        private readonly AppSettings _appSettings;
        public Repository(IDatabaseQueriesContext databaseQueriesContext, AppSettings appSettings) 
        {
            _databaseQueriesContext = databaseQueriesContext;
            _appSettings = appSettings;
        }
        public IMongoCollection<T> Get()
        {
            var database = _databaseQueriesContext.MongoDbInitialization();
            var result =  database.GetCollection<T>(_appSettings.DatabaseSettings.CollectionName);
            return result;
        }
        public IMongoCollection<T> Get(Expression<Func<T, bool>> predicate)
        {
            var database = _databaseQueriesContext.MongoDbInitialization();
            return database.GetCollection<T>(_appSettings.DatabaseSettings.CollectionName);
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
