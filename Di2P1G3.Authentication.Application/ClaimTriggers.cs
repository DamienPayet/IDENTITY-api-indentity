using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Di2P1G3.Authentication.Api.Requests;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Di2P1G3.Authentication.Api
{
    public class ClaimTriggers
    {
        private readonly IClaimService _claimService;
        
        public ClaimTriggers()
        {
            _claimService = ServiceContainerBuilder.Instance.GetService<IClaimService>();
        }
        
        [Function("AddUserClaim")]
        public async Task<HttpResponseData> Add(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "user/claim")]
            HttpRequestData req,
            ILogger log)
        {
            using var cancellationSource = new CancellationTokenSource();

            var request = await req.ReadFromJsonAsync<ClaimRequest>(cancellationToken: cancellationSource.Token);

            if (request == null)
            {
                return req.RespondWithBadRequest();
            }

            var success = await _claimService.AddClaimToUserAsync(request.IdUser, request.AppName);

            return success ? req.RespondWithNoContent() : req.RespondWithBadRequest();
        }
        
        [Function("RemoveUserClaim")]
        public async Task<HttpResponseData> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "user/claim")]
            HttpRequestData req,
            ILogger log)
        {
            using var cancellationSource = new CancellationTokenSource();

            var request = await req.ReadFromJsonAsync<ClaimRequest>(cancellationToken: cancellationSource.Token);

            if (request == null)
            {
                return req.RespondWithBadRequest();
            }

            var success = await _claimService.RemoveClaimFromUserAsync(request.IdUser, request.AppName);

            return success ? req.RespondWithNoContent() : req.RespondWithBadRequest();
        }
    }
}