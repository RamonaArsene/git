using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;

namespace WebStore.Common.Services
{
    public interface IEntityService<T>
        where T : IEntity
    {
        void Save(T entity);

        T GetById(int id);

        void Delete(T entity);

        List<T> GetAll();
    }
}
