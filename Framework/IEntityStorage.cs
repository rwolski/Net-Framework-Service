using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IEntityStorage<T>
    {
        void Save(T entity);

        void Delete(T entity);

        void Delete(object id);

        T FindByIdentity(object id);

        IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> expression);

        IEnumerable<T> FindAll();
    }
}
