using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;

        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        protected virtual DbSet<T> Entities => _entities ??= _context.Set<T>();
        public IQueryable<T> Table => Entities;
        public IQueryable<T> AsNoTracking => Entities.AsNoTracking();

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Entities.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await Entities.AnyAsync(filter);
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return await(filter == null ? _entities.ToListAsync() : _entities.Where(filter).ToListAsync());
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Entities.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            Entities.Update(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            return await Entities.SingleOrDefaultAsync(filter);
        }
    }
}
