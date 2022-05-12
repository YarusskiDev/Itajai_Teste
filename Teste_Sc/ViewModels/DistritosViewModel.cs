using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Sc.ViewModels
{

    public class DistritosViewModel
    {

        public string Sigla { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Nome { get; set; }

        [DisplayName("Digite o estado pela sigla UF o qual quer ver todas as cidades e recebelas por email")]
        [StringLength(2, ErrorMessage = "O campo {0} deve é obrigatório e deve ter no minimo {1} caracteres",MinimumLength =2)]
        public string UF { get; set; }

        
    }

}
