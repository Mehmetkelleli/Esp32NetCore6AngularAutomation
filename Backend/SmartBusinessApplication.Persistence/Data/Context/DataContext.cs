using Microsoft.EntityFrameworkCore;
using SmartBusinessApplication.Domain.Entity;

namespace SmartBusinessApplication.Persistence.Data.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(new Client
            {
                Id = 1,
                Name = "Deneme",
                InSideCurrentTemperature = 20.5,
                OutSideCurrentTemperature = 45.1,
                TemperatureLimit = 30.1,
                CreatedDate= DateTime.Now,
                UpdatedDate= DateTime.Now,
                ClientPasswordEncrypt = "Deneme",
                ClientUserName = "Deneme",
                State = false,
                Role1 = false,
                Role2 = false
            });
            modelBuilder.Entity<Client>().HasData(new Client
            {
                Id = 2,
                Name = "Deneme2",
                TemperatureLimit = 30.1,
                InSideCurrentTemperature = 37.5,
                OutSideCurrentTemperature=10.4,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ClientPasswordEncrypt = "Deneme2",
                ClientUserName = "Deneme2",
                State = false,
                Role1 = false,
                Role2 = false
            });
        }
    }
}
