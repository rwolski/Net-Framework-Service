using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Data
{
    public interface IEntityStorage<T> where T : Entity
    {
        void Save(T entity);

        void Delete(T entity);

        void Delete(object id);

        T FindByIdentity(object id);

        T FindFirstOrDefault(WhereCondition<T> whereConditions = null, IEnumerable<OrderBy<T>> orderBy = null);

        IEnumerable<T> FindAll();

        IEnumerable<T> Find(WhereCondition<T> whereConditions = null, IEnumerable<OrderBy<T>> orderBy = null, int? limit = null);

    };

    public class WhereCondition<T> where T : Entity
    {
        public Expression<Func<T, bool>> Exp { get; set; }

        public WhereCondition(Expression<Func<T, bool>> exp)
        {
            Exp = exp;
        }

        public static implicit operator WhereCondition<T>(Expression<Func<T, bool>> exp)
        {
            return new WhereCondition<T>(exp);
        }

        public static implicit operator Expression<Func<T, bool>>(WhereCondition<T> exp)
        {
            return exp.Exp;
        }
    };

    public class OrderBy<T> where T : Entity
    {
        public Expression<Func<T, object>> Exp { get; set; }

        public bool Ascending { get; set; }
    };
}
