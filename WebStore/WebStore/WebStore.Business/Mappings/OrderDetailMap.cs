using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace WebStore.Business.Mappings
{
    public class OderDetailMap : ClassMapping<OrderDetail>
    {
        public OderDetailMap()
        {
            Table("[OrderDetail]");
            Id(a => a.Id, a =>
            {
                a.Column("OrderDetailsId");
                a.Generator(Generators.Identity);
            });

            ManyToOne(p => p.Product, m =>
            {
                m.Column("ProductId");
                m.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(p => p.Order, m =>
           {
               m.Column("OrderId");
               m.Lazy(LazyRelation.NoLazy);

           });
            Property(a => a.Quantity, a =>
            {
                a.Column("Quantity");
                a.Precision(20);
                a.Scale(8);
            });

        }
    }
}
