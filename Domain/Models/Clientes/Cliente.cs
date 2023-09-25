using Domain.Models.Vendedores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Clientes
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCliente { get; set; }
        [ForeignKey("Vendedores")]
        public int IdVendedor { get; set; }
        public string NomeCliente { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; }

        [JsonIgnore]
        public virtual Vendedor Vendedores { get; set; }
    }
}
