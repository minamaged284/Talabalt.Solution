using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specefications;

namespace Talabat.Repository
{
    public class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> innerQuery, ISpecifications<T> spec)
        {
            var query = innerQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);


            }

            query = spec.Includes.Aggregate(query, (x, y) => x.Include(y));
            return query;
        }
    }
}
