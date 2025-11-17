using Microsoft.EntityFrameworkCore;
using MicrosoftSqlClientExamples.Models;

namespace MicrosoftSqlClientExamples.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Property> Properties { get; set; }
    }
}
