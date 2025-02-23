using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Services
{
    public interface ICategoryService
    {
        public List<CategoryDTO> GetCategories();
        public Task<CategoryDetailsDTO> GetCategory(int id);
        public Task<int> AddCategory(CategoryDTO categoryDTO);
        public Task<int> DeleteCategory(int id);
        public Task<int> UpdateCategory(CategoryDTO categoryDTO);
    }
}
