using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using Di2P1G3.Dependency.Injection;
using Di2P1G3.Authentication.Core.Interfaces;
using System.Threading;
using Di2P1G3.Authentication.SharedKernel;
using System.Threading.Tasks;
using Di2P1G3.Authentication.Api.Requests;
using System.Web;

namespace Di2P1G3.Authentication.Api
{
    public class AppFunction
    {

        private readonly IClientApplicationService _appService;

        public AppFunction()
        {
            _appService = ServiceContainerBuilder.Instance.GetService<IClientApplicationService>();
        }

        [Function("AddAppFunction")]
        public async Task<HttpResponseData> Add(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequestData req,
            ILogger log)
        {
            using var cancellationSource = new CancellationTokenSource();

            var request = await req.ReadFromJsonAsync<CreateAppRequest>(cancellationToken: cancellationSource.Token);

            if (request == null)
            {
                return req.RespondWithBadRequest();
            }

            var app = await _appService.AddAsync(request.Name, request.RedirectUrl, cancellationSource.Token);

            return await req.RespondWithObjectAsJson(app);
        }

        [Function("ReadAppFunction")]
        public async Task<ClientApplication> Read(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
                HttpRequestData req,
        ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string name = data?.name;


            var ca = await _appService.FindAsync(p => p.Name == name, new CancellationTokenSource().Token);

            return ca;
        }

        [Function("ReadAllAppFunction")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            using var cancellationSource = new CancellationTokenSource();

            var apps = await _appService.FindAppsAsync(app => true, cancellationSource.Token);

            return await req.RespondWithObjectAsJson(apps);


        }

        [Function("UpdateAppFunction")]
        public async Task<HttpResponseData> update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get" , "post")] HttpRequestData req,
            FunctionContext executionContext)
        {

            using var cancellationSource = new CancellationTokenSource();

            var queryDictionary = HttpUtility.ParseQueryString(req.Url.Query);
            var queriedName = queryDictionary["name"];

            var request = await req.ReadFromJsonAsync<CreateAppRequest>(cancellationToken: cancellationSource.Token);

            if (request == null)
            {
                return req.RespondWithBadRequest();
            }

            var app = await _appService.FindAsync(app => app.Name == queriedName, cancellationSource.Token);
            if (app == null)
            {
                return req.RespondWithNotFound();
            }

            app.Name = request.Name;
            app.RedirectUrl = request.RedirectUrl;

            var success = await _appService.UpdateAppAsync(app, cancellationSource.Token);
            return success ? req.RespondWithNoContent() : req.RespondWithBadRequest();
        }



            [Function("DeleteAppFunction")]
        public async Task<HttpResponseData> Delete(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
                    HttpRequestData req,
        ILogger log)
        {
            using var cancellationSource = new CancellationTokenSource();

            var queryDictionary = HttpUtility.ParseQueryString(req.Url.Query);
            var queriedName = queryDictionary["Name"];

            if (queriedName == null)
            {
                return req.RespondWithBadRequest();
            }

            var app = await _appService.FindAsync(app => app.Name == queriedName, cancellationSource.Token);
            if (app == null)
            {
                return req.RespondWithNotFound();
            }

            var success = await _appService.DeleteAsync(app.Name, cancellationSource.Token);

            return success ? req.RespondWithNoContent() : req.RespondWithBadRequest();

        }
    }
}
