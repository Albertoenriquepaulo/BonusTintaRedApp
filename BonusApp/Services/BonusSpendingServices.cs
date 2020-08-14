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

        public async Task<List<BonusSpending>> GetAllBonusSpendingByBonusIdAndUserBonusIdAsync(int bonusId, int userBonusId)
        {
            return await dbContext.BonusSpending.Where(bs => bs.BonusId == bonusId && bs.UserBonusId == userBonusId).ToListAsync();
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
        #endregion
    }
}
