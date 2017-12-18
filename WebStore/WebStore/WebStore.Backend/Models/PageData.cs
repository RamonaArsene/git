using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Backend.Models
{
    public class PageData<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int TotalEntries { get; set; }
    }
}