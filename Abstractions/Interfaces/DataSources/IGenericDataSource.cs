using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.DataSources
{
    public interface IGenericDataSource<T>
    {
        IQueryable<T> GetItems(Expression<Func<T, bool>>? filter = null);
        Task<T> GetById(int id);
        Task<T> AddItemAsync(T item);
        Task<T> DeleteItemAsync(int id);
        Task<T> UpdateItemAsync(T item);
        Task SaveChangesAsync();   
    }
}
