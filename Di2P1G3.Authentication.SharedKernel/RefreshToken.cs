using System;

namespace Di2P1G3.Authentication.SharedKernel
{
    public class RefreshToken : BaseToken
    {
        public Guid IdAccessToken { get; set; }

        public AccessToken AccessToken { get; set; }
    }
}