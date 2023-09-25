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

namespace WebApp.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProdutoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var response = _appDbContext.Produtos.Where(x => x.Ativo == true).ToList();

            var vendedores = _appDbContext.Vendedores
                    .Where(x => x.Ativo == true)
                    .Select(vendedor => new VendedorProdutoDropdownList()
                    {
                        IdVendedor = vendedor.IdVendedor,
                        NomeVendedor = vendedor.NomeVendedor
                    }).ToList();

            var marcas = _appDbContext.Marcas
                .Where(x => x.Ativo == true)
                .Select(marca => new MarcaDropdownList()
                {
                    IdMarca = marca.IdMarca,
                    NomeMarca = marca.NomeMarca
                }).ToList();

            ProdutoViewModel result = new ProdutoViewModel()
            {
                Produtos = response.ToList(),
                ProdutoIndividual = new Produto(),
                DropdownMarcas = marcas,
                DropdownVendedores = vendedores
            };
            return View("Produto", result);
        }

        public async Task<ProdutoViewModel> Listagem()
        {
            var response = _appDbContext.Produtos.Where(x => x.Ativo == true).ToList();

            var vendedores = _appDbContext.Vendedores
                    .Where(x => x.Ativo == true)
                    .Select(vendedor => new VendedorProdutoDropdownList()
                    {
                        IdVendedor = vendedor.IdVendedor,
                        NomeVendedor = vendedor.NomeVendedor
                    }).ToList();

            var marcas = _appDbContext.Marcas
                .Where(x => x.Ativo == true)
                .Select(marca => new MarcaDropdownList()
                {
                    IdMarca = marca.IdMarca,
                    NomeMarca = marca.NomeMarca
                }).ToList();

            ProdutoViewModel result = new ProdutoViewModel()
            {
                Produtos = response.ToList(),
                ProdutoIndividual = new Produto(),
                DropdownMarcas = marcas,
                DropdownVendedores = vendedores
            };
            return result;
        }

        [HttpPost]
        [Route("Produto/AdicionarProduto")]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            List<Produto> list = _appDbContext.Produtos.ToList();
            if (list.Any())
            {
                produto.IdProduto = list.Max(x => x.IdProduto) + 1;
            }

            if(produto.IdProduto == 0)
            {
                produto.IdProduto = 1; // i dont fuck with zeroes
            }

            produto.Ativo = true;
            _appDbContext.Produtos.Add(produto);
            _appDbContext.SaveChanges();

            return View("Produto", await Listagem());
        }

        [HttpPost]
        [Route("Produto/AtualizarProduto")]
        public async Task<IActionResult> AtualizarProduto(Produto produto)
        {
            _appDbContext.Produtos.Update(produto);
            _appDbContext.SaveChanges();
            return View("Produto", await Listagem());
        }

        [HttpGet]
        [Route("Produto/SelecionarProduto")]
        public IActionResult Selecionar(int IdProduto)
        {
            var produto = _appDbContext.Produtos.Where(x => x.IdProduto == IdProduto).First();
            
            var vendedor = _appDbContext.Vendedores
                .Where(x => x.IdVendedor == produto.IdVendedor).First();
            var marca = _appDbContext.Marcas
                .Where(x => x.IdMarca == produto.IdMarca).First();

            var vendedorView = new VendedorProdutoDropdownList()
            {
                IdVendedor = produto.IdVendedor,
                NomeVendedor = vendedor.NomeVendedor
            };

            var marcaView = new MarcaDropdownList()
            {
                IdMarca = produto.IdMarca,
                NomeMarca = marca.NomeMarca
            };

            ProdutoViewModel result = new ProdutoViewModel()
            {
                Produtos = new List<Produto>(),
                ProdutoIndividual = produto,
                VendedorSelecionado = vendedorView,
                MarcaSelecionado = marcaView
            };

            // sinceramente, vamos so fingir que ele ta indo pra uma tela diferente
            // mas na real eh a msma q a tela principal
            // so que eu nao vou carregar a tabela pra economizar request
            // e sim ta uma merda

            return View("Produto", result); 
        }
    }
}