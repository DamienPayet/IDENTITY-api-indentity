using System;

namespace Di2P1G3.Authentication.Api.Responses
{
    public class ContratResponse
    {
        public Guid Id { get; set; }
        
        public Guid IdContrat { get; set; }
        
        public string Name { get; set; }

        public string DateDebut { get; set; }
        
        public string DateFin { get; set; }
    }
}