using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Common.Entities
{
    public class OrderDetail : IEntity
    {
        public virtual int Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual decimal TotalPriceWithoutDiscount { get; set; }
        public virtual decimal TotalPrice { get; set; }
        //public virtual Order OrderLines { get; set; }
    }
}
