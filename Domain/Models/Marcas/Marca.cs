using Domain.Models.Vendedores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Marcas
{
    [Table("Marca")]
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMarca { get; set; }
        public string NomeMarca { get; set; }
        public bool Ativo { get; set; }
    }
}
