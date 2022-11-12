using ChuborekekCRUD.Common;
using ChuborekekCRUD.Data;

namespace ChuborekekCRUD.Interfaces
{
    public interface ICatRepository : IBaseRepository<Cat>
    {
        Task<RecordsResult<Cat>> GetAllWithCriteria(RecordsRequest recordsRequest);
    }
}
