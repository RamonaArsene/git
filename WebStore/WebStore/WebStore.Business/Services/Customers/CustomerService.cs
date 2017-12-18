using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using WebStore.Common.Services;

namespace WebStore.Business.Services
{
    class CustomerService<T> : ICustomerService
    {
        private ISession session;

        public CustomerService(ISession session)
        {
            this.session = session;
        }

        public void Delete(Customer entity)
        {

            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer entity)
        {
            throw new NotImplementedException();
        }
    }

}
