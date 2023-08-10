using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMongoContext<TEntity>
    {
        public IMongoCollection<TEntity> Entities { get; }
        public Task InsertOne(TEntity entity);
        public Task ReplaceOne(TEntity entity);
    }
}
