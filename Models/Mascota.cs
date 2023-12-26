using System.ComponentModel.DataAnnotations;

namespace vet_backend.Models
{
    public class Mascota
    {
        public int Id { get; set; }
        public string Raza { get; set; }
        public string Color { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public DataType FechaCreacion { get; set; }

    }
}
