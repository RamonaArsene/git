using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace WebStore.Business.Mappings
{
    public class CustomerMap : ClassMapping<Customer>
    {
        public CustomerMap()
        {
            Id(a => a.Id, a =>
            {
                a.Column("Id");
                a.Generator(Generators.Identity);
            });
            Property(a => a.Name, a =>
            {
                a.Column("Name");
               
            });
            Property(a => a.DiscountPercent, a =>
            {
                a.Column("DiscountPercent");
                a.Precision(20);
                a.Scale(8);
            });
           
        }
    }
}
