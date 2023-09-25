using Domain.Models.Marcas;
using Domain.Models.Vendedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Marcas
{
    public class MarcaViewModel
    {
        public List<Marca> Marcas { get; set; }
        public Marca MarcaIndividual { get; set; }
    }
}
