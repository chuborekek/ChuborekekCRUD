using ChuborekekCRUD.Common;
using ChuborekekCRUD.Data;
using ChuborekekCRUD.Enum;
using ChuborekekCRUD.Interfaces;
using System.Linq.Expressions;

namespace ChuborekekCRUD.Repositories
{
    public class DogRepository : BaseRepository<Dog>, IDogRepository
    {
        public DogRepository(ChuborekekCRUDDbContext db) : base(db)
        {

        }

        public async Task<RecordsResult<Dog>> GetAllWithCriteria(RecordsRequest recordsRequest)
        {
            //get the request of client
            var rr = new RecordsRequest();
            rr = recordsRequest;
            //for searching 
            Expression<Func<Dog, bool>> whereExpression =
                r => r.Name.Contains(rr.searchKeyword ?? string.Empty);
            //for sorting
            Func<IQueryable<Dog>, IOrderedQueryable<Dog>> orderExpression = null;
            if (rr.orderByWhatProperty != null)
            {
                if (rr.orderByWhatProperty == "Name")
                {
                    if (rr.orderByWhatDirection == SortDirection.Ascending)
                        orderExpression = m => m.OrderBy(x => x.Name);
                    else
                        orderExpression = m => m.OrderByDescending(x => x.Name);
                }
                else if (rr.orderByWhatProperty == "dogBreed")
                {
                    if (rr.orderByWhatDirection == SortDirection.Ascending)
                        orderExpression = m => m.OrderBy(x => x.dogBreed);
                    else
                        orderExpression = m => m.OrderByDescending(x => x.dogBreed);
                }
            }
            //if there is an entity to be included
            List<string> includes = new List<string>();
            string[] entitiesToInclude = { }; //string[] entitiesToInclude = {"Entity1","Entity2"}
            includes.AddRange(entitiesToInclude);


            var dogs = await GetAll(
                    whereExpression,
                    orderExpression,
                    includes
                );


            return GetPaginated(dogs, recordsRequest);
        }
    }
}
