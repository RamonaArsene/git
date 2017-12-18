using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Backend.Models
{
    public class InsertOrderPages
    {

        public IEnumerable<InsertOrder> orders { get; set; }
        public int totalPages { get; set; }
        public int totalEntries { get; set; }

    }
}