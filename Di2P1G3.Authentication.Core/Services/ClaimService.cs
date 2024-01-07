using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Authentication.Infrastructure;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;

namespace Di2P1G3.Authentication.Core.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientApplicationRepository _clientAppRepository;

        public ClaimService()
        {
            _userRepository = ServiceContainerBuilder.Instance.GetService<IUserRepository>();
            _clientAppRepository = ServiceContainerBuilder.Instance.GetService<IClientApplicationRepository>();
        }

        public async Task<bool> AddClaimToUserAsync(Guid userId, string clientAppName)
        {
            var user = await _userRepository.FindUserAsync(user => user.Id == userId, CancellationToken.None);

            if (user == null)
                return false;

            var clientApp = await _clientAppRepository.FindAsync(application => application.Name.Equals(clientAppName),
                CancellationToken.None);

            user.Applications.Add(clientApp);

            return await _userRepository.UpdateUserAsync(user, CancellationToken.None);
        }

        public async Task<bool> RemoveClaimFromUserAsync(Guid userId, string clientAppName)
        {
            var user = await _userRepository.FindUserAsync(user => user.Id == userId, CancellationToken.None);

            if (user == null)
                return false;

            var clientApp = await _clientAppRepository.FindAsync(application => application.Name.Equals(clientAppName),
                CancellationToken.None);

            var toDeleteCa = user.Applications.FirstOrDefault(ca => ca.Id == clientApp.Id);
            user.Applications.Remove(toDeleteCa);

            return await _userRepository.UpdateUserAsync(user, CancellationToken.None);
        }
    }
}