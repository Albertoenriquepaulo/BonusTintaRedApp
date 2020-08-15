using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class BonusAppDbContext : DbContext
    {
        #region Contructor
        public BonusAppDbContext(DbContextOptions<BonusAppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        #endregion

        #region Public properties
        public DbSet<Client> Client { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<ClientCoupon> ClientCoupon { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(GetClients());
            modelBuilder.Entity<Coupon>().HasData(GetCoupons());
            base.OnModelCreating(modelBuilder);
        }
        #endregion


        #region Private methods
        private List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client { Id=1, Email = "TRCtester@tcr.com"},
            };
        }

        private List<Coupon> GetCoupons()
        {
            return new List<Coupon>
            {
                new Coupon { Id=1, Name = "BYN100", Pages= 100},
                new Coupon { Id=2, Name = "BYN200", Pages= 200},
                new Coupon { Id=3, Name = "BYN500", Pages= 500},
                new Coupon { Id=4, Name = "COLOR50", Pages= 50},
                new Coupon { Id=5, Name = "COLOR100", Pages= 100},
                new Coupon { Id=6, Name = "COLOR200", Pages= 200},
            };
        }
        #endregion

    }
}
