using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using WebStore.Common.Models;

namespace WebStore.Common.Services
{
    public interface IOrderService : IEntityService<Order>
    {
        OrderSummary[] ReportByDateRange(DateTime startDate, DateTime endDate);
    }
}
