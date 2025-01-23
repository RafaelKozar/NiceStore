using Microsoft.EntityFrameworkCore;
using NiceStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Domain.Product> Products { get; set; }
        public DbSet<Domain.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in 
                modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);  
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                }
            }   

            return await base.SaveChangesAsync() > 0; 
        }
    }
}
