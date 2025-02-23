using Mapster;
using Models.DTOs;
using Models.Entities;

namespace test.Extensions
{
    public static class MapsterExtension
    {
        public static void InitMapping(this IApplicationBuilder app)
        {
            app.GameMapping();
        }
        public static void GameMapping(this IApplicationBuilder app)
        {
            TypeAdapterConfig<Game, GameDTO>.NewConfig()
                .Map(dest => dest.CategoryName, src => src.Category.Name);
            TypeAdapterConfig<Category, CategoryDetailsDTO>.NewConfig()
                .Map(dest => dest.Games, src => src.Games == null
                     ? new List<GameDTO>()
                     : src.Games.Adapt<List<GameDTO>>());
        }
    }
}
