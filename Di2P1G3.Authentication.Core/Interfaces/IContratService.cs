using Di2P1G3.Authentication.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.Core.Interfaces
{
    public interface IContratService
    {
        Task<Contrat> FindByUser(User user, CancellationToken cancellationToken);
    }
}
