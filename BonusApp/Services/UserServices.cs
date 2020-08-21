using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class ClientServices
    {
        #region Private members

        private BonusAppDbContext dbContext;

        #endregion Private members

        #region Constructor

        public ClientServices(BonusAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion Constructor

        #region Public methods

        public async Task<List<Client>> GetAllClientAsync()
        {
            return await dbContext.Client.ToListAsync();
        }

        public async Task<Client> GetClientAsync(int id)
        {
            return await dbContext.Client.Include(ub => ub.ClientCoupons).ThenInclude(b => b.Coupon).FirstOrDefaultAsync(x => x.Id == id);
            //return await dbContext.Client.FindAsync(id);
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            try
            {
                dbContext.Client.Add(client);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return client;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            try
            {
                var clientExist = dbContext.Client.FirstOrDefault(u => u.Id == client.Id);
                if (clientExist != null)
                {
                    dbContext.Update(client);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return client;
        }

        public async Task DeleteClientAsync(Client client)
        {
            try
            {
                dbContext.Client.Remove(client);
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