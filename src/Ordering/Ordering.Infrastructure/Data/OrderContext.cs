using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }


    }


    //  When we try to run the migrations, Entity Framework Tools tries to obtain the DbContext 
    //  object from the application’s service provider. The problem is that the DAL project is a 
    //  Class Library Project and NOT an ASP.NET Core Application/API. 
    //  Next, it tries to create an instance using a constructor with no parameters from the derived DbContext


    // There is other way we can tell Entity Framework Tools how to create our DbContext by implementing
    // the IDesignTimeDbContextFactory<TContext> interface. From the .NET Core documentation:
    // “If a class implementing this interface is found in either the same project as the derived
    //  DbContext or in the application’s startup project, the tools bypass the other ways of creating
    //   the DbContext and use the design-time factory instead.”

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Ordering.API/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<OrderContext>();
            var connectionString = configuration.GetConnectionString("OrderConnection");
            builder.UseSqlServer(connectionString);
            return new OrderContext(builder.Options);
        }

    }

}
