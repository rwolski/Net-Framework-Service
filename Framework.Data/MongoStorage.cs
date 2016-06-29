using MongoDB.Bson;
using MongoDB.Driver;

namespace Framework.Data
{
    public class MongoStorage : IStorage
    {
        readonly IMongoCollection<BsonDocument> _collection;

        public MongoStorage(IMongoCollection<BsonDocument> collection)
        {
            _collection = collection;
        }

        //public void Save(BsonDocument obj)
        //{
        //    var builder = Builders<BsonDocument>.Filter.Eq("_id", obj._)
        //    _collection.FindOneAndUpdate<BsonDocument>(x => x.Id == entity.Id, update, new FindOneAndUpdateOptions<T, T>()
        //    {
        //        IsUpsert = true
        //    });
        //}

        //public void Delete(T entity)
        //{
        //    if (entity.Id == null)
        //        throw new InvalidOperationException("Cannot delete entities that haven't been persisted");

        //    Delete(entity.Id);
        //}

        //public void Delete(object id)
        //{
        //    _collection.DeleteOne(x => x.Id == id);
        //}

        //public T FindByIdentity(object id)
        //{
        //    return _collection.Find(x => x.Id == id).FirstOrDefault();
        //}

        //public T FindFirstOrDefault(WhereCondition<T> whereConditions, IEnumerable<OrderBy<T>> orderBy = null)
        //{
        //    return Find(whereConditions, orderBy, 1).FirstOrDefault();
        //}

        //public IEnumerable<T> FindAll()
        //{
        //    return _collection.Find(x => true).ToEnumerable();
        //}

        //public IEnumerable<T> Find(Expression<Func<T, bool>> whereConditions)
        //{
        //    if (whereConditions == null)
        //        throw new ArgumentNullException("whereConditions");

        //    return _collection.Find(whereConditions).ToEnumerable();
        //}

        //public IEnumerable<T> Find(WhereCondition<T> whereConditions, IEnumerable<OrderBy<T>> orderBy = null,
        //    int? limit = null)
        //{
        //    var result = _collection.Find(whereConditions);
        //    if (orderBy != null)
        //    {
        //        foreach (var statement in orderBy)
        //        {
        //            if (statement.Ascending)
        //                result = result.SortBy(statement.Exp);
        //            else
        //                result = result.SortByDescending(statement.Exp);
        //        }
        //    }

        //    if (limit.HasValue)
        //        result.Limit(limit);

        //    return result.ToEnumerable();
        //}
    }
}