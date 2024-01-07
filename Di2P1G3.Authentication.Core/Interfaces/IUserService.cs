using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.SharedKernel;

namespace Di2P1G3.Authentication.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string username, string firstname, string lastname, string password,
            CancellationToken cancellationToken);

        Task<bool> DeleteUserAsync(User user, CancellationToken cancellationToken);

        Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken);

        Task<IEnumerable<User>> FindUsersAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        
        Task<User> FindUserAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> AreCredentialsValid(string username, string password, CancellationToken cancellationToken);
    }
}