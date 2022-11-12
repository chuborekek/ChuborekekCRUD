using ChuborekekCRUD.Common;
using System.Linq.Expressions;

namespace ChuborekekCRUD.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
          Expression<Func<T, bool>> expression = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
          List<string> includes = null
          );

        Task<T> Get(
            Expression<Func<T, bool>> expression,
            List<string> includes = null
            );

        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        Task Delete(object id);
        Task Update(object id, object model);
        RecordsResult<T> GetPaginated(IList<T> list, RecordsRequest rr);

    }
}
