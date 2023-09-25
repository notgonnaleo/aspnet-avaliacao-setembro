using Domain.Models.Marcas;
using Domain.Models.Vendedores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Produtos
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProduto { get; set; }
        [ForeignKey("Vendedores")]
        public int IdVendedor { get; set; }
        [ForeignKey("Marcas")]
        public int IdMarca { get; set; }
        public string NomeProduto { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }

        [JsonIgnore]
        public virtual Vendedor Vendedores { get; set; }
        [JsonIgnore]
        public virtual Marca Marcas { get; set; }
    }
}
