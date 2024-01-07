using System;
using System.ComponentModel.DataAnnotations;

namespace Di2P1G3.Authentication.SharedKernel
{
    public abstract class Entity
    {
        [Key] public Guid Id { get; set; }
    }
}