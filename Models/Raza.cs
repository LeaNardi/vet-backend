using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace vet_backend.Models
{
    public class Raza
    {
        [Key]
        public int RazaId { get; set; }
        public string RazaNombre { get; set; }
    }
}
