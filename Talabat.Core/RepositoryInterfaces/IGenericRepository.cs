﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specefications;

namespace Talabat.Core.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllBySpecAsync(ISpecifications<T> spec);
        Task<T> GetBySpecAsync(ISpecifications<T> spec);



    }
}
