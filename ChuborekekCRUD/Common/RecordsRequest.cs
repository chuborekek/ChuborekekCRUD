using ChuborekekCRUD.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ChuborekekCRUD.Common
{
    public class RecordsRequest
    {
        [FromQuery(Name = "sk")]
        public string searchKeyword { get; set; } = null;
        [FromQuery(Name = "op")]
        public string orderByWhatProperty { get; set; } = null;
        [FromQuery(Name = "od")]
        public SortDirection orderByWhatDirection { get; set; }
        [FromQuery(Name = "pg")]
        public int page { get; set; }
        [FromQuery(Name = "ps")]
        public int pageSize { get; set; } = 10;
    }
}
