using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.SharedKernel.Interfaces
{
    public interface IClientApplicationRepository
    {
        Task<ClientApplication> AddAsync(ClientApplication ca, CancellationToken cancellationToken);

        Task<ClientApplication> FindAsync(Expression<Func<ClientApplication, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<ClientApplication>> FindAppsAsync(Expression<Func<ClientApplication, bool>> predicate,
           CancellationToken cancellationToken);

        Task<bool> UpdateAppAsync(ClientApplication app, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(ClientApplication ca, CancellationToken cancellationToken);
    }
}
