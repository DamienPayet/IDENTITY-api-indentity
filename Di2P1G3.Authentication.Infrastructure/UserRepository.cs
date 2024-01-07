using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.Infrastructure.Data;
using Di2P1G3.Authentication.SharedKernel;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Di2P1G3.Authentication.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public UserRepository()
        {
            _logger = ServiceContainerBuilder.Instance.GetService<ILogger>();
            _dbContext = (AppDbContext)ServiceContainerBuilder.Instance.GetService<DbContext>();
        }

        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.AddAsync(user, cancellationToken);
                var created = await _dbContext.SaveChangesAsync(cancellationToken);

                return created > 0 ? user : null;
            }
            catch (Exception e)
            {
                _logger.Warning(e, "An unexpected SQL error happened, could not create {User}",
                    user.Id);

                return null;
            }
        }

        public async Task<IEnumerable<User>> FindUsersAsync(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Users.Where(predicate).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Warning(e, "An unexpected SQL error happened, could not find {Predicate}",
                    predicate.ToString());

                return null;
            }
        }

        public async Task<User> FindUserAsync(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken)
        {
            try
            {
                return (await _dbContext.Users
                        .Include(user => user.Applications)
                        .Include(user => user.Tokens)
                        .Where(predicate)
                        .ToListAsync(cancellationToken))
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.Warning(e, "An unexpected SQL error happened, could not find {Predicate}",
                    predicate.ToString());

                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.Users.Remove(user);
                var removed = await _dbContext.SaveChangesAsync(cancellationToken);

                return removed > 0;
            }
            catch (Exception e)
            {
                _logger.Warning(e, "An unexpected SQL error happened, could not delete {User}",
                    user.Id);

                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.Users.Update(user);
                var updated = await _dbContext.SaveChangesAsync(cancellationToken);

                return updated > 0;
            }
            catch (Exception e)
            {
                _logger.Warning(e, "An unexpected SQL error happened, could not create {User}",
                    user.Id);

                return false;
            }
        }
    }
}