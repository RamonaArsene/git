using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Common.Models
{
    public class OrderSummary
    {
        public int Id { get; set; }

        public int LinesCount { get; set; }

        public decimal TotalPriceWithoutDiscount { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
