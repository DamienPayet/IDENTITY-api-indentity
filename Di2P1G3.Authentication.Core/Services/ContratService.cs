using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Authentication.Infrastructure;
using Di2P1G3.Authentication.SharedKernel;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.Core.Services
{
    public class ContratService : IContratService
    {

        private readonly ContratRepository _repo;

        public ContratService()
        {
            _repo = (ContratRepository)ServiceContainerBuilder.Instance.GetService<IContratRepository>();
        }

        public Task<Contrat> FindByUser(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
