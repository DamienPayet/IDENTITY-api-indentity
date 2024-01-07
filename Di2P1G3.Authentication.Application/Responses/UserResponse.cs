using System;
using System.Collections.Generic;

namespace Di2P1G3.Authentication.Api.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
            
        public string Username { get; set; }
        
        public string Name { get; set; }

        public string Firstname { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public IList<ClientApplicationResponse> Applications { get; set; }
    }
}