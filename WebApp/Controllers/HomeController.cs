using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain.Models;
using Infrastructure.Contexts;
using Domain.Models.Vendedores;
using Domain.ViewModels.Vendedores;
using System.ComponentModel;
using Azure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var response = _appDbContext.Vendedores.Where(x => x.Ativo == true).ToList();
            VendedorViewModel result = new VendedorViewModel()
            {
                Vendedores = response.ToList(),
                VendedorIndividual = new Vendedor()
            };
            return View("Index", result);
        }

        public async Task<VendedorViewModel> Listagem()
        {
            var response = _appDbContext.Vendedores.Where(x => x.Ativo == true).ToList();
            VendedorViewModel result = new VendedorViewModel()
            {
                Vendedores = response.ToList(),
                VendedorIndividual = new Vendedor()
            };
            return result;
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> Adicionar(Vendedor vendedor)
        {
            List<Vendedor> list = _appDbContext.Vendedores.ToList();
            if (list.Any())
            {
                vendedor.IdVendedor = list.Max(x => x.IdVendedor) + 1;
            }

            if(vendedor.IdVendedor == 0)
            {
                vendedor.IdVendedor = 1; // i dont fuck with zeroes
            }

            vendedor.Ativo = true;
            _appDbContext.Vendedores.Add(vendedor);
            _appDbContext.SaveChanges();

            return View("Index", await Listagem());
        }

        [HttpPost]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar(Vendedor vendedor)
        {
            _appDbContext.Vendedores.Update(vendedor);
            _appDbContext.SaveChanges();
            return View("Index", await Listagem());
        }

        [HttpGet]
        [Route("Selecionar")]
        public IActionResult Selecionar(int IdVendedor)
        {
            var vendedor = _appDbContext.Vendedores.Where(x => x.IdVendedor == IdVendedor).First();

            VendedorViewModel result = new VendedorViewModel()
            {
                Vendedores = new List<Vendedor>(),
                VendedorIndividual = vendedor
            };

            // sinceramente, vamos so fingir que ele ta indo pra uma tela diferente
            // mas na real eh a msma q a tela principal
            // so que eu nao vou carregar a tabela pra economizar request
            // e sim ta uma merda

            return View("Index", result); 
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}