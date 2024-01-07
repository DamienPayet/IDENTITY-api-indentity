using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.SharedKernel.Interfaces
{
    public interface IContratRepository
    {
        Task<Contrat> FindByUser(User user, CancellationToken cancellationToken);


    }
}
