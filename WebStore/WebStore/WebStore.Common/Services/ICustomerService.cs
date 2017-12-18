using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;

namespace WebStore.Common.Services
{
    public interface ICustomerService : IEntityService<Customer>
    {
        Customer GetByName(string name);
    }
}
