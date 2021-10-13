using ApiCatalogoGames.Entities;
using ApiCatalogoGames.Entities.Exceptions;
using ApiCatalogoGames.InputModel;
using ApiCatalogoGames.InputModel.Repositories;
using ApiCatalogoGames.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoGames.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Obter(int pagina, int quantidade)
        {
            // "quantidade" é o total de games mostrados.
            // "pagina" é tipo página de pesquisa do Google, mostra outros "quantidade"
            var games = await gameRepository.Obter(pagina, quantidade);

            return games.Select(game => new GameViewModel { 
                Id = game.Id,
                Nome = game.Nome,
                Produtora = game.Produtora,
                Preco = game.Preco
            }).ToList();
        }

        public async Task<GameViewModel> Obter(Guid id)
        {
            var game = await gameRepository.Obter(id);

            if (game == null) return null;

            return new GameViewModel
            {
                Id = game.Id,
                Nome = game.Nome,
                Produtora = game.Produtora,
                Preco = game.Preco
            };
        }

        public async Task<GameViewModel> Inserir(GameInputModel game)
        {
            var entidadeGame = await gameRepository.Obter(game.Nome, game.Produtora);

            if (entidadeGame.Count > 0)
                throw new GameJaCadastradoException();

            var GameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Nome = game.Nome,
                Produtora = game.Produtora,
                Preco = game.Preco
            };

            await gameRepository.Inserir(GameInsert);

            return new GameViewModel
            {
                Id = GameInsert.Id,
                Nome = game.Nome,
                Produtora = game.Produtora,
                Preco = game.Preco
            };
        }

        public async Task Atualizar(Guid id, GameInputModel game)
        {
            var entidadeGame = await gameRepository.Obter(id);

            if (entidadeGame == null) throw new GameNaoCadastradoException();

            entidadeGame.Nome = game.Nome;
            entidadeGame.Produtora = game.Produtora;
            entidadeGame.Preco = game.Preco;

            await gameRepository.Atualizar(entidadeGame);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeGame = await gameRepository.Obter(id);

            if (entidadeGame == null) throw new GameNaoCadastradoException();

            entidadeGame.Preco = preco;

            await gameRepository.Atualizar(entidadeGame);
        }

        public async Task Remover(Guid id)
        {
            var game = await gameRepository.Obter(id);

            if (game == null) throw new GameNaoCadastradoException();

            await gameRepository.Remover(id);
        }


        // Fecha a conexão
        public void Dispose()
        {
            gameRepository?.Dispose();
        }
    }
}
