using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace LSS.HCM.Core.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IMongoCollection<T> Get();
        IMongoCollection<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
