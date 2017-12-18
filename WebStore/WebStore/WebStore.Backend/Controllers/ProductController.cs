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
    public class ProductController : ApiController
    {
        private static string connectionString =
           @"Data Source=INTERN2017-41;Initial Catalog=webstore;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;


        static ProductController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }


        public int GetTotalPages(int pageSize)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var pageTotal = session.Query<Product>().Count();

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

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var x = session.Query<Product>().ToList();
                return x;
            }
        }

        [HttpGet]
        public int CountAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<Product>().Count();
            }
        }

        [HttpGet]
        public IEnumerable<Product> GetProductPages(int pageNumber, int pageSize)
        {
            using (var session = sessionFactory.OpenSession())
            {

                var query = session.Query<Product>()
                                .Skip(pageNumber * pageSize)
                                .Take(pageSize);
                return query.ToList();
            }
        }

        [HttpPost]
        public Product SaveProduct(Product newProduct)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Save(newProduct);
                session.Flush();
                return newProduct;
            }


        }

        [HttpDelete]
        public Product DeleteProduct([FromUri] int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var Product = session.Query<Product>().SingleOrDefault(c => c.Id == id);
                session.Delete(Product);
                session.Flush();
                return Product;
            }
        }

        [HttpPost]
        public Product UpdateProduct(Product newProduct)
        {
            using (var session = sessionFactory.OpenSession())
            {

                session.Update(newProduct);
                session.Flush();
            }
            return newProduct;
        }

        [HttpGet]
        public Product FindProductById(string ProductId)
        {
            int id = int.Parse(ProductId);
            using (var session = sessionFactory.OpenSession())
            {
                Product res = new Product();
                var w = session.Query<OrderDetail>().Where(od => od.Product.Id == id).ToList();
                foreach(var prod in w)
                {
                    if (w != null)
                        res = prod.Product;
                }
                
                return res;

            }
        }


        [HttpGet]
        public string GetNumberOfOrders(string productId)
        {
            int id = int.Parse(productId);
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<OrderDetail>().Where(od => od.Product.Id == id);
                return query.Count().ToString();
            }
        }

        [HttpGet]
        public string GetTotalQuantity(string productId)
        {
            int id = int.Parse(productId);
            using (var session = sessionFactory.OpenSession())
            {
                int query = (int) session.Query<OrderDetail>().Where(od => od.Product.Id == id).Sum(od => od.Quantity);
                return query.ToString();
            }
        }


        [HttpGet]
        public string GetTotalAmount(string productId)
        {
            Product customer = this.FindProductById(productId);
            int id = int.Parse(productId);
            decimal totalPerCustomer = 0;
            decimal totalPerProduct = 0;
            using (var session = sessionFactory.OpenSession())
            {
                List<Order> customerOrders = new List<Order>();
                customerOrders = session.Query<Order>().ToList();

                foreach (var order in customerOrders)
                {
                    totalPerCustomer = 0;
                    foreach (var orderLine in order.OrderLines)
                    {
                        if(orderLine.Product.Id == id)
                        {
                            totalPerCustomer = totalPerCustomer + (orderLine.Product.ListPrice * orderLine.Quantity);
                        }
                       
                    }
                    totalPerProduct = totalPerProduct + (totalPerCustomer - totalPerCustomer * (order.Customer.DiscountPercent/100));
                }
            }
            return totalPerProduct.ToString();
        }

        [HttpGet]
        public PageData<Product> Filter(string nameFilter, int? startPrice, int? endPrice, int pageNumber, int pageSize)
        {
            PageData<Product> filteredProducts = new PageData<Product>();
            if ((nameFilter == "undefined") && startPrice == null && endPrice == null)
            {
                filteredProducts.Data = GetProductPages(pageNumber, pageSize);
                filteredProducts.Total = GetTotalPages(pageSize);
                filteredProducts.TotalEntries = CountAll();
                return filteredProducts;
            }

            else
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var query = session.Query<Product>();

                    if (nameFilter != "undefined")
                    {
                        query = query.Where(c => c.Name.ToUpper().StartsWith(nameFilter.ToUpper()));

                        if (startPrice != null)
                        {
                            query = query.Where(c => c.ListPrice >= startPrice);

                            if (endPrice != null)
                            {
                                query = query.Where(c => c.ListPrice <= endPrice);
                            }
                        }
                        else
                        {
                            if (endPrice != null)
                            {
                                query = query.Where(c => c.ListPrice <= endPrice);
                            }
                        }
                    }

                    else
                    {
                        if (startPrice != null)
                        {
                            query = query.Where(c => c.ListPrice >= startPrice);

                            if (endPrice != null)
                            {
                                query = query.Where(c => c.ListPrice <= endPrice);
                            }
                        }
                        else
                        {
                            if (endPrice != null)
                            {
                                query = query.Where(c => c.ListPrice <= endPrice);
                            }
                        }
                    }

                    filteredProducts.Total = getTotalPages(pageSize, query.Count());
                    filteredProducts.TotalEntries = query.Count();
                    filteredProducts.Data = query.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                    return filteredProducts;
                }
            }
        }

    }

}