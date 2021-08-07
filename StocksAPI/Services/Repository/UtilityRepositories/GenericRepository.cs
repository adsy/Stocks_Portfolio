using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.IRepository;
using Services.Models;
using X.PagedList;

namespace Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StockDbContext _dbContext;
        private readonly DbSet<T> _db;

        public GenericRepository(StockDbContext dbContext)
        {
            _dbContext = dbContext;
            _db = dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            // gets all the records in that table <T>
            IQueryable<T> query = _db;

            // check if expression is null - if not null, filter original query (all records) by the expression.
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                // for each property in the includes list, add that property to the query
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // any record recieved through query is not tracked - turn data into a list
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetAll(RequestParams requestParmas, List<string> includes = null)
        {
            // gets all the records in that table <T>
            IQueryable<T> query = _db;

            if (includes != null)
            {
                // for each property in the includes list, add that property to the query
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            // any record recieved through query is not tracked - turn data into a list
            return await query.AsNoTracking().ToPagedListAsync(requestParmas.PageNumber, requestParmas.PageSize);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            // gets all the records in that table <T>
            IQueryable<T> query = _db;

            if (includes != null)
            {
                // for each property in the includes list, add that property to the query
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            // any record recieved through query is not tracked
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            // attach function - pay attention to certain entity
            _db.Attach(entity);
            // lets db know it has been updated, and updates the object
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}