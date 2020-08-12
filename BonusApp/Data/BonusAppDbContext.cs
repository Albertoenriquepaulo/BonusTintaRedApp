﻿using Microsoft.EntityFrameworkCore;
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
            base.OnModelCreating(modelBuilder);
        }
        #endregion


        #region Private methods
        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id=1, Email = "email@email.com"},
                new User { Id=2, Email = "Alan@email.com"},
                new User { Id=3, Email = "Tom@email.com"}
            };
        }
        #endregion

    }
}
