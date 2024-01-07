using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Di2P1G3.Authentication.SharedKernel
{
    public class Contrat : Entity
    {
        public Guid IdContrat { get; set; }
        
        public string Name { get; set; }

        public string DateDebut { get; set; }
        
        public string DateFin { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}