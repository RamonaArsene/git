using Countries.Entities;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Nh.Mappings
{
    public class CountryMap : ClassMapping<Country>
    {
        public CountryMap()
        {
            Id(a => a.Code, a =>
            {
                a.Column("Cod");
            });
            Property(a => a.Latitude, a =>
            {
                a.Column("Latitudine");
                a.Precision(20);
                a.Scale(8);
            });
            Property(a => a.Longitude, a =>
            {
                a.Column("Longitudine");
                a.Precision(20);
                a.Scale(8);
            });
            Property(a => a.Name, a =>
            {
                a.Column("Nume");
            });
        }
    }
}
