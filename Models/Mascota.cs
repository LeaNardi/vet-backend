using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace vet_backend.Models
{
    public class Mascota
    {
        [Key]
        public int MascotaId { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public int RazaId { get; set; }
        [ForeignKey("RazaId")]
        [JsonIgnore]
        public Raza Raza { get; set; }
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        [JsonIgnore]
        public Color Color { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
