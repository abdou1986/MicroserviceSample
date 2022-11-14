using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastucture.Persistence
{
    public class OrderContext : DbContext
    {
        //1- Add DbContextOptions<ClassContext> as injection inside the ctor
        //2- should add inheritence from base

        public OrderContext(DbContextOptions<OrderContext> options ) : base( options )
        {
        }

        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "swn";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "swn";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
