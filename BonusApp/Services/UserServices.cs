using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class UserServices
    {
        #region Private members
        private BonusAppDbContext dbContext;
        #endregion

        #region Constructor
        public UserServices(BonusAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods

        public async Task<List<User>> GetAllUserAsync()
        {

            return await dbContext.User.ToListAsync();
        }
        public async Task<User> GetUserAsync(int id)
        {

            return await dbContext.User.Include(ub => ub.UserBonuses).ThenInclude(b => b.Bonus).FirstOrDefaultAsync(x => x.Id == id);
            //return await dbContext.User.FindAsync(id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                dbContext.User.Add(user);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                var userExist = dbContext.User.FirstOrDefault(u => u.Id == user.Id);
                if (userExist != null)
                {
                    dbContext.Update(user);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public async Task DeleteUserAsync(User user)
        {
            try
            {
                dbContext.User.Remove(user);
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
