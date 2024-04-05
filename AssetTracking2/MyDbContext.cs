using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2
{
    internal class MyDbContext : DbContext
    {
        string connectionString = "data source=Hall-9000;initial catalog=AssetTracking;trusted_connection=true;Encrypt=False";

        public DbSet<Assets> Assets { get; set; } 
        public DbSet<LaptopComputers> LaptopComputers { get; set; }
        public DbSet<MobilePhones> MobilePhones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring.
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Assets>().UseTpcMappingStrategy();

           modelBuilder.Entity<Assets>().HasData(new Assets {Id=1, Brand = "MacBook", PurchaseDate =Convert.ToDateTime("2021-06-14"), Office="Sweden", PriceInUSD=1200 });
            modelBuilder.Entity<LaptopComputers>().HasData(new LaptopComputers { Id = 1, Model = "SD", Type = "Computer", AssetsId=1 });
            
            modelBuilder.Entity<Assets>().HasData(new Assets {Id=2, Brand = "Samsung", PurchaseDate = Convert.ToDateTime("2022-03-12"), Office = "USA", PriceInUSD = 200 });
            modelBuilder.Entity<MobilePhones>().HasData(new MobilePhones { Id = 1, Type = "Mobile", Model = "SD", AssetsId=2});

            modelBuilder.Entity<Assets>().HasData(new Assets {Id = 3, Brand = "Dell", PurchaseDate = Convert.ToDateTime("2014-06-17"), Office = "India", PriceInUSD = 599});
            modelBuilder.Entity<LaptopComputers>().HasData(new LaptopComputers { Id = 3, Type = "Computer", Model = "ThinkPad", AssetsId=3});

            modelBuilder.Entity<Assets>().HasData(new Assets { Id = 4, Brand = "Motorolla", PurchaseDate = Convert.ToDateTime("2020-03-19"), Office = "India", PriceInUSD = 699 });
            modelBuilder.Entity<MobilePhones>().HasData(new MobilePhones { Id = 2, Type = "Mobile", Model = "iPhone14", AssetsId=4});

            modelBuilder.Entity<Assets>().HasData(new Assets { Id = 5, Brand = "Lenovo", PurchaseDate = Convert.ToDateTime("2021-05-10"), Office = "Sweden", PriceInUSD = 599 });
            modelBuilder.Entity<LaptopComputers>().HasData(new LaptopComputers { Id = 4, Model = "IdeaPad", Type = "Computer", AssetsId = 5 });

            //modelBuilder.Entity<Assets>().HasMany(x => x.MobilePhoneList);
            //modelBuilder.Entity<Assets>().HasMany(x => x.LaptopComputerList);
        }
    }
}
