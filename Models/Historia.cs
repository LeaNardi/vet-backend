using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;

namespace vet_backend.Models
{
    public class Historia
    {
        [Key]
        public int HistoriaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Veterinario { get; set; }
        public string Detalle { get; set; }
        public int MascotaId { get; set; }
        [ForeignKey("MascotaId")]
        [JsonIgnore]
        public Mascota Mascota { get; set; }
    }
}
