using Countries.Entities;
using Countries.Nh;
using Countries.Nh.Respositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Countries.Web.Controllers
{
    public class CountriesController : ApiController
    {
        private static string connectionString =
            @"Data Source=.\sql2014;Initial Catalog=Countries;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;

        static CountriesController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }

        public IEnumerable<Country> Get()
        {
            using(var session = sessionFactory.OpenSession())
            {
                var repository = new Repository<Country>(session);

                return repository.GetAll();
            }
        }
    }
}