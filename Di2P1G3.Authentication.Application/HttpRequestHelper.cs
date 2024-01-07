using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace Di2P1G3.Authentication.Api
{
    public static class HttpRequestHelper
    {
        public static async Task<HttpResponseData> RespondWithObjectAsJson(this HttpRequestData req, object obj)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await response.WriteStringAsync(JsonSerializer.Serialize(obj));

            return response;
        }
        
        public static HttpResponseData RespondWithBadRequest(this HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.BadRequest);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            return response;
        }
        
        public static HttpResponseData RespondWithNotFound(this HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.NotFound);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            return response;
        }
        
        public static HttpResponseData RespondWithNoContent(this HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.NoContent);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            return response;
        }
    }
}