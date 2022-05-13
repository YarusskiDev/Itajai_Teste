using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Sc.ViewModels
{
    public class EstadosViewModel
    {

        [DisplayName("Estados")]
        public string Nome { get; set; }

        public string Sigla { get; set; }
    }
}
