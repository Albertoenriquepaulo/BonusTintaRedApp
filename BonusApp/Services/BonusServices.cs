using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class BonusServices
    {
        #region Private members
        private BonusAppDbContext dbContext;
        #endregion

        #region Constructor
        public BonusServices(BonusAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public async Task<List<Bonus>> GetAllBonusAsync()
        {
            return await dbContext.Bonus.ToListAsync();
        }

        public async Task<Bonus> GetBonusAsync(int bonusId)
        {
            return await dbContext.Bonus.FindAsync(bonusId);
        }

        public async Task<Bonus> AddBonusAsync(Bonus bond)
        {
            try
            {
                dbContext.Bonus.Add(bond);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return bond;
        }

        public async Task<Bonus> UpdateBonusAsync(Bonus bond)
        {
            try
            {
                var bondExist = dbContext.Bonus.FirstOrDefault(b => b.Id == bond.Id);
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

        public async Task DeleteBonusAsync(Bonus bond)
        {
            try
            {
                dbContext.Bonus.Remove(bond);
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
