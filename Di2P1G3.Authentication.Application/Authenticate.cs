using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Authentication.SharedKernel;
using Di2P1G3.Dependency.Injection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Di2P1G3.Authentication.Api
{
	public class Authenticate
	{
		private readonly IUserService _userService;
		private readonly ITokenService _tokenService;
		public Authenticate()
		{
			_userService = ServiceContainerBuilder.Instance.GetService<IUserService>();
			_tokenService = ServiceContainerBuilder.Instance.GetService<ITokenService>();
		}
		[Function("Authenticate")]
		public async Task<HttpResponseData> Run(
				[HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
				FunctionContext executionContext)
		{
			
			using var cancellationSource = new CancellationTokenSource();

			var queryDictionary = HttpUtility.ParseQueryString(req.Url.Query); 
			var queriedUsername = queryDictionary["username"];
			var queriedPassword = queryDictionary["password"];

			if (queriedUsername != null && queriedPassword !=null)
			{
				if (await _userService.AreCredentialsValid(queriedUsername, queriedPassword,cancellationSource.Token)){
					var user = await _userService.FindUserAsync(user => user.Username == queriedUsername, cancellationSource.Token);
					if (user != null)
					{
						var token = await _tokenService.CreateBearerTokenAsync(user, cancellationSource.Token);
						var coldtoken = await _tokenService.CreateRefreshTokenAsync(token, cancellationSource.Token);
						Dictionary<string, Object> response = new Dictionary<string, object>();
						response.Add("bearertoken", token.Token);
						return token == null ? req.RespondWithNotFound() : await req.RespondWithObjectAsJson(response);
					}
				}
				
			}
			return req.RespondWithBadRequest();
		}
		[Function("ForgeToken")]
		public async Task<HttpResponseData> forging(
				[HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
				FunctionContext executionContext)
		{

			using var cancellationSource = new CancellationTokenSource();

			var queryDictionary = HttpUtility.ParseQueryString(req.Url.Query);
			var queriedToken = queryDictionary["bearer"];

			if (queriedToken != null)
			{
				if (_tokenService.checkToken(queriedToken, cancellationSource.Token))
				{
					var bearerToken = await _tokenService.FindTokenAsync(token => token.Token == queriedToken, cancellationSource.Token);
					if (bearerToken != null)
					{
						var usr = await _userService.FindUserAsync(user => user.Id == bearerToken.IdUser, cancellationSource.Token);
						var token = await _tokenService.CreateAccessTokenAsync(usr, cancellationSource.Token);
						var coldtoken = await _tokenService.CreateRefreshTokenAsync(token, cancellationSource.Token);
						Dictionary<string, Object> response = new Dictionary<string, object>();
						response.Add("token", token.Token);
						response.Add("refreshToken", coldtoken.Token);
						response.Add("userName", usr.Username);
						return token == null ? req.RespondWithNotFound() : await req.RespondWithObjectAsJson(response);
					}
				}
			}
			return req.RespondWithBadRequest();
		}

	}
}
