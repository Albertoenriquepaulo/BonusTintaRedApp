﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class CouponServices
    {
        #region Private members

        private CouponAppDbContext dbContext;

        #endregion Private members

        #region Constructor

        public CouponServices(CouponAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion Constructor

        #region Public methods

        public async Task<List<Coupon>> GetAllCouponAsync()
        {
            return await dbContext.Coupon.ToListAsync();
        }

        public async Task<Coupon> GetCouponAsync(int couponId)
        {
            return await dbContext.Coupon.FindAsync(couponId);
        }

        public async Task<Coupon> AddCouponAsync(Coupon bond)
        {
            try
            {
                dbContext.Coupon.Add(bond);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return bond;
        }

        public async Task<Coupon> UpdateCouponAsync(Coupon bond)
        {
            try
            {
                var bondExist = dbContext.Coupon.FirstOrDefault(b => b.Id == bond.Id);
                if (bondExist != null)
                {
                    dbContext.Update(bond);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bond;
        }

        public async Task DeleteCouponAsync(Coupon bond)
        {
            try
            {
                dbContext.Coupon.Remove(bond);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Public methods
    }
}