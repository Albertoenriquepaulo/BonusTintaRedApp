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

            return await dbContext.User.Include(ub => ub.UserBonus).ThenInclude(b => b.Bonus).FirstOrDefaultAsync(x => x.Id == id);
            //return await dbContext.User.FindAsync(id);
        }

        public async Task<List<UserBonus>> GetUserBonus(int id)
        {
            return (await dbContext.User.Include(u => u.UserBonus).Where(u => u.Id == 1).FirstOrDefaultAsync()).UserBonus.ToList();
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

        //public void UpdateUserAsync1(User model)
        //{
        //    var existingParent = dbContext.Parents
        //        .Where(p => p.Id == model.Id)
        //        .Include(p => p.Children)
        //        .SingleOrDefault();

        //    if (existingParent != null)
        //    {
        //        // Update parent
        //        dbContext.Entry(existingParent).CurrentValues.SetValues(model);

        //        // Delete children
        //        foreach (var existingChild in existingParent.Children.ToList())
        //        {
        //            if (!model.Children.Any(c => c.Id == existingChild.Id))
        //                dbContext.Children.Remove(existingChild);
        //        }

        //        // Update and Insert children
        //        foreach (var childModel in model.Children)
        //        {
        //            var existingChild = existingParent.Children
        //                .Where(c => c.Id == childModel.Id)
        //                .SingleOrDefault();

        //            if (existingChild != null)
        //                // Update child
        //                dbContext.Entry(existingChild).CurrentValues.SetValues(childModel);
        //            else
        //            {
        //                // Insert child
        //                var newChild = new Child
        //                {
        //                    Data = childModel.Data,
        //                    //...
        //                };
        //                existingParent.Children.Add(newChild);
        //            }
        //        }

        //        dbContext.SaveChanges();
        //    }
        //}


        #endregion
    }
}
