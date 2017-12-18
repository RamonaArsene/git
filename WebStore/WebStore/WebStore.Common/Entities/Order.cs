using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Common.Entities
{
    public class Order : IEntity
    {
        public virtual int Id { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual decimal DiscountPercent { get; set; }

        public virtual IList<OrderDetail> OrderLines { get; set; }
       
    }
}
