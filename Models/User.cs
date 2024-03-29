﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace vet_backend.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Password { get; set; } // No se usa. Es solo para que quede guardada mientras se hacen las pruebas. Se compara con el Hash
        // Propiedades heredadas de IdentityUser:
        //string Id
        //string UserName
        //string PasswordHash
        //string Email
    }
}
