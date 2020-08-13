using BonusApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Services
{
    public class BonusSpendingServices
    {
        #region Private members
        private BonusAppDbContext dbContext;
        #endregion

        #region Constructor
        public BonusSpendingServices(BonusAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public async Task<List<BonusSpending>> GetAllBonusSpendingAsync()
        {
            return await dbContext.BonusSpending.ToListAsync();
        }
        public async Task<List<BonusSpending>> GetAllBonusSpendingByBonusIdAsync(int bonusId)
        {
            return await dbContext.BonusSpending.Where(bs => bs.BonusId == bonusId).ToListAsync();
        }

        public async Task<BonusSpending> GetBonusSpendingAsync(int id)
        {
            return await dbContext.BonusSpending.FindAsync(id);
        }

        public async Task<BonusSpending> AddBonusSpendingAsync(BonusSpending bonusSpending)
        {
            try
            {
                dbContext.BonusSpending.Add(bonusSpending);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return bonusSpending;
        }

        public async Task<int> GetSpentPagesByBonusSpendingId(int bonusSpendingId)
        {
            var list = await dbContext.BonusSpending.Where(bs => bs.Id == bonusSpendingId).ToListAsync();
            int sum = list.Sum(item => item.SpentPages);
            return (await dbContext.BonusSpending.Where(bs => bs.Id == bonusSpendingId).ToListAsync()).Sum(item => item.SpentPages);
        }



        //public async Task<BonusSpending> Get(BonusSpending bonusSpending)
        //{
        //    try
        //    {
        //        dbContext.BonusSpending.Add(bonusSpending);
        //        await dbContext.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return bonusSpending;
        //}


        public async Task<BonusSpending> UpdateBonusSpendingAsync(BonusSpending bonusSpending)
        {
            try
            {
                var bondExist = dbContext.BonusSpending.FirstOrDefault(bs => bs.Id == bonusSpending.Id);
                if (bondExist != null)
                {
                    dbContext.Update(bonusSpending);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bonusSpending;
        }

        public async Task DeleteBonusSpendingAsync(BonusSpending bonusSpending)
        {
            try
            {
                dbContext.BonusSpending.Remove(bonusSpending);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
