using Countries.Entities;
using Countries.Nh;
using Countries.Nh.Respositories;
using Countries.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Tests
{
    [TestClass]
    public class CountryRepositoryTest
    {
        private string connectionString = @"Data Source=.\sql2014;Initial Catalog=Countries;User ID=sa;Password=1234%asd;";
        protected ISession session;
        protected ISessionFactory sessionFactory;

        [TestInitialize]
        public void Initialize()
        {
            sessionFactory = new NhConfig(connectionString).Create();
            session = sessionFactory.OpenSession();

            using (var command = session.Connection.CreateCommand())
            {
                command.CommandText = "delete Country";
                command.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            session.Flush();
            session.Dispose();
        }

        protected IRepository<Country> CreateTarget()
        {
            return new Repository<Country>(session);
        }

        protected void AssertSame(Country expected, Country actual, bool testId = false)
        {
        }


        protected Country CreateObject()
        {
            return new Country
            {
                Code = "ZZ",
                Name = "Mangalia"
            };
        }

        protected void UpdateObject(Country entity)
        {
            entity.Latitude = 23;
        }

        [TestMethod]
        public void WhenInsertingOneWeCanGetThatObject()
        {
            var expected = CreateObject();

            var target = CreateTarget();

            var countInitial = target.GetAll().Count;

            target.Insert(expected);

            var countFinal = target.GetAll().Count;

            var actual = target.GetById(expected.Code);

            Assert.AreEqual(countInitial + 1, countFinal);
            Assert.IsNotNull(actual);
            AssertSame(expected, actual);
        }

        [TestMethod]
        public void WhenUpdatingOneWeCanGetThatObject()
        {
            var expected = CreateObject();

            var target = CreateTarget();

            var countInitial = target.GetAll().Count;

            target.Insert(expected);

            expected = target.GetById(expected.Code);
            Assert.IsNotNull(expected);

            var countFinal = target.GetAll().Count;

            UpdateObject(expected);

            target.Update(expected);

            var actual = target.GetById(expected.Code);

            Assert.AreEqual(countInitial + 1, countFinal);
            Assert.IsNotNull(actual);
            AssertSame(expected, actual, true);
        }

        [TestMethod]
        public void WhenDeleteingOneWeCanGetThatObject()
        {
            var expected = CreateObject();

            var target = CreateTarget();

            var countInitial = target.GetAll().Count;

            target.Insert(expected);

            var actual = target.GetById(expected.Code);
            Assert.IsNotNull(actual);

            target.Delete(actual);

            actual = target.GetById(expected.Code);
            Assert.IsNull(actual);
        }
    }
}
