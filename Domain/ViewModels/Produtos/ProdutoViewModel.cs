using Domain.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Produtos
{
    public class ProdutoViewModel // vo faze assim mesmo caguei
    {
        public List<Produto> Produtos { get; set; }
        public Produto ProdutoIndividual { get; set; }
        public List<VendedorProdutoDropdownList>? DropdownVendedores { get; set; }
        public VendedorProdutoDropdownList? VendedorSelecionado { get; set; }
        public List<MarcaDropdownList>? DropdownMarcas { get; set; }
        public MarcaDropdownList? MarcaSelecionado { get; set; }
    }

    public class VendedorProdutoDropdownList
    {
        public int? IdVendedor { get; set; }
        public string? NomeVendedor { get; set; }
    }

    public class MarcaDropdownList
    {
        public int? IdMarca { get; set; }
        public string? NomeMarca { get; set; }
    }
}
