using System.Linq.Expressions;
using User_Registration_System.Shared.Entities;

namespace User_Registration_System.Shared.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetByCriteriaAsync(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);

        Task<int> SaveChangesAsync();

    }
}
