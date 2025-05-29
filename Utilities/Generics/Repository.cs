using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Utilities.Generics
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T?> ReadByIdAsync(object entityKey);
        Task<T?> ReadFirstAsync(Expression<Func<T, bool>>? expression=null);
        Task<IEnumerable<T>> ReadManyAsync(Expression<Func<T, bool>>? expression = null,params string[] includes);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object entityKey);
        Task DeleteAsync(T entity);

    }
    public abstract class Repository<T>:IRepository<T> where T: class
    {
        protected DbContext _context;
        protected DbSet<T> _set;

        protected Repository(DbContext db)
        {
            _context = db;
            _set = db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() =>_set.Remove(entity));
        }

        public Task DeleteAsync(object entityKey)    
        {
            throw new NotImplementedException();
        }

        public async Task<T?> ReadByIdAsync(object entityKey)
        {
            return await _set.FindAsync(entityKey);
        }

        public async Task<T?> ReadFirstAsync(Expression<Func<T, bool>>? expression = null)
        {
            return expression != null ? await _set.FirstOrDefaultAsync(expression) : await _set.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ReadManyAsync(Expression<Func<T, bool>>? expression = null, params string[] includes)
        {
            IQueryable<T> data = expression != null ? _set.Where(expression) : _set;
            foreach( var include in includes)
            {
                data=data.Include(include);
            }
            return await data.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => _set.Update(entity));
        }
    }
}
