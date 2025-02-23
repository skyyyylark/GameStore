using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.DataSources
{
    public interface IGameDataSource : IGenericDataSource<Game>
    {
        Task<GameDetailsDTO> GetGameWithCategory(int id);
    }
}
