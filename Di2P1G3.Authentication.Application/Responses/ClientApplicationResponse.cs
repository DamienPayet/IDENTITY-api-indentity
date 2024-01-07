using System;

namespace Di2P1G3.Authentication.Api.Responses
{
    public class ClientApplicationResponse
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string RedirectUrl { get; set; }
    }
}