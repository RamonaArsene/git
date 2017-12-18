using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Common.Models
{
    public class ProductSummary
    {
        public int Id { get; set; }

        public int OrdersCount { get; set; }

        public decimal TotalQuantityOrdered { get; set; }

        public decimal TotalAmountCharged { get; set; }
    }
}
