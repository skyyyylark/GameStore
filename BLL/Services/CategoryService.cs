using Abstractions.Interfaces.DataSources;
using Abstractions.Interfaces.Services;
using DAL.EntityFramework;
using Mapster;
using Microsoft.Extensions.Localization;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDataSource _categoryDataSource;
        public CategoryService(ICategoryDataSource categoryDataSource)
        {
            _categoryDataSource = categoryDataSource;
        }
        public List<CategoryDTO> GetCategories()
        {
            return _categoryDataSource.GetItems()
                .ProjectToType<CategoryDTO>()
                .ToList();
        }
        public async Task<int> AddCategory(CategoryDTO categoryDTO)
        {
            var category = categoryDTO.Adapt<Category>();
            category = await _categoryDataSource.AddItemAsync(category);
            await _categoryDataSource.SaveChangesAsync();
            return category.Id;
        }
        public async Task<int> DeleteCategory(int id)
        {
            var category = await _categoryDataSource.DeleteItemAsync(id);
            await _categoryDataSource.SaveChangesAsync();
            return category.Id;
        }
        public async Task<int> UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = categoryDTO.Adapt<Category>();
            category = await _categoryDataSource.UpdateItemAsync(category);
            await _categoryDataSource.SaveChangesAsync();
            return category.Id;
        }

        public async Task<CategoryDetailsDTO> GetCategory(int id)
        {
            var category = await _categoryDataSource.GetCategoryWithGames(x => x.Id == id)
                ?? throw new ArgumentNullException();
            return category.Adapt<CategoryDetailsDTO>();
        }
    }
}
