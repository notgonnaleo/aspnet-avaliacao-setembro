using Domain.Models.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Clientes
{
    public class ClienteViewModel // vo faze assim mesmo caguei
    {
        public List<Cliente> Clientes { get; set; }
        public Cliente ClienteIndividual { get; set; }
        public List<VendedorDropdownList>? DropdownVendedores { get; set; }
        public VendedorDropdownList? VendedorSelecionado { get; set; }
    }

    public class VendedorDropdownList
    {
        public int? IdVendedor { get; set; }
        public string? NomeVendedor { get; set; }
    }
}
