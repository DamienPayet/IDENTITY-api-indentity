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

namespace Di2P1G3.Authentication.Infrastructure
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _dbContext;
        public TokenRepository()
        {
            _dbContext = (AppDbContext)ServiceContainerBuilder.Instance.GetService<DbContext>();
        }

        public async Task<AccessToken> StoreTokenAsync(AccessToken token, CancellationToken cancellationToken)
        {
            _dbContext.AccessTokens.Add(token);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return token;
        }

        public async Task<RefreshToken> StoreRefreshTokenAsync(RefreshToken token, CancellationToken cancellationToken)
        {
            _dbContext.RefreshTokens.Add(token);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return token;
        }
        public async Task<AccessToken> FindTokenAsync(Expression<Func<AccessToken, bool>> predicate, CancellationToken cancellationToken)
        {
            try
            {
                return (await _dbContext.AccessTokens.Where(predicate).ToListAsync(cancellationToken)).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<IEnumerable<AccessToken>> FindTokensAsync(Expression<Func<AccessToken, bool>> predicate, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.AccessTokens
                    .Where(predicate).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


    }
}
