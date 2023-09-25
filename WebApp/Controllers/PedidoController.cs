using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain.Models;
using Infrastructure.Contexts;
using Domain.Models.Clientes;
using Domain.ViewModels.Produtos;
using System.ComponentModel;
using Azure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Domain.Models.Vendedores;
using Domain.Models.Produtos;
using Domain.ViewModels.Pedidos;
using Domain.Models.Pedidos;

namespace WebApp.Controllers
{
    public class PedidoController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PedidoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var response = _appDbContext.Pedidos.ToList();

            var vendedores = _appDbContext.Vendedores
                .Where(x => x.Ativo == true)
                .Select(vendedor => new VendedorPedidoDropdownList()
                {
                    IdVendedor = vendedor.IdVendedor,
                    NomeVendedor = vendedor.NomeVendedor
                }).ToList();

            var clientes = _appDbContext.Clientes
                .Where(x => x.Ativo == true)
                .Select(cliente => new ClienteDropdownList()
                {
                    IdCliente = cliente.IdCliente,
                    NomeCliente = cliente.NomeCliente
                }).ToList();

            var produtos = _appDbContext.Produtos
                .Where(x => x.Ativo == true)
                .Select(produto => new ProdutoDropdownList()
                {
                    IdProduto = produto.IdProduto,
                    NomeProduto = produto.NomeProduto
                }).ToList();

            PedidoViewModel result = new PedidoViewModel()
            {
                Pedidos = response.ToList(),
                PedidoIndividual = new Pedido(),
                DropdownClientes = clientes,
                DropdownVendedores = vendedores,
                DropdownProdutos = produtos,
            };
            return View("Pedido", result);
        }

        public async Task<PedidoViewModel> Listagem()
        {
            var response = _appDbContext.Pedidos.ToList();

            var vendedores = _appDbContext.Vendedores
                .Where(x => x.Ativo == true)
                .Select(vendedor => new VendedorPedidoDropdownList()
                {
                    IdVendedor = vendedor.IdVendedor,
                    NomeVendedor = vendedor.NomeVendedor
                }).ToList();

            var clientes = _appDbContext.Clientes
                .Where(x => x.Ativo == true)
                .Select(cliente => new ClienteDropdownList()
                {
                    IdCliente = cliente.IdCliente,
                    NomeCliente = cliente.NomeCliente
                }).ToList();

            var produtos = _appDbContext.Produtos
                .Where(x => x.Ativo == true)
                .Select(produto => new ProdutoDropdownList()
                {
                    IdProduto = produto.IdProduto,
                    NomeProduto = produto.NomeProduto
                }).ToList();

            PedidoViewModel result = new PedidoViewModel()
            {
                Pedidos = response.ToList(),
                PedidoIndividual = new Pedido(),
                DropdownClientes = clientes,
                DropdownVendedores = vendedores,
                DropdownProdutos = produtos,
            };
            return result;
        }

        [HttpPost]
        [Route("Pedido/AdicionarPedido")]
        public async Task<IActionResult> AdicionarPedido(Pedido pedido)
        {
            List<Pedido> list = _appDbContext.Pedidos.ToList();
            if (list.Any())
            {
                pedido.IdProduto = list.Max(x => x.IdPedido) + 1;
            }

            if(pedido.IdProduto == 0)
            {
                pedido.IdProduto = 1; // i dont fuck with zeroes
            }

            var valorProduto = _appDbContext.Produtos.Where(x => x.IdProduto == pedido.IdProduto).First();
            pedido.ValorTotal = pedido.QtdeProduto * valorProduto.Preco;

            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            return View("Pedido", await Listagem());
        }

        [HttpPost]
        [Route("Pedido/AtualizarPedido")]
        public async Task<IActionResult> AtualizarPedido(Pedido pedido)
        {
            var valorProduto = _appDbContext.Produtos.Where(x => x.IdProduto == pedido.IdProduto).First();
            pedido.ValorTotal = pedido.QtdeProduto * valorProduto.Preco;

            _appDbContext.Pedidos.Update(pedido);
            _appDbContext.SaveChanges();
            return View("Pedido", await Listagem());
        }

        [HttpGet]
        [Route("Pedido/SelecionarPedido")]
        public IActionResult Selecionar(int IdPedido)
        {

            var pedido = _appDbContext.Pedidos.Where(x => x.IdPedido == IdPedido).First();
            
            var vendedor = _appDbContext.Vendedores
                .Where(x => x.IdVendedor == pedido.IdVendedor).First();
            var cliente = _appDbContext.Clientes
                .Where(x => x.IdCliente == pedido.IdCliente).First();
            var produto = _appDbContext.Produtos
                .Where(x => x.IdProduto == pedido.IdProduto).First();

            var vendedorView = new VendedorPedidoDropdownList()
            {
                IdVendedor = pedido.IdVendedor,
                NomeVendedor = vendedor.NomeVendedor
            };

            var clienteView = new ClienteDropdownList()
            {
                IdCliente = pedido.IdCliente,
                NomeCliente = cliente.NomeCliente
            };

            var produtoView = new ProdutoDropdownList()
            {
                IdProduto = pedido.IdVendedor,
                NomeProduto = vendedor.NomeVendedor,
                ValorTotal = pedido.ValorTotal
            };


            PedidoViewModel result = new PedidoViewModel()
            {
                Pedidos = new List<Pedido>(),
                PedidoIndividual = pedido,
                VendedorSelecionado = vendedorView,
                ClienteSelecionado = clienteView,
                ProdutoSelecionado = produtoView
            };

            // sinceramente, vamos so fingir que ele ta indo pra uma tela diferente
            // mas na real eh a msma q a tela principal
            // so que eu nao vou carregar a tabela pra economizar request
            // e sim ta uma merda

            return View("Pedido", result); 
        }
    }
}