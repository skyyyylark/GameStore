using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Services
{
    public interface IGameService
    {
        public List<GameDTO> GetGames();
        public Task<GameDetailsDTO> GetGameById(int id);
        public Task<int> AddGame(GameDTO gameDTO);
        public Task<int> DeleteGame(int id);
        public Task<int> UpdateGame(GameDTO gameDTO);
    }
}
