using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain.Models;
using Infrastructure.Contexts;
using Domain.Models.Vendedores;
using Domain.ViewModels.Vendedores;
using System.ComponentModel;
using Azure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Domain.ViewModels.Marcas;
using Domain.Models.Marcas;

namespace WebApp.Controllers
{
    public class MarcaController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public MarcaController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var response = _appDbContext.Marcas.Where(x => x.Ativo == true).ToList();
            MarcaViewModel result = new MarcaViewModel()
            {
                Marcas = response.ToList(),
                MarcaIndividual = new Marca()
            };
            return View("Marca", result);
        }

        public async Task<MarcaViewModel> Listagem()
        {
            var response = _appDbContext.Marcas.Where(x => x.Ativo == true).ToList();
            MarcaViewModel result = new MarcaViewModel()
            {
                Marcas = response.ToList(),
                MarcaIndividual = new Marca()
            };
            return result;
        }

        [HttpPost]
        [Route("AdicionarMarca")]
        public async Task<IActionResult> AdicionarMarca(Marca marca)
        {
            List<Marca> list = _appDbContext.Marcas.ToList();
            if (list.Any())
            {
                marca.IdMarca = list.Max(x => x.IdMarca) + 1;
            }

            if(marca.IdMarca == 0)
            {
                marca.IdMarca = 1; // i dont fuck with zeroes
            }

            marca.Ativo = true;
            _appDbContext.Marcas.Add(marca);
            _appDbContext.SaveChanges();

            return View("Marca", await Listagem());
        }

        [HttpPost]
        [Route("AtualizarMarca")]
        public async Task<IActionResult> AtualizarMarca(Marca marca)
        {
            _appDbContext.Marcas.Update(marca);
            _appDbContext.SaveChanges();
            return View("Marca", await Listagem());
        }

        [HttpGet]
        [Route("SelecionarMarca")]
        public IActionResult SelecionarMarca(int IdMarca)
        {
            var marca = _appDbContext.Marcas.Where(x => x.IdMarca == IdMarca).First();

            MarcaViewModel result = new MarcaViewModel()
            {
                Marcas = new List<Marca>(),
                MarcaIndividual = marca
            };

            // sinceramente, vamos so fingir que ele ta indo pra uma tela diferente
            // mas na real eh a msma q a tela principal
            // so que eu nao vou carregar a tabela pra economizar request
            // e sim ta uma merda

            return View("Marca", result); 
        }
    }
}