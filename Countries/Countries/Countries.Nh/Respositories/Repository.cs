using Countries.Repositories;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Nh.Respositories
{
    public class Repository<T> : IRepository<T>
    {
        private ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public void Delete(T entity)
        {
            session.Delete(entity);
            session.Flush();
        }

        public IList<T> GetAll()
        {
            return session.Query<T>().ToList();
        }

        public T GetById(object id)
        {
            return session.Get<T>(id);
        }

        public void Insert(T entity)
        {
            session.Save(entity);
            session.Flush();
        }

        public void Update(T entity)
        {
            session.Update(entity);
            session.Flush();
        }
    }
}
