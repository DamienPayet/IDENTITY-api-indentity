using System.Collections.Generic;

namespace Di2P1G3.Authentication.SharedKernel
{
    public class ClientApplication : Entity
    {
        public string Name { get; set; }

        public string RedirectUrl { get; set; }

        public IList<User> Users { get; set; }
    }
}