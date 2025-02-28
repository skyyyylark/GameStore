using Abstractions.Interfaces.DataSources;
using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSource
{
    public class GameDataSource : GenericDataSource<Game>, IGameDataSource
    {
        private readonly IStringLocalizer _localizer;

        public GameDataSource(AppDbContext _context, IStringLocalizerFactory factory) : base(_context, factory)
        {
            _localizer = factory.Create("Common.Resources.SharedResource", "Common");
        }

        public async Task<GameDetailsDTO> GetGameWithCategory(int id)
        {
            var game = await _context.Games
                .Include(g => g.Category)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
                throw new KeyNotFoundException(_localizer["KeyNotFound"]);
            return new GameDetailsDTO
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                Category = game.Category.Name,
            };
        }
    }
}
