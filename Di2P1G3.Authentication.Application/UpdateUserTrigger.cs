﻿using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Di2P1G3.Authentication.Api.Requests;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Di2P1G3.Authentication.Api
{
    public class UpdateUserTrigger
    {
        private readonly IUserService _userService;

        public UpdateUserTrigger()
        {
            _userService = ServiceContainerBuilder.Instance.GetService<IUserService>();
        }

        [Function("user/update")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put")]
            HttpRequestData req,
            FunctionContext executionContext)
        {
            using var cancellationSource = new CancellationTokenSource();

            var queryDictionary = HttpUtility.ParseQueryString(req.Url.Query);
            var queriedUsername = queryDictionary["username"];

            var request = await req.ReadFromJsonAsync<UpdateUserRequest>(cancellationToken: cancellationSource.Token);

            if (request == null)
            {
                return req.RespondWithBadRequest();
            }

            var user = await _userService.FindUserAsync(user => user.Username == queriedUsername,
                cancellationSource.Token);
            if (user == null)
            {
                return req.RespondWithNotFound();
            }

            user.Firstname = request.Firstname;
            user.Name = request.Name;
            
            var success = await _userService.UpdateUserAsync(user, cancellationSource.Token);

            return success ? req.RespondWithNoContent() : req.RespondWithBadRequest();
        }
    }
}