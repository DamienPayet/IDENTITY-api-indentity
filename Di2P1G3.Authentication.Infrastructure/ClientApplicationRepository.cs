using Di2P1G3.Authentication.Infrastructure.Data;
using Di2P1G3.Authentication.SharedKernel;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.Infrastructure
{
    public class ClientApplicationRepository : IClientApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public ClientApplicationRepository()
        {
            _dbContext = (AppDbContext)ServiceContainerBuilder.Instance.GetService<DbContext>();
        }


        public async Task<ClientApplication> AddAsync(ClientApplication ca, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.ClientApplications.Add(ca);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return ca;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ClientApplication> FindAsync(Expression<Func<ClientApplication, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return (await _dbContext.ClientApplications
                .Include(application => application.Users)
                .Where(predicate)
                .ToListAsync(cancellationToken)).FirstOrDefault();
        }

        public async Task<bool> DeleteAsync(ClientApplication ca, CancellationToken cancellationToken)
        {
            try
            {
                var foundCa = _dbContext.ClientApplications.FirstOrDefault(c => c.Name == ca.Name);
                if (foundCa != null)
                    _dbContext.Remove((object)foundCa);

                var changes = await _dbContext.SaveChangesAsync(cancellationToken);

                return changes > 0;
            }
            catch
            {
                return false;
            }
        }


        public async Task<IEnumerable<ClientApplication>> FindAppsAsync(Expression<Func<ClientApplication, bool>> predicate, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.ClientApplications.Where(predicate).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {


                return null;
            }
        }
        public async Task<bool> UpdateAppAsync(ClientApplication app, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.ClientApplications.Update(app);
                var updated = await _dbContext.SaveChangesAsync(cancellationToken);

                return updated > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}