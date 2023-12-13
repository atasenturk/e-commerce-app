using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Api.Domain.Models;

namespace E_CommerceApp.Api.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<bool> Exists(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(Guid? id);
        void UpdateAsync(TEntity entity);
        IQueryable<TEntity> AsQueryable();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
