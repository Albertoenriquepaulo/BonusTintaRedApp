using BonusApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Services
{
    public class TransactionServices
    {
        #region Private members

        private BonusAppDbContext dbContext;

        #endregion Private members

        #region Constructor

        public TransactionServices(BonusAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion Constructor

        #region Public methods

        public async Task<List<Transaction>> GetAllTransactionAsync()
        {
            return await dbContext.Transaction.ToListAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionByCouponIdAndClientCouponIdAsync(int couponId, int clientCouponId)
        {
            return await dbContext.Transaction.Where(bs => bs.CouponId == couponId && bs.ClientCouponId == clientCouponId).ToListAsync();
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transactions)
        {
            try
            {
                dbContext.Transaction.Add(transactions);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return transactions;
        }

        public async Task<int> GetSpentPagesByTransactionId(int transactionsId)
        {
            var list = await dbContext.Transaction.Where(bs => bs.Id == transactionsId).ToListAsync();
            int sum = list.Sum(item => item.SpentPages);
            return (await dbContext.Transaction.Where(bs => bs.Id == transactionsId).ToListAsync()).Sum(item => item.SpentPages);
        }

        #endregion Public methods
    }
}