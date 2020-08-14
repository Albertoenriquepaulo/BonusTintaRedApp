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
        public DbSet<User> User { get; set; }
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<UserBonus> UserBonus { get; set; }
        public DbSet<BonusSpending> BonusSpending { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetUsers());
            modelBuilder.Entity<Bonus>().HasData(GetCoupons());
            base.OnModelCreating(modelBuilder);
        }
        #endregion


        #region Private methods
        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id=1, Email = "TRCtester@tcr.com"},
            };
        }

        private List<Bonus> GetCoupons()
        {
            return new List<Bonus>
            {
                new Bonus { Id=1, Name = "BYN100", Pages= 100},
                new Bonus { Id=2, Name = "BYN200", Pages= 200},
                new Bonus { Id=3, Name = "BYN500", Pages= 500},
                new Bonus { Id=4, Name = "COLOR50", Pages= 50},
                new Bonus { Id=5, Name = "COLOR100", Pages= 100},
                new Bonus { Id=6, Name = "COLOR200", Pages= 200},
            };
        }
        #endregion

    }
}
