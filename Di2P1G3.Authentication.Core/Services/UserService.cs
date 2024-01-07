using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Authentication.SharedKernel;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;

namespace Di2P1G3.Authentication.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = ServiceContainerBuilder.Instance.GetService<IUserRepository>();
        }

        public async Task<User> CreateUserAsync(string username, string firstname, string lastname, string password,
            CancellationToken cancellationToken)
        {
            var salt = PasswordEncryption.GetSalt();
            var hash = PasswordEncryption.GenerateSaltedHash(password, salt);

            return await _userRepository.CreateUserAsync(new User
            {
                Firstname = firstname,
                Name = lastname,
                Username = username,
                PasswordSalt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(hash),
            }, cancellationToken);
        }

        public async Task<bool> DeleteUserAsync(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.DeleteUserAsync(user, cancellationToken);
        }

        public async Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateUserAsync(user, cancellationToken);
        }

        public async Task<IEnumerable<User>> FindUsersAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _userRepository.FindUsersAsync(predicate, cancellationToken);
        }

        public async Task<User> FindUserAsync(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await _userRepository.FindUserAsync(predicate, cancellationToken);
        }

        public async Task<bool> AreCredentialsValid(string username, string password,
            CancellationToken cancellationToken)
        {
            var user = await FindUserAsync(u => u.Username == username, cancellationToken);
			if (user != null)
			{
                return PasswordEncryption.CheckPasswords(password, Convert.FromBase64String(user.PasswordHash),
               Convert.FromBase64String(user.PasswordSalt));
			}
			else
			{
                return false;
			}
           
        }
    }
}