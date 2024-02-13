using System.ComponentModel.DataAnnotations;

namespace vet_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
