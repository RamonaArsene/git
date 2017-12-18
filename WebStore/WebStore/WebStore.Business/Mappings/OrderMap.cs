using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace OrderMap.Business.Mappings
{
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Table("[Order]");
            Id(a => a.Id, a =>
            {
                a.Column("OrderId");
                a.Generator(Generators.Identity);
            });

            ManyToOne(p => p.Customer, m =>
            {
                m.Column("CustomerId");
                m.Lazy(LazyRelation.NoLazy);
            });
            Property(a => a.OrderDate, a =>
            {
                a.Column("OrderDate");
            });

            Property(a => a.DiscountPercent, a =>
            {
                a.Column("DiscountPercent");
            });


            Bag(p => p.OrderLines, cm =>
            {
                cm.Fetch(CollectionFetchMode.Subselect);
                cm.Cascade(Cascade.All | Cascade.DeleteOrphans);
                cm.Inverse(true);
                cm.Table("OrderDetail");
                cm.Key(k => k.Column("OrderId"));
                cm.Lazy(CollectionLazy.NoLazy);
            },
           action => action.OneToMany());
        }
    }
}
