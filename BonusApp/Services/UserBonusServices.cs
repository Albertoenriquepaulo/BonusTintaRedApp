using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class UserBonusServices
    {
        #region Private members
        private BonusAppDbContext dbContext;
        #endregion

        #region Constructor
        public UserBonusServices(BonusAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods

        public async Task<List<UserBonus>> GetAllUserBonusAsync()
        {

            return await dbContext.UserBonus.Include(ub => ub.Bonus).ToListAsync();
        }
        public async Task<UserBonus> GetUserBonusAsync(int id)
        {

            return await dbContext.UserBonus.FindAsync(id);
            //return await dbContext.User.FindAsync(id);
        }

        public async Task<UserBonus> AddUserBonusAsync(UserBonus userBonus)
        {
            try
            {
                dbContext.UserBonus.Add(userBonus);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return userBonus;
        }

        public async Task<UserBonus> UpdateUserBonusAsync(UserBonus userBonus)
        {
            try
            {
                var userExist = dbContext.UserBonus.FirstOrDefault(u => u.Id == userBonus.Id);
                if (userExist != null)
                {
                    dbContext.Update(userBonus);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userBonus;
        }

        public async Task DiscountPagesAsync(UserBonus userBonus, int pages)
        {
            userBonus.SpentPages += pages;
            await UpdateUserBonusAsync(userBonus);
        }

        //public async Task DeleteUserAsync(UserBonus userBonus)
        //{
        //    try
        //    {
        //        dbContext.UserBonus.Remove(userBonus);
        //        await dbContext.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
    }
}
