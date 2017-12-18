using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Repositories
{
    public interface IRepository<T>
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        T GetById(object id);

        IList<T> GetAll();
    }
}
