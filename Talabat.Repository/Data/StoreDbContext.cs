using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public class StoreDbContext:DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> option):base(option) 
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductCategory> Category { get; set; }
        public DbSet<ProductBrand> Brand { get; set; }
    }
}
