using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using User_Registration_System.Data.DBContexts;
using User_Registration_System.Shared.Entities;
using User_Registration_System.Shared.Interfaces;

namespace User_Registration_System.Shared.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbContext;

        private readonly DbSet<T> _dbSet;


        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            _dbSet = dbContext.Set<T>();

        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetByCriteriaAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

    }
}
