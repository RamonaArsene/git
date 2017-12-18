using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebStore.Nh;
using WebStore.Common.Entities;
using NHibernate.Linq;
using WebStore.Backend.Models;

namespace WebStore.Backend.Controllers
{
    public class OrderController : ApiController
    {
        private static string connectionString =
           @"Data Source=INTERN2017-41;Initial Catalog=webstore;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;


        static OrderController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }

        [HttpGet]
        public IEnumerable<Order> GetAllOrders()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var orders = session.Query<Order>().ToList();

                foreach (var order in orders)
                {
                    foreach (var detail in order.OrderLines)
                    {
                        detail.Order = null;
                    }
                }

                return orders;
            }
        }
        [HttpGet]
        public PageData<Order> GetOrderLinesPage(int pageNumber, int pageSize)
        {
            PageData<Order> orderPage = new PageData<Order>();
            using (var session = sessionFactory.OpenSession())
            {
                var orderQuery = session.Query<Order>();

                orderPage.TotalEntries = orderQuery.Count();
                orderPage.Total = this.getTotalPages(pageSize, orderPage.TotalEntries);
                orderPage.Data = orderQuery.Skip(pageNumber * pageSize)
                                          .Take(pageSize)
                                          .ToList();

                foreach (var order in orderPage.Data)
                {
                    order.OrderLines = session.Query<OrderDetail>().Where(od => od.Order.Id == order.Id).ToList();
                    foreach (var orderDetail in order.OrderLines)
                    {
                        orderDetail.TotalPriceWithoutDiscount = orderDetail.Quantity * orderDetail.Product.ListPrice;
                        orderDetail.TotalPrice = orderDetail.TotalPriceWithoutDiscount - orderDetail.TotalPriceWithoutDiscount * (orderDetail.Order.Customer.DiscountPercent / 100);
                        orderDetail.Order = null;
                    }

                }
                return orderPage;
            }
        }
        [HttpGet]
        public OrderDetail FindOrderDetailById(int orderDetailId)
        {
            OrderDetail oDetail = new OrderDetail();
            using (var session = sessionFactory.OpenSession())
            {
                var res = session.Query<OrderDetail>().Where(od => od.Id == orderDetailId).SingleOrDefault();
                if (res != null)
                {
                    oDetail = res;
                }
                oDetail.Order = null;
                return oDetail;
            }
        }

        public int GetTotalPages(int pageSize)
        {
            using (var session = sessionFactory.OpenSession())
            {

                var pageTotal = session.Query<Order>().Count();

                var shouldAddOne = !(pageTotal % pageSize == 0);
                if (shouldAddOne)
                    pageTotal = pageTotal / pageSize + 1;
                else
                    pageTotal = pageTotal / pageSize;
                return pageTotal;
            }
        }
        [HttpGet]
        public List<OrderDetail> FindOrderDetailsByCustomer(string customerId)
        {

            int id = int.Parse(customerId);
            using (var session = sessionFactory.OpenSession())
            {
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                var orders = session.Query<Order>().Where(od => od.Customer.Id == id).ToList();
                foreach (var order in orders)
                {
                    foreach (var detail in order.OrderLines)
                    {
                        detail.Order = null;
                    }
                    orderDetails.AddRange(order.OrderLines);
                }

                return orderDetails;

            }
        }

        [HttpGet]
        public List<OrderDetail> FindOrderDetails(string orderId)
        {
            List<OrderDetail> currentOrderDetails = new List<OrderDetail>();
            int id = int.Parse(orderId);
            using (var session = sessionFactory.OpenSession())
            {
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                currentOrderDetails = session.Query<OrderDetail>().Where(od => od.Order.Id == id).ToList();
                foreach (var orderDetail in currentOrderDetails)
                {
                    orderDetail.TotalPriceWithoutDiscount = orderDetail.Quantity * orderDetail.Product.ListPrice;
                    orderDetail.TotalPrice = orderDetail.TotalPriceWithoutDiscount - orderDetail.TotalPriceWithoutDiscount * (orderDetail.Order.DiscountPercent / 100);

                    orderDetail.Order = null;
                }
                return currentOrderDetails;
            }
        }

        [HttpGet]
        public Order GetOrderById(string orderId)
        {
            int id = int.Parse(orderId);
            using (var session = sessionFactory.OpenSession())
            {
                var order = session.Query<Order>().Where(od => od.Id == id).SingleOrDefault();

                foreach (var detail in order.OrderLines)
                {
                    detail.Order = null;
                }
                return order;
            }
        }

        [HttpGet]
        public Customer FindCustomerById(string customerId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                int id = int.Parse(customerId);
                Customer res = new Customer();
                var w = session.Query<Order>().Where(o => o.Customer.Id == id).Take(1).SingleOrDefault();

                if (w != null)
                    res = w.Customer;

                return res;
            }
        }

        public Product FindProductById(int productId)
        {
            //int id = int.Parse(orderId);
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<OrderDetail>().Where(c => c.Product.Id == productId).SingleOrDefault().Product;
            }
        }

        public PageData<Order> GetOrderPages(int pageNumber, int pageSize)
        {
            PageData<Order> orderPageData = new PageData<Order>();

            using (var session = sessionFactory.OpenSession())
            {

                var query = session.Query<Order>();

                orderPageData.TotalEntries = session.Query<Order>().Count();
                orderPageData.Total = this.getTotalPages(pageSize, orderPageData.TotalEntries);
                orderPageData.Data = query.Skip(pageNumber * pageSize)
                                          .Take(pageSize)
                                          .ToList();
                foreach (var order in orderPageData.Data)
                {
                    foreach (var detail in order.OrderLines)
                    {
                        detail.Order = null;
                    }
                }
                return orderPageData;
            }
        }


        [HttpPost]
        public InsertOrder AddOrder(InsertOrder insertOrder)
        {
            using (var session = sessionFactory.OpenSession())
            {
                insertOrder.order.OrderDate = DateTime.Now;
                var lines = insertOrder.details;
                insertOrder.order.OrderLines = new List<OrderDetail>();
                session.SaveOrUpdate(insertOrder.order);
                session.Flush();
                foreach (var orderDetail in insertOrder.details)
                {
                    orderDetail.Order = insertOrder.order;
                    //insertOrder.order.OrderLines.Add(orderDetail);   
                    session.SaveOrUpdate(orderDetail);
                }
                //session.SaveOrUpdate(insertOrder.order);
                session.Flush();
                foreach (var detail in insertOrder.order.OrderLines)
                {
                    detail.Order = null;
                }
                return insertOrder;
            }
        }

        [HttpPost]
        public OrderDetail DeleteOrderDetail(OrderDetail orderDetail)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Delete(orderDetail);
                session.Flush();
                return orderDetail;
            }
        }

        [HttpPost]
        public Order DeleteOrder(Order order)
        {
            using (var session = sessionFactory.OpenSession())
            {
              
                session.Delete(order);
                session.Flush();
                return order;
            }
        }

        [HttpPost]
        public OrderDetail AddOrderDetail(OrderDetail orderDetail)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(orderDetail);
                session.Flush();
                return orderDetail;
            }
        }

        [HttpGet]
        public int GetLastOrderDetailId()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<OrderDetail>().Max(od => od.Id);
                return query + 1;
            }
        }

        [HttpGet]
        public PageData<Order> Filter(int? custId, int? startDiscount, int? endDiscount, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize)
        {
            PageData<Order> orderPageData = new PageData<Order>();

            if (custId == null && endDiscount == null && startDiscount == null && startDate == null && endDate == null)
            {
                orderPageData = this.GetOrderPages(pageNumber, pageSize);
                return orderPageData;
            }
            else
            {
                if (startDiscount == null)
                    startDiscount = 0;

                if (startDiscount >= 100)
                    startDiscount = 101;
        
                if (endDiscount == null)
                    endDiscount = 100;

                if (startDiscount < 0)
                    startDiscount = -1;

                if (startDate == null)
                    startDate = DateTime.Parse("1/1/2000");
                //else
                //    startDate = DateTime.Parse(startDate.ToString());

                if (endDate == null)
                    endDate = DateTime.Now;
                else
                    endDate = DateTime.Parse(endDate.ToString()).AddDays(1);


                using (var session = sessionFactory.OpenSession())
                {
                    var query = session.Query<Order>();

                    if (custId != null)
                    {
                        query = query.Where(or => or.Customer.Id == custId && or.DiscountPercent >= startDiscount && or.DiscountPercent <= endDiscount && or.OrderDate >= startDate && or.OrderDate <= endDate.Value);
                    }
                    else
                        query = query.Where(or => or.DiscountPercent >= startDiscount && or.DiscountPercent <= endDiscount && or.OrderDate >= startDate && or.OrderDate<= endDate.Value);
                    
                    orderPageData.TotalEntries = query.Count();
                    orderPageData.Total = this.getTotalPages(pageSize, query.Count());
                    query = query.Skip(pageNumber * pageSize)
                                 .Take(pageSize);
                    orderPageData.Data = query.ToList();

                    foreach (var order in orderPageData.Data)
                    {
                        foreach (var detail in order.OrderLines)
                        {
                            detail.Order = null;
                        }
                    }
                    return orderPageData;
                }
            }
        }
    


            public int getTotalPages(int pageSize, int entries)
        {
            int totalPages;
            var shouldAddOne = !(entries % pageSize == 0);
            if (shouldAddOne)
                totalPages = entries / pageSize + 1;
            else
                totalPages = entries / pageSize;
            return totalPages;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<Product>().ToList();
            }

        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            IEnumerable<Order> orders = this.GetAllOrders();

            foreach (var order in orders)
            {
                customers.Add(order.Customer);
            }
            return customers;
        }

        public IEnumerable<Customer> GetAllCustomersTotal()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<Customer>().ToList();
            }


        }

    }
}