using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.SharedKernel.Interfaces
{
    public interface ITokenRepository
    {
        public Task<AccessToken> StoreTokenAsync(AccessToken token, CancellationToken cancellationToken);
        public Task<RefreshToken> StoreRefreshTokenAsync(RefreshToken token, CancellationToken cancellationToken);

        public Task<AccessToken> FindTokenAsync(Expression<Func<AccessToken, bool>> predicate, CancellationToken cancellationToken);

        public Task<IEnumerable<AccessToken>> FindTokensAsync(Expression<Func<AccessToken, bool>> predicate, CancellationToken cancellationToken);

    }
}
