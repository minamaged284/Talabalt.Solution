using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryInterfaces;

namespace Talabat.Repository.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {

            if (typeof(T) == typeof(Products))
            {
                return (IEnumerable < T >) await _dbContext.Set<Products>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();

            }
            return await _dbContext.Set<T>().ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Products))
            {
                return await _dbContext.Set<Products>().Where(p=>p.Id==id).Include(p=>p.Category).Include(p=>p.Brand).FirstOrDefaultAsync() as T;
                

            }
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
