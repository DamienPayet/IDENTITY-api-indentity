using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Dependency.Injection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Di2P1G3.Authentication.Api
{
    public class RemoveUserTrigger
    {
        private readonly IUserService _userService;
        
        public RemoveUserTrigger()
        {
            _userService = ServiceContainerBuilder.Instance.GetService<IUserService>();
        }
        
        [Function("user/remove")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequestData req,
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
            if (user == null)
            {
                return req.RespondWithNotFound();
            }

            var success = await _userService.DeleteUserAsync(user, cancellationSource.Token);
            
            return success ? req.RespondWithNoContent() : req.RespondWithBadRequest();
        }
    }
}