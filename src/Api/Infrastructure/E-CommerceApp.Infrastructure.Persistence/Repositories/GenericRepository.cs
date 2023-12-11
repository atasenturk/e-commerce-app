using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ECommerceDbContext _context;

        private DbSet<TEntity> Entity => _context.Set<TEntity>();

        public GenericRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null) this.Entity.Remove(entity);
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await this.Entity.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }
            return await this.Entity.FindAsync(id);
        }

        public void UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
        }

        public IQueryable<TEntity> AsQueryable() => this.Entity.AsQueryable();
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
