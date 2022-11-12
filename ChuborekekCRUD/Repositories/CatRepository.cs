using ChuborekekCRUD.Common;
using ChuborekekCRUD.Data;
using ChuborekekCRUD.Enum;
using ChuborekekCRUD.Interfaces;
using System.Linq.Expressions;

namespace ChuborekekCRUD.Repositories
{
    public class CatRepository : BaseRepository<Cat>, ICatRepository
    {
        public CatRepository(ChuborekekCRUDDbContext db) : base(db)
        {
        }

        public async Task<RecordsResult<Cat>> GetAllWithCriteria(RecordsRequest recordsRequest)
        {
            //get the request of client
            var rr = new RecordsRequest();
            rr = recordsRequest;
            //for searching 
            Expression<Func<Cat, bool>> whereExpression =
                r => r.Name.Contains(rr.searchKeyword ?? string.Empty);
            //for sorting
            Func<IQueryable<Cat>, IOrderedQueryable<Cat>> orderExpression=null;
            if (rr.orderByWhatProperty != null)
            {
               if(rr.orderByWhatProperty == "Name")
               {
                    if (rr.orderByWhatDirection == SortDirection.Ascending)
                        orderExpression = m => m.OrderBy(x => x.Name);
                    else
                        orderExpression = m => m.OrderByDescending(x => x.Name);
                }
                else if (rr.orderByWhatProperty == "catBreed")
                {
                    if (rr.orderByWhatDirection == SortDirection.Ascending)
                        orderExpression = m => m.OrderBy(x => x.catBreed);
                    else
                        orderExpression = m => m.OrderByDescending(x => x.catBreed);
                }
            }
            //if there is an entity to be included
            List<string> includes = new List<string>();
            string[] entitiesToInclude = { }; //string[] entitiesToInclude = {"Entity1","Entity2"}
            includes.AddRange(entitiesToInclude);


            var cats = await GetAll(
                    whereExpression,
                    orderExpression,
                    includes
                );
            

            return GetPaginated(cats, recordsRequest);
        }
    }
}
