using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoGames.Controllers.V1
{

    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> Obter()
        {
            return Ok();
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<object>> Obter(Guid idGame)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>> InserirGame(object game)
        {
            return Ok();
        }


        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> AtualizarGame(Guid idGame, object game)
        {
            return Ok();
        }

        [HttpPatch("{idGame:guid}/preco/{preco:double}")]
        //Patch só sobrescreve
        public async Task<ActionResult> AtualizarGame(Guid idGame, double preco)
        {
            return Ok();
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<AcceptedResult> ApagarGame(Guid idGame)
        {
            return Ok();
        }
    }
}
