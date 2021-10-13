using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoGames.Entities.Exceptions
{
    public class GameNaoCadastradoException : Exception
    {
        public GameNaoCadastradoException() : base("Jogo não cadastrado") { }
    }
}
