using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Data.DbContexts;
using AlatorX.Server.Management.Data.IRepositories;
using AlatorX.Server.Management.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace AlatorX.Server.Management.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public ValueTask<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TEntity> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> SelectAll()
        {
            throw new NotImplementedException();
        }

        public ValueTask<TEntity> SelectByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}