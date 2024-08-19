using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        IQueryable<T> Table { get; }
        IQueryable<T> AsNoTracking { get; }
        Task<T> GetByIdAsync(Guid id);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
