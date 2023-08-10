using Application.Interfaces;
using Infraestructure.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Context
{
    public class MongoContext<TEntity> : IMongoContext<TEntity> where TEntity : AuditableEntity<string>
    {
        private IMongoCollection<TEntity> _collection;
        private ICurrentUserService _currentUser { get; set; }

        public MongoContext(ICurrentUserService currentUser)
        {
            var appSettings = File.ReadAllText("./appsettings.json");

            JObject obj = JObject.Parse(appSettings);

            obj.TryGetValue("MongoDBConnection", out var connString);

            var mongoClient = new MongoClient(connString.Value<string>());

            var database = mongoClient.GetDatabase("Database");

            _collection = database.GetCollection<TEntity>(nameof(TEntity).Replace("E_", ""));

            _currentUser = currentUser;
        }


        public IMongoCollection<TEntity> Entities => _collection;

        public async Task InsertOne(TEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = _currentUser.Email;
            entity.CreatedByName = $"{_currentUser.Name} {_currentUser.SurName}";

            await _collection.InsertOneAsync(entity);
        }

        public async Task ReplaceOne(TEntity entity)
        {
            entity.LastModifiedOn = DateTime.Now;
            entity.LastModifiedBy = _currentUser.Email;
            entity.LastModifiedByName = $"{_currentUser.Name} {_currentUser.SurName}";

            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
