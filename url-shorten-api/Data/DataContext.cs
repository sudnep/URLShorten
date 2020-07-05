using Microsoft.EntityFrameworkCore;
using url_shorten_api.Models;

namespace url_shorten_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //add a property of type dbset
        public DbSet<URLShort> URLShort { get; set; }
    }
}