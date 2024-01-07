using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Di2P1G3.Authentication.Api
{
    public class GetTokens
    {
        private readonly ITokenService _tokenService;

        public GetTokens()
        {
            _tokenService = ServiceContainerBuilder.Instance.GetService<ITokenService>();
        }
        [Function("GetTokens")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            using var CancelationSource = new CancellationTokenSource();
            var tokens = await _tokenService.FindTokensAsync(token => true, CancelationSource.Token);

            return tokens == null ? req.RespondWithNotFound() : await req.RespondWithObjectAsJson(tokens); 
        }
    }
}
