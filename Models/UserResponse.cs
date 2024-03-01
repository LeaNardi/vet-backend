using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace vet_backend.Models
{
    public class UserResponse
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public string username { get; set; }
        public string email { get; set; }
        public string role { get; set; }

    }
}
