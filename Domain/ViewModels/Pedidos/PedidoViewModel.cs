using Domain.Models.Pedidos;
using Domain.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Pedidos
{
    public class PedidoViewModel // vo faze assim mesmo caguei
    {
        public List<Pedido> Pedidos { get; set; }
        public Pedido PedidoIndividual { get; set; }
        public List<VendedorPedidoDropdownList>? DropdownVendedores { get; set; }
        public VendedorPedidoDropdownList? VendedorSelecionado { get; set; }
        public List<ClienteDropdownList>? DropdownClientes { get; set; }
        public ClienteDropdownList? ClienteSelecionado { get; set; }
        public List<ProdutoDropdownList>? DropdownProdutos { get; set; }
        public ProdutoDropdownList? ProdutoSelecionado { get; set; }

    }

    public class VendedorPedidoDropdownList
    {
        public int? IdVendedor { get; set; }
        public string? NomeVendedor { get; set; }
    }

    public class ClienteDropdownList
    {
        public int? IdCliente { get; set; }
        public string? NomeCliente { get; set; }
    }

    public class ProdutoDropdownList
    {
        public int? IdProduto { get; set; }
        public string? NomeProduto { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}
