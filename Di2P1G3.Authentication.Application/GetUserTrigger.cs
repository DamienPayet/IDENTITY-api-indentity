using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Di2P1G3.Authentication.Api.Responses;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Dependency.Injection;
using Mapster;
using MapsterMapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Di2P1G3.Authentication.Api
{
    public class GetUserTrigger
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public GetUserTrigger()
        {
            _userService = ServiceContainerBuilder.Instance.GetService<IUserService>();
            _mapper = ServiceContainerBuilder.Instance.GetService<IMapper>();
        }
        
        [Function("user")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            using var cancellationSource = new CancellationTokenSource();

            var queryDictionary = HttpUtility.ParseQueryString(req.Url.Query);
            var queriedUsername = queryDictionary["username"];

            if (queriedUsername == null)
            {
                return req.RespondWithBadRequest();
            }
            
            var user = await _userService.FindUserAsync(user => user.Username == queriedUsername, cancellationSource.Token);

            return user == null ? req.RespondWithNotFound() : await req.RespondWithObjectAsJson(_mapper.Map<UserResponse>(user));
        }
    }
}