﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace vet_backend.Models
{
    public class User : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [Required]
        public string Password { get; set; } // Usada temporalmente hasta comparar con el Hash
        // Propiedades heredadas de IdentityUser:
        //string Id
        //string UserName
        //string PasswordHash
        //colection Roles
        //string Email

    }
}
