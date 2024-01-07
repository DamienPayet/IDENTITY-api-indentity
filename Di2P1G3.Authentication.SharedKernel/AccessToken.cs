using System;
using System.Collections.Generic;

namespace Di2P1G3.Authentication.SharedKernel
{
    public class AccessToken : BaseToken
    {
        public Guid IdUser { get; set; }

        public User User { get; set; }
        
        public IEnumerable<RefreshToken> RefreshTokens { get; set; }
    }
}