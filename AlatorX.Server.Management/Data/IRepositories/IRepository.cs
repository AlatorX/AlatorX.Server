using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Commons;

namespace AlatorX.Server.Management.Data.IRepositories
{
    public interface IRepository<TEntity> where TEntity : Auditable
    {
        IQueryable<TEntity> SelectAll();
        ValueTask<TEntity> SelectByIdAsync(long id);
        ValueTask<TEntity> InsertAsync(TEntity entity);
        ValueTask<TEntity> UpdateAsync(TEntity entity);
        ValueTask<bool> DeleteAsync(long id);
    }
}