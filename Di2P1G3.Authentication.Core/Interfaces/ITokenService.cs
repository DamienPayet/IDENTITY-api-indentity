using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.SharedKernel;

namespace Di2P1G3.Authentication.Core.Interfaces
{
    public interface ITokenService
    {
        Task<AccessToken> CreateAccessTokenAsync(User user, CancellationToken cancellationToken);

        Task<RefreshToken> CreateRefreshTokenAsync(AccessToken token, CancellationToken cancellationToken);
        Task<AccessToken> CreateBearerTokenAsync(User user, CancellationToken cancellationToken);

        Task<AccessToken> FindTokenAsync(Expression<Func<AccessToken, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<AccessToken>> FindTokensAsync(Expression<Func<AccessToken, bool>> predicate,
        CancellationToken cancellationToken);

        bool checkToken(string token, CancellationToken cancellationToken);
    }
}