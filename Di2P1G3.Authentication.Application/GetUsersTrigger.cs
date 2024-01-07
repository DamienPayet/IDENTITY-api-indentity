using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Di2P1G3.Authentication.Api.Responses;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Dependency.Injection;
using MapsterMapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Di2P1G3.Authentication.Api
{
    public class GetUsersTrigger
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public GetUsersTrigger()
        {
            _userService = ServiceContainerBuilder.Instance.GetService<IUserService>();
            _mapper = ServiceContainerBuilder.Instance.GetService<IMapper>();
        }
        
        [Function("users")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            using var cancellationSource = new CancellationTokenSource();

            var users = await _userService.FindUsersAsync(user => true, cancellationSource.Token);

            return await req.RespondWithObjectAsJson(_mapper.Map<UserResponse[]>(users));
        }
    }
}