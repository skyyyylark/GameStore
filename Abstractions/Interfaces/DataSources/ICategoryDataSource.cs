using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.DataSources
{
    public interface ICategoryDataSource : IGenericDataSource<Category>
    {
        /// <summary>
        /// async-ly returns Category with Games included
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Category with Games</returns>
        Task<Category?> GetCategoryWithGames(Expression<Func<Category, bool>> filter); 
    }
}
