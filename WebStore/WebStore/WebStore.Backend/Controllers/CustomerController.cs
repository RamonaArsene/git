using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Common.Entities;
using NHibernate;
using NHibernate.Linq;
using System.Web.Http;
using WebStore.Nh;
using WebStore.Backend.Models;

namespace WebStore.Backend.Controllers
{
    public class CustomerController : ApiController
    {
        private static string connectionString =
           @"Data Source=INTERN2017-41;Initial Catalog=webstore;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;


        static CustomerController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }


        public int GetTotalPages(int pageSize)
        {

            using (var session = sessionFactory.OpenSession())
            {

                var pageTotal = session.Query<Customer>().Count();

                var shouldAddOne = !(pageTotal % pageSize == 0);
                if (shouldAddOne)
                    pageTotal = pageTotal / pageSize + 1;
                else
                    pageTotal = pageTotal / pageSize;
                return pageTotal;
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

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<Customer>().ToList();
            }
        }

        [HttpGet]
        public int CountAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<Customer>().Count();
            }
        }


        public IEnumerable<Customer> GetCustomerPages(int pageNumber, int pageSize)
        {

            using (var session = sessionFactory.OpenSession())
            {

                var query = session.Query<Customer>()
                                .Skip(pageNumber * pageSize)
                                .Take(pageSize);


                return query.ToList();

            }
        }


        [HttpPost]
        public Customer SaveCustomer(Customer newCustomer)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Save(newCustomer);
                session.Flush();
                return newCustomer;
            }


        }

        [HttpDelete]
        public Customer DeleteCustomer([FromUri] int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var customer = session.Query<Customer>().SingleOrDefault(c => c.Id == id);
                session.Delete(customer);
                session.Flush();
                return customer;
            }
        }

        [HttpPost]
        public Customer UpdateCustomer(Customer newCustomer)
        {
            using (var session = sessionFactory.OpenSession())
            {

                session.Update(newCustomer);
                session.Flush();
            }
            return newCustomer;
        }

        [HttpGet]
        public Customer FindCustomerById(string customerId)
        {
            int id = int.Parse(customerId);
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<Customer>().Where(c => c.Id == id).SingleOrDefault();
            }
        }

        [HttpGet]
        public string GetNumberOfOrders(string customerId)
        {
            int id = int.Parse(customerId);
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>().Where(o => o.Customer.Id == id);
                return query.Count().ToString();
            }
        }

        [HttpGet]
        public string GetTotalAmount(string customerId)
        {
            Customer customer = this.FindCustomerById(customerId);
            int id = int.Parse(customerId);
            decimal total = 0;
            using (var session = sessionFactory.OpenSession())
            {
                List<Order> customerOrders = new List<Order>();
                customerOrders = session.Query<Order>().Where(o => o.Customer.Id == id).ToList();

                foreach (var order in customerOrders)
                {
                    foreach (var orderLine in order.OrderLines)
                        total = total + (orderLine.Product.ListPrice * orderLine.Quantity);
                }
            }
            total = total - (customer.DiscountPercent/100) * total;
            return total.ToString();
        }


        [HttpGet]
        public string GetLastDate(string customerId)
        {
            int id = int.Parse(customerId);

            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>().Where(o => o.Customer.Id == id).OrderByDescending( o => o.OrderDate).Take(1).SingleOrDefault();
                return query.OrderDate.ToString();
            }
        }


        [HttpGet]
        public PageData<Customer> Filter(string nameFilter, int? startDiscount, int? endDiscount, int pageNumber, int pageSize)
        {
            PageData<Customer> filteredCustomers = new PageData<Customer>();

            if (nameFilter=="undefined" && startDiscount == 0 && endDiscount == 100)
            {
                filteredCustomers.Data = GetCustomerPages(pageNumber, pageSize);
                filteredCustomers.Total = GetTotalPages(pageSize);
                filteredCustomers.TotalEntries = CountAll();
                return filteredCustomers;
            }

            else
            {
                if (startDiscount == null)
                {
                    startDiscount = 0;
                }
                if (startDiscount >= 100)
                    startDiscount = 101;
                   
                if (endDiscount == null)
                {
                    endDiscount = 100;
                }

                if(startDiscount < 0)
                {
                    startDiscount = -1;
                }
                using (var session = sessionFactory.OpenSession())
                {
                    var query = session.Query< Customer >();
                    if (nameFilter != "undefined")
                    {
                        query = query.Where(c => c.Name.ToUpper().StartsWith(nameFilter.ToUpper()) && c.DiscountPercent >= startDiscount && c.DiscountPercent <= endDiscount);
                    }
                    else
                    {
                        query = query.Where(c => c.DiscountPercent >= startDiscount && c.DiscountPercent <= endDiscount);
                    }

                    filteredCustomers.Total = getTotalPages(pageSize, query.Count());
                    filteredCustomers.TotalEntries = query.Count();
                    filteredCustomers.Data = query.Skip(pageNumber * pageSize)
                                .Take(pageSize).ToList();

                    return filteredCustomers;
                }

            }
        }
    }
}