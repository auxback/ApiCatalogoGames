using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoGames.Entities.Exceptions
{
    public class GameJaCadastradoException : Exception
    {
        public GameJaCadastradoException() : base("Jogo já cadastrado") { }
    }
}
