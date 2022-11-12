using ChuborekekCRUD.Common;
using ChuborekekCRUD.Data;

namespace ChuborekekCRUD.Interfaces
{
    public interface IDogRepository: IBaseRepository<Dog>
    {
        Task<RecordsResult<Dog>> GetAllWithCriteria(RecordsRequest recordsRequest);
    }
}
