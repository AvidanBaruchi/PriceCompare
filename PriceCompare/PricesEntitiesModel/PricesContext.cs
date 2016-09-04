using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricesEntitiesModel
{
    public class PricesContext : DbContext
    {
        public PricesContext() : this("PricesCompare")
        {
              
        }

        public PricesContext(string name): base(name)
        {
            
        }

        public DbSet<Chain> Chains { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Store>().HasKey(k => new {k.StoreId, k.Chain.ChainId});
        }
    }
}
