using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoGames.Entities.Exceptions;
using ApiCatalogoGames.InputModel;
using ApiCatalogoGames.Services;
using ApiCatalogoGames.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoGames.Controllers.V1
{
    //Geralmente, usa-se a classe object nos parâm como tipo genérico
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Buscar todos os games de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os games sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de games</response>
        /// <response code="204">Caso não haja games</response>
        [HttpGet]
        // Abaixo, definição da paginação
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var games = await gameService.Obter(pagina, quantidade);

            if (games.Count() == 0) return NoContent();

            return Ok(games);
        }

        //---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Buscar um game pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do games buscado</param>
        /// <response code="200">Retorna o game filtrado</response>
        /// <response code="204">Caso não haja game com este id</response>
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Obter([FromRoute]Guid idGame)
        {
            var game = await gameService.Obter(idGame);

            if (game == null) return NoContent();

            return Ok(game);
        }

        
        /// <summary>
        /// Inserir um game no catálogo
        /// </summary>
        /// <param name="jogoInputModel">Dados do game a ser inserido</param>
        /// <response code="200">Caso o game seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um game com mesmo nome da mesma produtora</response>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InserirGame([FromBody] GameInputModel gameInputModel)
        {
            try 
            {
                var game = await gameService.Inserir(gameInputModel);
                
                return Ok(game);
            }
            catch (GameJaCadastradoException ex)
            {
                return UnprocessableEntity("Esse nome já existe");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Atualizar um game no catálogo
        /// </summary>
        /// /// <param name="idJogo">Id do game a ser atualizado</param>
        /// <param name="jogoInputModel">Novos dados para atualizar o game indicado</param>
        /// <response code="200">Caso o game seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>
        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> AtualizarGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await gameService.Atualizar(idGame, gameInputModel);
                return Ok();
            }
            catch(GameNaoCadastradoException ex)
            {
                return NotFound("Game não existe");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Atualizar o preço de um game
        /// </summary>
        /// /// <param name="idJogo">Id do game a ser atualizado</param>
        /// <param name="preco">Novo preço do jogo</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um game com este Id</response>
        [HttpPatch("{idGame:guid}/preco/{preco:double}")]
        //Patch só sobrescreve
        public async Task<ActionResult> AtualizarGame([FromRoute] Guid idGame, [FromRoute] double preco)
        {
            try
            {
                await gameService.Atualizar(idGame, preco);
                return Ok();
            }
            catch(GameNaoCadastradoException ex)
            {
                return NotFound("Game não existe");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Excluir um game
        /// </summary>
        /// /// <param name="idJogo">Id do game a ser excluído</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um game com este Id</response>
        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> ApagarGame([FromRoute] Guid idGame)
        {
            try
            {
                await gameService.Remover(idGame);
                return Ok();
            }
            catch(GameNaoCadastradoException ex)
            {
                return NotFound("Game não existe");
            }

        }
    }
}
