using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Authentication.SharedKernel;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace Di2P1G3.Authentication.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private X509Certificate2 _certificate2;

        public TokenService()
        {
            getX509Certificate2FromAzureKeyVault();
            _tokenRepository = ServiceContainerBuilder.Instance.GetService<ITokenRepository>();
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(AccessToken token, CancellationToken cancellationToken)
        {
            var payload = new Payload(TokenType.Refresh);
            var coldtoken = new Token(JsonSerializer.Serialize(payload), _certificate2);
            return await _tokenRepository.StoreRefreshTokenAsync(new RefreshToken() { Token = coldtoken._Token, IdAccessToken = token.Id }, cancellationToken);
        }

        public async Task<AccessToken> CreateBearerTokenAsync(User user, CancellationToken cancellationToken)
        {
            var payload = new Payload(TokenType.Bearer);
            var token = new Token(JsonSerializer.Serialize(payload), _certificate2);
            return await _tokenRepository.StoreTokenAsync(new AccessToken() { Token = token._Token, IdUser = user.Id }, cancellationToken);
        }
        public async Task<AccessToken> CreateAccessTokenAsync(User user, CancellationToken cancellationToken)
        {
            var payload = new Payload(user.Id, TokenType.Access);
            var token = new Token(JsonSerializer.Serialize(payload), _certificate2);
            return await _tokenRepository.StoreTokenAsync(new AccessToken() { Token = token._Token, IdUser = user.Id }, cancellationToken);
        }

        public async Task<AccessToken> FindTokenAsync(Expression<Func<AccessToken, bool>> predicate,
         CancellationToken cancellationToken)
        {
            return await _tokenRepository.FindTokenAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<AccessToken>> FindTokensAsync(Expression<Func<AccessToken, bool>> predicate,
       CancellationToken cancellationToken)
        {
            return await _tokenRepository.FindTokensAsync(predicate, cancellationToken);
        }

        public bool checkToken(string token , CancellationToken cancellationToken)
		{
            Token tokentocheck = new Token(_certificate2, token);
			if (tokentocheck.VerifySignatureToken(token) && tokentocheck.VerifyExpirationToken(token))
			{
                return true;
			}
            return false; 
		}

        private void getX509Certificate2FromAzureKeyVault()
        {
            var _keyVaultName = $"https://diiage2-p2-g3-vault.vault.azure.net/";
            var secretName = "p1-cert";
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var _client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secret = _client.GetSecretAsync(_keyVaultName, secretName);
            var privateKeyBytes = Convert.FromBase64String(secret.Result.Value);
            _certificate2 = new X509Certificate2(privateKeyBytes, string.Empty);

        }
    }
}