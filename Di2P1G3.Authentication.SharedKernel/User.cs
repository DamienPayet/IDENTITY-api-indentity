using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Di2P1G3.Authentication.SharedKernel
{
    public class User : Entity
    {
        public string Username { get; set; }
        
        public string Name { get; set; }

        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Password salt")]
        [Column("password_salt")]
        public string PasswordSalt { get; set; }

        [Required]
        [Display(Name = "Password hash")]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        public IList<ClientApplication> Applications { get; set; }

        public IEnumerable<AccessToken> Tokens { get; set; }
    }
}