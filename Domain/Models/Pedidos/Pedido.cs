using Domain.Models.Clientes;
using Domain.Models.Produtos;
using Domain.Models.Vendedores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Pedidos
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPedido { get; set; }
        [ForeignKey("Produtos")]
        public int IdProduto { get; set; }
        [ForeignKey("Clientes")]
        public int IdCliente { get; set; }
        [ForeignKey("Vendedores")]
        public int IdVendedor { get; set; }
        public int QtdeProduto { get; set; }
        public decimal ValorTotal { get; set; }

        [JsonIgnore]
        public virtual Vendedor Vendedores { get; set; }
        [JsonIgnore]
        public virtual Produto Produtos { get; set; }
        [JsonIgnore]
        public virtual Cliente Clientes { get; set; }
    }
}
