
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Common.Entities;

namespace WebStore.Backend.Models
{
    public class InsertOrder
    {
        public Order order;
        public OrderDetail[] details;
    }
}