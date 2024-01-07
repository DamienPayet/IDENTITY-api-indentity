using System;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.Core.Interfaces
{
    public interface IClaimService
    {
        Task<bool> AddClaimToUserAsync(Guid userId, string clientAppName);
        
        Task<bool> RemoveClaimFromUserAsync(Guid userId, string clientAppName);
    }
}