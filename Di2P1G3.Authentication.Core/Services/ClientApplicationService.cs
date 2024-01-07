using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Authentication.Infrastructure;
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

namespace Di2P1G3.Authentication.Core.Services
{
    public class ClientApplicationService : IClientApplicationService
    {
        private readonly IClientApplicationRepository _repo;

        public ClientApplicationService()
        {
            _repo = ServiceContainerBuilder.Instance.GetService<IClientApplicationRepository>();
        }

        public async Task<ClientApplication> AddAsync(string name, string redirectUrl,
            CancellationToken cancellationToken)
        {
            ClientApplication ca = new ClientApplication { Name = name, RedirectUrl = redirectUrl };

            return await _repo.AddAsync(ca, cancellationToken);
        }

        public async Task<bool> DeleteAsync(string name, CancellationToken cancellationToken)
        {
            var ca = new ClientApplication { Name = name };

            return await _repo.DeleteAsync(ca, cancellationToken);
        }

        public async Task<IEnumerable<ClientApplication>> FindAppsAsync(Expression<Func<ClientApplication, bool>> predicate,
          CancellationToken cancellationToken)
        {
            return await _repo.FindAppsAsync(predicate, cancellationToken);
        }

        public async Task<ClientApplication> FindAsync(Expression<Func<ClientApplication, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repo.FindAsync(predicate, cancellationToken);
        }
        public async Task<bool> UpdateAppAsync(ClientApplication app, CancellationToken cancellationToken)
        {
            return await _repo.UpdateAppAsync(app, cancellationToken);
        }


    }
}
