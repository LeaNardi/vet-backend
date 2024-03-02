using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace vet_backend.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        public string ColorNombre { get; set; }
    }
}
