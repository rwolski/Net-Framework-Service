using Framework;
using Framework.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Data
{
    public class MongoEntityStorage<T> : IEntityStorage<T> where T : Entity
    {
        readonly IMongoCollection<T> _collection;


        public MongoEntityStorage(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        //public void SaveUpdate(T entity)
        //{
        //    if (entity.Id == null)
        //    {
        //        entity.CreatedDateTime = DateTime.Now;
        //        entity.UpdatedDateTime = DateTime.Now;
        //        _collection.ReplaceOne(x => false, entity, new UpdateOptions() { IsUpsert = true });
        //    }
        //    else
        //    {
        //        entity.UpdatedDateTime = DateTime.Now;
        //        var update = BuildUpdatedFields(entity);
        //        if (update == null)
        //            return;

        //        //_collection.FindOneAndReplace<T>(x => x.Id == entity.Id, entity, new FindOneAndReplaceOptions<T, T>()
        //        _collection.FindOneAndUpdate<T>(x => x.Id == entity.Id, update, new FindOneAndUpdateOptions<T, T>()
        //        {
        //            IsUpsert = true
        //        });
        //    }
        //}

        public void Save(T entity)
        {
            if (entity.Id == null)
            {
                entity.CreatedDateTime = DateTime.Now;
                entity.UpdatedDateTime = DateTime.Now;
                _collection.InsertOne(entity);
            }
            else
            {
                entity.UpdatedDateTime = DateTime.Now;
                entity.Version++;
                _collection.FindOneAndReplace<T>(x => x.Id == entity.Id, entity, new FindOneAndReplaceOptions<T, T>()
                {
                    IsUpsert = true
                });
            }
        }

        public void Delete(T entity)
        {
            if (entity.Id == null)
                throw new InvalidOperationException("Cannot delete entities that haven't been persisted");

            Delete(entity.Id);
        }

        public void Delete(object id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }

        public T FindByIdentity(object id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public T FindFirstOrDefault(WhereCondition<T> whereConditions, IEnumerable<OrderBy<T>> orderBy = null)
        {
            return Find(whereConditions, orderBy, 1).FirstOrDefault();
        }

        public IEnumerable<T> FindAll()
        {
            return _collection.Find(x => true).ToEnumerable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> whereConditions)
        {
            if (whereConditions == null)
                throw new ArgumentNullException("whereConditions");

            return _collection.Find(whereConditions).ToEnumerable();
        }

        public IEnumerable<T> Find(WhereCondition<T> whereConditions, IEnumerable<OrderBy<T>> orderBy = null,
            int? limit = null)
        {
            IFindFluent<T, T> result;

            if (whereConditions != null)
                result = _collection.Find(whereConditions);
            else
                result = _collection.Find(new BsonDocument());

            if (orderBy != null)
            {
                foreach (var statement in orderBy)
                {
                    if (statement.Ascending)
                        result = result.SortBy(statement.Exp);
                    else
                        result = result.SortByDescending(statement.Exp);
                }
            }

            if (limit.HasValue)
                result.Limit(limit);

            return result.ToEnumerable();
        }

        private UpdateDefinition<T> BuildUpdatedFields(T entity)
        {
            UpdateDefinitionBuilder<T> builder = Builders<T>.Update;

            var props = typeof(T).GetProperties().Select(x => new
            {
                Property = x,
                EntityMapping = x.GetCustomAttributes(typeof(EntityFieldAttribute), true).Cast<EntityFieldAttribute>().FirstOrDefault()
            }).Where(x => x.EntityMapping != null);

            UpdateDefinition<T> result = builder.ToBsonDocument();

            foreach (var prop in props)
            {
                var field = prop.EntityMapping.FieldName ?? prop.Property.Name;
                var val = prop.Property.GetValue(entity);
                if (!prop.EntityMapping.Incrementing)
                    result = result.Set(field, val);
                else
                    result = result.Inc(field, 1);
            }

            return result;
        }
    }
}
