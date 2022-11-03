using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_ConsoleApp
{
    //     A DbContext instance represents a session with the database and can be used to
    //     query and save instances of your entities. DbContext is a combination of the
    //     Unit Of Work and Repository patterns.
    public class DigitalConcurrencyDbContext: DbContext
    {
        // represents the collection of all entities
        public DbSet<Wallet> Wallets { get; set; } = null!; //null forgiving operator

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            var configurations = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var conStr = configurations.GetSection("conStr").Value;

            optionsBuilder.UseSqlServer(conStr);
            
        }
    }
}
