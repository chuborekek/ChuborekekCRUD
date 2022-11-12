using ChuborekekCRUD.Common;
using ChuborekekCRUD.Data;
using ChuborekekCRUD.Enum;
using ChuborekekCRUD.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChuborekekCRUD.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _db;
        private readonly DbSet<T> _table;
        //------------Setting the constructor
        public BaseRepository(ChuborekekCRUDDbContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }
        public async Task Delete(object id)
        {
            var entity = await _table.FindAsync(id);
            if (entity != null)
            {
                _table.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _table;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<string> includes = null)
        {
            IQueryable<T> query = _table;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public RecordsResult<T> GetPaginated(IList<T> list, RecordsRequest rr)
        {
            var totalItems = list.Count();
            int totalPages = (int)(Math.Ceiling((double)totalItems / (double)rr.pageSize));
            int currentPage = (rr.page < 1 || rr.page > totalPages) ? 1 : rr.page;
            int startPage = currentPage - 5; 
            int endPage = currentPage + 4; 


            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
                

            }

            var records = list
               .Skip((currentPage - 1) * (int)rr.pageSize)
               .Take((int)rr.pageSize);


            return new RecordsResult<T>
            {
                Result = records,
                TotalItems = totalItems,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                StartPage = startPage,
                EndPage = endPage,
                PageSize = rr.pageSize,
                SearchKeyword = rr.searchKeyword,
                SortProperty = rr.orderByWhatProperty,
                SortDirection = rr.orderByWhatDirection
            };
                
        }

        public async Task Insert(T entity)
        {
            await _table.AddAsync(entity);

            await _db.SaveChangesAsync();
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        /*
        public void Update(T entity)
        {
            _table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
        */
        public async Task Update(object id, object model)
        {
            var entity = await _table.FindAsync(id);
            if (entity != null)
            {
                _db.Entry(entity).CurrentValues.SetValues(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
