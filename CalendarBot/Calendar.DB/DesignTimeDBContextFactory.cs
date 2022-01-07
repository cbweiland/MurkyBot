using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Calendar.DB
{
    public class DesignTimeDBContextFactory : IDesignTimeDbContextFactory<CalendarContext>
    {
        public CalendarContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Calendar.DB/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<CalendarContext>();
            var connectionString = configuration.GetConnectionString("CalendarContext");
            builder.UseMySQL(connectionString);
            return new CalendarContext(builder.Options);
        }
    }
}
