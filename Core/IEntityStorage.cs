using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IEntityStorage<T>
    {
        void Save(T entity);

        void Delete(T entity);

        void Delete(long id);

        T GetByIdentity(long id);
    }
}
