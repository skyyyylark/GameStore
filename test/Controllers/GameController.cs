using Abstractions.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet("getGameById")]
        public async Task<GameDetailsDTO> GetGameById(int id)
        {
            return await _gameService.GetGameById(id);
        }
        [HttpGet("getAllGames")]
        public async Task<List<GameDTO>> GetGames()
        {
            return await Task.FromResult(_gameService.GetGames());
        }
        [HttpPost("addGame")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<int> AddGame([FromBody] GameDTO game)
        {
            return await _gameService.AddGame(game);
        }
        [HttpPut("updateGame")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<int> UpdateGame([FromBody] GameDTO game)
        {
            return await (_gameService.UpdateGame(game));
        }
        [HttpDelete("deleteGame")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<int> DeleteGame(int id)
        {
            return await (_gameService.DeleteGame(id));
        }
    }
}
