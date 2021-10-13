using ApiCatalogoGames.Entities;
using ApiCatalogoGames.InputModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoGames.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "FIFA 21", Produtora = "EA", Preco = 200} }
        };

        public Task<List<Game>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(games.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Game> Obter(Guid id)
        {
            if (!games.ContainsKey(id)) return null;

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Obter(string nome, string produtora)
        {
            // link ?
            return Task.FromResult(games.Values.Where(game => game.Nome.Equals(nome) && game.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Game>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Game>();

            foreach (var game in games.Values)
            {
                if (game.Nome.Equals(nome) && game.Produtora.Equals(produtora)) retorno.Add(game);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Atualizar(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // fecha conex com BD
        }

        public Task Obter()
        {
            throw new NotImplementedException();
        }
    }
}
