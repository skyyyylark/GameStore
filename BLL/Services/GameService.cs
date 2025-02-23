using Abstractions.Interfaces.DataSources;
using Abstractions.Interfaces.Services;
using DAL.EntityFramework;
using Mapster;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameService : IGameService 
    {
        private readonly IGameDataSource _gameDataSource;
        public GameService(IGameDataSource gameDataSource)
        {
            _gameDataSource = gameDataSource;
        }

        public List<GameDTO> GetGames()
        {
            return _gameDataSource.GetItems().ProjectToType<GameDTO>().ToList();
        }

        public async Task<int> AddGame(GameDTO gameDTO)
        {
            var game =  gameDTO.Adapt<Game>();
            game = await _gameDataSource.AddItemAsync(game);
            await _gameDataSource.SaveChangesAsync();
            return game.Id;
        }

        public async Task<int> DeleteGame(int id)
        {
            var game = _gameDataSource.DeleteItemAsync(id);
            await _gameDataSource.SaveChangesAsync();
            return game.Id;
        }

        public async Task<int> UpdateGame(GameDTO gameDTO)
        {
            var game = gameDTO.Adapt<Game>();
            await _gameDataSource.UpdateItemAsync(game);
            await _gameDataSource.SaveChangesAsync();
            return game.Id;
        }

        public async Task<GameDetailsDTO> GetGameById(int id)
        {
            return await _gameDataSource.GetGameWithCategory(id);
        }
    }
}
