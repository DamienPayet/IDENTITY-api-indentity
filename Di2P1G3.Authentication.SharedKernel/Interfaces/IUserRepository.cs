using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.SharedKernel.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);

        Task<IEnumerable<User>> FindUsersAsync(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken);

        Task<User> FindUserAsync(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken);

        Task<bool> DeleteUserAsync(User user, CancellationToken cancellationToken);

        Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken);
    }
}