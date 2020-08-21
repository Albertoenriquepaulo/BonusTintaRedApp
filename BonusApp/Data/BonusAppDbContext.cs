using BonusApp.Pages.ClientPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BonusApp.Data
{
    public class BonusAppDbContext : DbContext
    {
        #region Contructor

        public BonusAppDbContext(DbContextOptions<BonusAppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion Contructor

        #region Public properties

        public DbSet<Client> Client { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<ClientCoupon> ClientCoupon { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        #endregion Public properties

        #region Overidden methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(GetClients());
            modelBuilder.Entity<Client>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Coupon>().HasData(GetCoupons());

            base.OnModelCreating(modelBuilder);
        }

        #endregion Overidden methods

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
                new Coupon { Id=1, Name = CouponTypes.BYN100, Pages= 100},
                new Coupon { Id=2, Name = CouponTypes.BYN200, Pages= 200},
                new Coupon { Id=3, Name = CouponTypes.BYN500, Pages= 500},
                new Coupon { Id=4, Name = CouponTypes.COLOR50, Pages= 50},
                new Coupon { Id=5, Name = CouponTypes.COLOR150, Pages= 150},
                new Coupon { Id=6, Name = CouponTypes.COLOR350, Pages= 350},
            };
        }

        #endregion Private methods
    }
}