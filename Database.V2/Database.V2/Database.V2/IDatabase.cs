using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.V2
{
    public interface IDatabase
    {
        void Insert<T>(T entity);
        void Update<T>(T entity);
        void Delete(int id);
        T Get<T>(int id);
        IList<T> GetAll<T>();
    }
}
