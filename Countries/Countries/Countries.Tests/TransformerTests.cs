using Countries.Entities;
using Countries.Nh;
using Countries.Nh.Respositories;
using Countries.Parsers;
using Countries.Repositories;
using Countries.Transformers;
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
    public class TransformerTests
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

        [TestMethod]
        public void WhenReadingFileAndInsertingInDbWeGet245()
        {
            var repo = new Repository<Country>(session);
            var parser = new CsvCountrieFileParser();

            var target = new CsvFileToDbTransformer(parser, repo);

            target.Execute(@"Countries.csv");

            var count = repo.GetAll().Count;

            Assert.AreEqual(245, count);
        }

  
    }
}
