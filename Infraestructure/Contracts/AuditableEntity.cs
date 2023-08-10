using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Contracts
{
    public abstract class AuditableEntity<TId>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public TId Id { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedByName { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
