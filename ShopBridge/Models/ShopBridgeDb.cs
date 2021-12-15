using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShopBridge.Models
{
    public class ShopBridgeDb : DbContext
    {
        public ShopBridgeDb(DbContextOptions<ShopBridgeDb> options)
            : base(options)
        {
        }

        public DbSet<ItemMaster> ItemMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);Database=Ef_Test1;Trusted_Connection=True;");
            }

        }

    }
}
