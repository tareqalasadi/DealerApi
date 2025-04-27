using DealerApi.DomenClass;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DealerApi.AppDb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyCategory> PropertyCategories { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

       
    }

}
