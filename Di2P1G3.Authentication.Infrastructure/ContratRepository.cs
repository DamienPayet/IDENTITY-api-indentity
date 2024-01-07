using Di2P1G3.Authentication.Infrastructure.Data;
using Di2P1G3.Authentication.SharedKernel;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.Infrastructure
{
    public class ContratRepository : IContratRepository
    {
        private readonly AppDbContext _dbContext;

        public ContratRepository()
        {
            _dbContext = (AppDbContext)ServiceContainerBuilder.Instance.GetService<DbContext>();
        }


        public Task<Contrat> FindByUser(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
