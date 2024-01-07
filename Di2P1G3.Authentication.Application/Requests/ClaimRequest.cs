using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.Api.Requests
{
    public class ClaimRequest
    {
        public Guid IdUser { get; set; }

        public string AppName { get; set; }
    }
}