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

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            _dbSet.Remove(entity);
            
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async ValueTask<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public IQueryable<TEntity> SelectAll()
        {
            return _dbSet;
        }

        public async ValueTask<TEntity> SelectByIdAsync(long id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = _dbSet.Update(entity);

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}