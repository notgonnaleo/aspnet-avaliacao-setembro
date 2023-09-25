using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain.Models;
using Infrastructure.Contexts;
using Domain.Models.Clientes;
using Domain.ViewModels.Clientes;
using System.ComponentModel;
using Azure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Domain.Models.Vendedores;

namespace WebApp.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ClienteController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var response = _appDbContext.Clientes.Where(x => x.Ativo == true).ToList();

            var vendedores = _appDbContext.Vendedores
                    .Where(x => x.Ativo == true)
                    .Select(vendedor => new VendedorDropdownList()
                    {
                        IdVendedor = vendedor.IdVendedor,
                        NomeVendedor = vendedor.NomeVendedor
                    }).ToList();

            ClienteViewModel result = new ClienteViewModel()
            {
                Clientes = response.ToList(),
                ClienteIndividual = new Cliente(),
                DropdownVendedores = vendedores
            };
            return View("Cliente", result);
        }

        public async Task<ClienteViewModel> Listagem()
        {
            var response = _appDbContext.Clientes.Where(x => x.Ativo == true).ToList();

            var vendedores = _appDbContext.Vendedores
                    .Where(x => x.Ativo == true)
                    .Select(vendedor => new VendedorDropdownList()
                    {
                        IdVendedor = vendedor.IdVendedor,
                        NomeVendedor = vendedor.NomeVendedor
                    }).ToList();

            ClienteViewModel result = new ClienteViewModel()
            {
                Clientes = response.ToList(),
                ClienteIndividual = new Cliente(),
                DropdownVendedores = vendedores
            };
            return result;
        }

        [HttpPost]
        [Route("Cliente/AdicionarCliente")]
        public async Task<IActionResult> AdicionarCliente(Cliente cliente)
        {
            List<Cliente> list = _appDbContext.Clientes.ToList();
            if (list.Any())
            {
                cliente.IdCliente = list.Max(x => x.IdCliente) + 1;
            }

            if(cliente.IdCliente == 0)
            {
                cliente.IdCliente = 1; // i dont fuck with zeroes
            }

            cliente.Ativo = true;
            _appDbContext.Clientes.Add(cliente);
            _appDbContext.SaveChanges();

            return View("Cliente", await Listagem());
        }

        [HttpPost]
        [Route("Cliente/AtualizarCliente")]
        public async Task<IActionResult> AtualizarCliente(Cliente cliente)
        {
            _appDbContext.Clientes.Update(cliente);
            _appDbContext.SaveChanges();
            return View("Cliente", await Listagem());
        }

        [HttpGet]
        [Route("Cliente/SelecionarCliente")]
        public IActionResult Selecionar(int IdCliente)
        {
            var cliente = _appDbContext.Clientes.Where(x => x.IdCliente == IdCliente).First();
            var vendedor = _appDbContext.Vendedores
                .Where(x => x.IdVendedor == cliente.IdVendedor).First();

            var vendedorView = new VendedorDropdownList()
            {
                IdVendedor = cliente.IdVendedor,
                NomeVendedor = vendedor.NomeVendedor
            };

            ClienteViewModel result = new ClienteViewModel()
            {
                Clientes = new List<Cliente>(),
                ClienteIndividual = cliente,
                VendedorSelecionado = vendedorView
            };

            // sinceramente, vamos so fingir que ele ta indo pra uma tela diferente
            // mas na real eh a msma q a tela principal
            // so que eu nao vou carregar a tabela pra economizar request
            // e sim ta uma merda

            return View("Cliente", result); 
        }
    }
}