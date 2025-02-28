using Abstractions.Interfaces.DataSources;
using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSource
{
    public class GenericDataSource<T> : IGenericDataSource<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly IStringLocalizer _localizer;
        public GenericDataSource(AppDbContext context, IStringLocalizerFactory factory)
        {
            _context = context;
            _localizer = factory.Create("Common.Resources.SharedResource", "Common"); ;
        }
        protected DbSet<T> Set => _context.Set<T>();

        public IQueryable<T> GetItems(Expression<Func<T, bool>>? filter = null)
        {
            if (filter is not null)
                return Set.Where(filter).AsNoTracking();
            return Set.AsNoTracking();
        }
        public async Task<T> AddItemAsync(T item)
        {
            await Set.AddAsync(item);
            return item;
        }

        public async Task<T> DeleteItemAsync(int id)
        {
            T item = await Set.FindAsync(id);
            if (item == null)
                throw new KeyNotFoundException(_localizer["KeyNotFound"]);
            Set.Remove(item);
            await _context.SaveChangesAsync();
            return item;

        }
        public async Task<T> UpdateItemAsync(T item)
        {
            T findingItem = await Set.FindAsync(item);
            if (findingItem == null)
                throw new ArgumentNullException(_localizer["ArgumentNull"]);
            await Set.AddAsync(findingItem);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<T> GetById(int id)
        {
            T item = await Set.FindAsync(id);
            if(item == null)
                throw new KeyNotFoundException(_localizer["KeyNotFound"]);
            return item;    
        }
    }
}
