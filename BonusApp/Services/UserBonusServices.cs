using BonusApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace BonusApp.Data
{
    public class UserBonusServices
    {
        #region Private members
        private BonusAppDbContext dbContext;
        private readonly BonusServices _bonusServices;
        #endregion

        #region Constructor
        public UserBonusServices(BonusAppDbContext dbContext, BonusServices bonusServices)
        {
            this.dbContext = dbContext;
            _bonusServices = bonusServices;
        }
        #endregion

        #region Public methods

        public async Task<List<UserBonus>> GetAllUserBonusAsync()
        {

            return await dbContext.UserBonus.Include(ub => ub.Bonus).ToListAsync();
        }
        public async Task<List<UserBonus>> GetAllUserBonusByUserIdAsync(int id)
        {
            return await dbContext.UserBonus.Include(ub => ub.Bonus).Where(ub => ub.UserId == id).ToListAsync();
        }
        public async Task<UserBonus> GetBonusIdByUserBonusIdAsync(int id)
        {
            return await dbContext.UserBonus.Include(ub => ub.User).Where(ub => ub.Id == id).FirstOrDefaultAsync();
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

        public async Task<bool> AddListUserBonusAsync(List<UserBonus> userBonuses)
        {
            foreach (var userBonus in userBonuses)
            {
                try
                {
                    dbContext.UserBonus.Add(userBonus);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException.Message;
                    return false;
                }
            }
            return true;
        }

        public bool AddListUserBonusAsyncWSQL(List<UserBonus> userBonuses)
        {
            const string connection = "Data Source=UserBondsDB.db;";
            string stringQuery;

            SqliteConnection sqlite_conn = new SqliteConnection(connection);
            sqlite_conn.Open();
            var SqliteCmd = new SqliteCommand();
            SqliteCmd = sqlite_conn.CreateCommand();

            try
            {
                foreach (var userBonus in userBonuses)
                {

                    stringQuery = "INSERT INTO UserBonus (UserId, BonusId, SpentPages, Date) VALUES(" +
                        $"{userBonus.UserId}, {userBonus.BonusId}, {userBonus.SpentPages}, '{userBonus.Date}')";
                    SqliteCmd.CommandText = stringQuery;
                    SqliteCmd.ExecuteNonQuery();
                }
                sqlite_conn.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
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

        public async Task<bool> DiscountPagesAsync(UserBonus userBonus, int pages)
        {
            if (userBonus.SpentPages + pages > (await _bonusServices.GetBonusAsync(userBonus.BonusId)).Pages)
            {
                return false;
            }
            userBonus.SpentPages += pages;
            await UpdateUserBonusAsync(userBonus);
            return true;
        }

        public async Task PagesAvailableAsync(int userBonusId)
        {
            UserBonus userBonus = await GetUserBonusAsync(userBonusId);

            await UpdateUserBonusAsync(userBonus);
        }

        public async Task DeleteUserBonusAsync(UserBonus userBonus)
        {
            try
            {
                dbContext.UserBonus.Remove(userBonus);
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
