using Abstractions.Interfaces.DataSources;
using Abstractions.Interfaces.Services;
using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSource
{
    public class CategoryDataSource : GenericDataSource<Category>, ICategoryDataSource
    {
        public CategoryDataSource(AppDbContext _context) : base(_context) { }

        public async Task<Category?> GetCategoryWithGames(Expression<Func<Category, bool>> filter)
        {
            return await _context.Categories.Include(x => x.Games).FirstOrDefaultAsync(filter);
        }
    }
}
