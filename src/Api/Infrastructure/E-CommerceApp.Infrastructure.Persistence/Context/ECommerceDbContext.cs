using E_CommerceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Infrastructure.Persistence.Context
{
    public class ECommerceDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public ECommerceDbContext()
        {
        }

        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public void SetCreatedDateBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                                .Where(q => q.State == EntityState.Added)
                                .Select(q => (BaseEntity)q.Entity);

            foreach (var entity in addedEntities)
            {
                if (entity.CreatedDate == DateTime.MinValue)
                    entity.CreatedDate = DateTime.Now;
            }
        }
    }
}
