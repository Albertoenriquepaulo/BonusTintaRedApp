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
    public class ClientCouponServices
    {
        #region Private members
        private BonusAppDbContext dbContext;
        private readonly CouponServices _couponServices;
        #endregion

        #region Constructor
        public ClientCouponServices(BonusAppDbContext dbContext, CouponServices couponServices)
        {
            this.dbContext = dbContext;
            _couponServices = couponServices;
        }
        #endregion

        #region Public methods
        public async Task<List<ClientCoupon>> GetAllClientCouponAsync()
        {
            return await dbContext.ClientCoupon.Include(ub => ub.Coupon).Include(ub => ub.Client).ToListAsync();
        }
        public async Task<List<ClientCoupon>> GetAllClientCouponByClientIdAsync(int id)
        {
            return await dbContext.ClientCoupon.Include(ub => ub.Coupon).Where(ub => ub.ClientId == id).ToListAsync();
        }
        public async Task<ClientCoupon> GetCouponIdByClientCouponIdAsync(int id)
        {
            return await dbContext.ClientCoupon.Include(ub => ub.Client).Where(ub => ub.Id == id).FirstOrDefaultAsync();
        }
        public async Task<ClientCoupon> GetClientCouponAsync(int id)
        {

            return await dbContext.ClientCoupon.FindAsync(id);
            //return await dbContext.Client.FindAsync(id);
        }
        public bool AddListClientCouponAsyncWSQL(List<ClientCoupon> clientCoupons)
        {
            const string connection = "Data Source=ClientBondsDB.db;";
            string stringQuery;

            SqliteConnection sqlite_conn = new SqliteConnection(connection);
            sqlite_conn.Open();
            var SqliteCmd = new SqliteCommand();
            SqliteCmd = sqlite_conn.CreateCommand();

            try
            {
                foreach (var clientCoupon in clientCoupons)
                {

                    stringQuery = "INSERT INTO ClientCoupon (ClientId, CouponId, SpentPages, Date) VALUES(" +
                        $"{clientCoupon.ClientId}, {clientCoupon.CouponId}, {clientCoupon.SpentPages}, '{clientCoupon.Date}')";
                    SqliteCmd.CommandText = stringQuery;
                    SqliteCmd.ExecuteNonQuery();
                }
                sqlite_conn.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public async Task<ClientCoupon> UpdateClientCouponAsync(ClientCoupon clientCoupon)
        {
            try
            {
                var clientExist = dbContext.ClientCoupon.FirstOrDefault(u => u.Id == clientCoupon.Id);
                if (clientExist != null)
                {
                    dbContext.Update(clientCoupon);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return clientCoupon;
        }
        public async Task<bool> DiscountPagesAsync(ClientCoupon clientCoupon, int pages)
        {
            if (clientCoupon.SpentPages + pages > (await _couponServices.GetCouponAsync(clientCoupon.CouponId)).Pages)
            {
                return false;
            }
            clientCoupon.SpentPages += pages;
            await UpdateClientCouponAsync(clientCoupon);
            return true;
        }
        public async Task DeleteClientCouponAsync(ClientCoupon clientCoupon)
        {
            try
            {
                dbContext.ClientCoupon.Remove(clientCoupon);
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
