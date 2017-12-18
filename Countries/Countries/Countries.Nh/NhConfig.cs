using Countries.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Nh
{
    public class NhConfig
    {
        Configuration cfg;

        public NhConfig(string connectionString)
        {
            cfg = new Configuration()
                .DataBaseIntegration(db =>
                {
                db.ConnectionString = connectionString;
                db.Dialect<MsSql2012Dialect>();
                });

            /* Add the mapping we defined: */
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);
        }

        public ISessionFactory Create()
        {
            return cfg.BuildSessionFactory();
        }
    }
}
