using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Common.Entities
{
    public class Customer : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal DiscountPercent { get; set; }
    }
}
