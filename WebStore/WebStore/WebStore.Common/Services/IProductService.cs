using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;

namespace WebStore.Common.Services
{
    public interface IProductService : IEntityService<Product>
    {
        Product GetByName(string name);
    }
}
