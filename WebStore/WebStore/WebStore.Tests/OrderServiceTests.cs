using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Common.Services;
using WebStore.Common.Entities;
using System.Collections.Generic;

namespace WebStore.Tests
{
    [TestClass]
    public class OrderServiceTests : EntityServiceTests<Order, IOrderService>
    {
        private string connectionString = @"Data Source=.;Initial Catalog=WebStore;User ID=sa;Password=1234%asd";

        private ICustomerService CreateCustomerService()
        {
            throw new NotImplementedException();
        }

        private IProductService CreateProductService()
        {
            throw new NotImplementedException();
        }

        protected override IOrderService CreateTarget()
        {
            throw new NotImplementedException();
        }

        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();
            // create database connection

            // cleanup all test data.... best to cleanup entire database
            var customerService = CreateCustomerService();
            var productService = CreateProductService();
            var orderService = CreateTarget();

            TestHelpers.Cleanup(orderService);
            TestHelpers.Cleanup(productService);
            TestHelpers.Cleanup(customerService);

            // also, insert here test data that needs to exist, like Products and Customers

            // insert customer with name "IBM" ... it is needed in the test
            CreateCustomerService().Save(new Customer { Name = "IBM" });

            // insert products with names "A4 Paper" and "Printer EPSON X1" .. it is needed in the test
            CreateProductService().Save(new Product { Name = "A4 Paper" });
            CreateProductService().Save(new Product { Name = "Printer EPSON X1" });
        }

        [TestCleanup]
        public override void CleanupTest()
        {
            base.CleanupTest();

            // cleanup after ... close database sessions etc...
        }

        [TestMethod]
        public void WhenAddingOneOrderWith2LinesWeGetCorrectReportOnDates()
        {
            var target = CreateTarget();

            var order = CreateInput();

            target.Save(order);

            var summaries = target.ReportByDateRange(new DateTime(2017, 10, 1), new DateTime(2017, 10, 30));

            Assert.IsNotNull(summaries);
            Assert.AreEqual(1, summaries.Length);

            var summary = summaries[0];
            Assert.IsNotNull(summary);
            Assert.AreEqual(2, summary.LinesCount);
            Assert.AreEqual(270, summary.TotalPriceWithoutDiscount);
            Assert.AreEqual(270 - 27, summary.TotalPrice);
        }

        protected override Order CreateInput()
        {
            return new Order
            {
                OrderDate = new DateTime(2017, 10, 11),
                DiscountPercent = 10,
                Customer = CreateCustomerService().GetByName("IBM"),
                OrderLines = new List<OrderDetail>
                {
                    new OrderDetail { Price = 10, Quantity = 6, Product = CreateProductService().GetByName("A4 Paper") },
                    new OrderDetail { Price = 30, Quantity = 7, Product = CreateProductService().GetByName("Printer EPSON X1") }
                }
            };
        }

        protected override void AssertAreEqual(Order expected, Order actual)
        {
            throw new NotImplementedException();
        }
    }
}
