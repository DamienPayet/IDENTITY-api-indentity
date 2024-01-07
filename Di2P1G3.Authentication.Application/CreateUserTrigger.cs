using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
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
    public class CreateUserTrigger
    {
        private readonly IUserService _userService;

        public CreateUserTrigger()
        {
            _userService = ServiceContainerBuilder.Instance.GetService<IUserService>();
        }

        [Function("user/register")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
            HttpRequestData req,
            FunctionContext executionContext)
        {
            using var cancellationSource = new CancellationTokenSource();

            var request = await req.ReadFromJsonAsync<CreateUserRequest>(cancellationToken: cancellationSource.Token);

            if (request == null)
            {
                return req.RespondWithBadRequest();
            }

            var user = await _userService.CreateUserAsync(request.Username, request.Firstname, request.Name, request.Password,
                cancellationSource.Token);

            return await req.RespondWithObjectAsJson(user);

        }
    }
}