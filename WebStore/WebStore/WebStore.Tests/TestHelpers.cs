using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using WebStore.Common.Services;

namespace WebStore.Tests
{
    public class TestHelpers
    {
        public static void Cleanup<T>(IEntityService<T> service)
            where T : IEntity
        {
            var all = service.GetAll();

            foreach (var entity in all)
            {
                service.Delete(entity);
            }
        }
    }
}
