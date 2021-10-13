using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoGames.InputModel
{

    // Aqui é usado o Fail Fast
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Deve conter entre 1 e 100 carácteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Deve conter entre 1 e 100 carácteres")]
        public string Produtora { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Preço entre R$1,00 a R$1.000,00")]
        public double Preco { get; set; }
    }
}
