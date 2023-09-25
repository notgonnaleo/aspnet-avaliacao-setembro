using Domain.Models.Vendedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Vendedores
{
    public class VendedorViewModel
    {
        public List<Vendedor> Vendedores { get; set; }
        public Vendedor VendedorIndividual { get; set; }
    }
}
